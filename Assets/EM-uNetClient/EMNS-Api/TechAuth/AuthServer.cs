#if INCLUDE_AWS_CODE
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Utf8Json;

namespace kde.tech
{

public class AuthServer
{
    private static readonly Encoding encoding = Encoding.UTF8;
    
    public AuthServer(ILambdaContext ctx){
	m_ctx = ctx;	
    }
    
    ILambdaContext m_ctx;

    public async Task<AuthData.Response> Regist(LambdaAuthArg data){
	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	var userData = UserData.CreateNewUser();

	try {	    
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {
		// まず、S3にUIDに対応したプロファイルを保存
		var s3Req = new PutObjectRequest
		{
		    BucketName  = data.s3Bucket,
		    Key         = UserData.GetS3PathFromUid(data.s3DirObject, userData.profile.uid),
		    ContentType = @"application/json",
		    ContentBody = userData.Export()
		};
		var s3Resp = await s3Client.PutObjectAsync(s3Req);		
		var sess   = new UserData.Session(userData.profile);
				
		// Sessionを発行して、Redisサーバに登録
		var redisRet = await RedisClientHelper.Set(m_ctx,data.redisApiUrl, data.redisApiKey,
							   sess.keyField, sess.valueField, sess.ttlSec);
		if(redisRet == RedisError.E_OK){
		    resp.status      = AuthError.E_OK.ToString();
		    resp.userProfile = userData.profile;		
		    resp.sessCode    = sess.sessCode;		    
		}
		else {
		    resp.status = AuthError.E_SESS_ISSUE.ToString();
		    resp.meta   = redisRet.ToString();
		    m_ctx.Log(resp.status.ToString() + "," + resp.meta);
		}
	    }
	}
	catch (Exception ex){
	    m_ctx.Log(ex.Message);	
	    resp.status = AuthError.E_CRITICAL.ToString();
	    resp.meta   = ex.Message;
	}
	
	return resp;
    }

    // Guidでログイン
    public async Task<AuthData.Response> Login(LambdaAuthArg data){
	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	var userData = new UserData(data.userProfile.guid);

	try{	    
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {
		// まず、S3にUIDに対応したプロファイルがあるかチェック	       
		var s3Req = new GetObjectRequest
		{
		    BucketName = data.s3Bucket,
		    Key        = UserData.GetS3PathFromUid(data.s3DirObject, userData.profile.uid),
		};		
		var s3Resp = await s3Client.GetObjectAsync(s3Req);
		
		using(var s3Stream = new StreamReader(s3Resp.ResponseStream)){
		    var dataBody  = s3Stream.ReadToEnd();
		    userData.Import(dataBody);		    
		}

		var sess   = new UserData.Session(userData.profile);

		// Sessionを発行して、Redisサーバに登録
		var redisRet = await RedisClientHelper.Set(m_ctx,data.redisApiUrl, data.redisApiKey,
							   sess.keyField, sess.valueField, sess.ttlSec);
		if(redisRet == RedisError.E_OK){
		    resp.status      = AuthError.E_OK.ToString();
		    resp.userProfile = userData.profile;		
		    resp.sessCode    = sess.sessCode;		    
		}
		else {
		    resp.status = AuthError.E_SESS_ISSUE.ToString();
		    resp.meta   = redisRet.ToString();
		    m_ctx.Log(resp.status.ToString() + "," + resp.meta);			    
		}
	    }
	}
	catch (Exception ex){
	    m_ctx.Log(ex.Message);	
	    resp.status = AuthError.E_CRITICAL.ToString();
	    resp.meta   = ex.Message;
	}
		
	return resp;
    }

    public async Task<AuthData.Response> CreateTakeoverCode(LambdaAuthArg data){
	
	var resp     = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	var userData = new UserData("", data.userProfile.uid);
	var sess     = new UserData.Session(userData.profile, data.sessCode);
	    
	try {
	    
	    // セッションチェック	    
	    if(await RedisClientHelper.CheckValueEqual
	       (m_ctx, data.redisApiUrl, data.redisApiKey,sess.keyField, sess.valueField) != RedisError.E_OK){
		resp.status = AuthError.E_SESS_INVALID.ToString();
		return resp;
	    }
	    
	    // S3からGUIDを取得
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {	    
		var s3Req = new GetObjectRequest
		{
		    BucketName = data.s3Bucket,
		    Key        = UserData.GetS3PathFromUid(data.s3DirObject, userData.profile.uid),
		};		
		var s3Resp = await s3Client.GetObjectAsync(s3Req);
	    
		using(var s3Stream = new StreamReader(s3Resp.ResponseStream)){
		    var dataBody  = s3Stream.ReadToEnd();
		    userData.Import(dataBody);		    
		}
	    }

	    // 引き継ぎコードをRedisにセット
	    var takeover = new UserData.TakeoverCode(userData.profile);
	    var redisRet = await RedisClientHelper.Set(m_ctx, data.redisApiUrl, data.redisApiKey,
						       takeover.keyField, takeover.valueField, takeover.ttlSec);
	    
	    if(redisRet == RedisError.E_OK){
		resp.status       = AuthError.E_OK.ToString();
		resp.takeoverCode = takeover.takeoverCode;		
	    }
	    else {
		resp.status = AuthError.E_TAKEOVER_ISSUE.ToString();
		resp.meta   = redisRet.ToString();
		m_ctx.Log(resp.status.ToString() + "," + resp.meta);	
	    }
	}
	catch (Exception ex){
	    m_ctx.Log(ex.Message);	
	    resp.status = AuthError.E_CRITICAL.ToString();
	    resp.meta   = ex.Message;
	}
		
	return resp;
    }

    // uid と takeoverCode から GUID を引いて返す
    // 基本この後、GUIDを使って、LOGINしなおす
    public async Task<AuthData.Response> ConsumeTakeoverCode(LambdaAuthArg data){
	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	var userData = new UserData("", data.userProfile.uid);
	var takeover = new UserData.TakeoverCode(userData.profile, data.takeoverCode);

	try {
	    var guid = await RedisClientHelper.Get(m_ctx, data.redisApiUrl, data.redisApiKey, takeover.keyField);
	    if(guid == ""){
		resp.status = AuthError.E_TAKEOVER_INVALID.ToString();
		return resp;
	    }

	    userData.profile.guid = guid;
	    
	    // この時点で、引き継ぎコードを無効化する。
	    var redisRet = await RedisClientHelper.Set(m_ctx, data.redisApiUrl, data.redisApiKey,
						       takeover.keyField, "", 1);
	    	    
	    resp.status      = AuthError.E_OK.ToString();
	    resp.userProfile = userData.profile;
	}
	catch (Exception ex){
	    m_ctx.Log(ex.Message);	
	    resp.status = AuthError.E_CRITICAL.ToString();
	    resp.meta   = ex.Message;
	}
		
	return resp;
    }

    public async Task<AuthData.Response> SaveData(LambdaAuthArg data){
	
	var resp     = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	var userData = new UserData("", data.userProfile.uid);
	var sess     = new UserData.Session(userData.profile, data.sessCode);
	    
	try {
	    
	    // セッションチェック	    
	    if(await RedisClientHelper.CheckValueEqual
	       (m_ctx, data.redisApiUrl, data.redisApiKey,sess.keyField, sess.valueField) != RedisError.E_OK){
		resp.status = AuthError.E_SESS_INVALID.ToString();
		return resp;
	    }
	    
	    // S3からGUIDを取得
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {	    
		var s3Req = new GetObjectRequest
		{
		    BucketName = data.s3Bucket,
		    Key        = UserData.GetS3PathFromUid(data.s3DirObject, userData.profile.uid),
		};		
		var s3Resp = await s3Client.GetObjectAsync(s3Req);
	    
		using(var s3Stream = new StreamReader(s3Resp.ResponseStream)){
		    var dataBody  = s3Stream.ReadToEnd();
		    userData.Import(dataBody);		    
		}
	    }

	    // 引き継ぎコードをRedisにセット
	    var takeover = new UserData.TakeoverCode(userData.profile);
	    var redisRet = await RedisClientHelper.Set(m_ctx, data.redisApiUrl, data.redisApiKey,
						       takeover.keyField, takeover.valueField, takeover.ttlSec);
	    
	    if(redisRet == RedisError.E_OK){
		resp.status       = AuthError.E_OK.ToString();
		resp.takeoverCode = takeover.takeoverCode;		
	    }
	    else {
		resp.status = AuthError.E_TAKEOVER_ISSUE.ToString();
		resp.meta   = redisRet.ToString();
		m_ctx.Log(resp.status.ToString() + "," + resp.meta);	
	    }
	}
	catch (Exception ex){
	    m_ctx.Log(ex.Message);	
	    resp.status = AuthError.E_CRITICAL.ToString();
	    resp.meta   = ex.Message;
	}
		
	return resp;
    }

    
}
}
#endif
