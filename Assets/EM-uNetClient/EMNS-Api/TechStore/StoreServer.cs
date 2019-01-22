#if INCLUDE_AWS_CODE
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Utf8Json;

namespace kde.tech
{

public class StoreServer
{
    private static readonly Encoding encoding = Encoding.UTF8;
    
    public StoreServer(ILambdaContext ctx){
	m_ctx = ctx;
    }

    ILambdaContext m_ctx;
    
    public async Task<StoreData.Response> Load(LambdaStoreArg data){
	
	var resp = new StoreData.Response(){ status = StoreError.E_CHAOS.ToString() };

	try{	    
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {
		var s3Req = new GetObjectRequest
		{
		    BucketName = data.s3Bucket,
		    Key        = data.s3Path
		};		
		var s3Resp = await s3Client.GetObjectAsync(s3Req);
		
		using(var s3Stream = new StreamReader(s3Resp.ResponseStream)){
		    var dataBody  = s3Stream.ReadToEnd();
		    resp.status   = StoreError.E_OK.ToString();
		    resp.dataBody = Convert.ToBase64String(encoding.GetBytes(dataBody));
		}
	    }
	}
	catch (Exception ex){
	    m_ctx.Log(ex.Message);	
	    resp.status = StoreError.E_CRITICAL.ToString();
	    resp.meta   = data.s3Path + " : " + ex.Message;
	}
		
	return resp;
    }

    public async Task<StoreData.Response> Save(LambdaStoreArg data){
	
	var resp    = new StoreData.Response(){ status = StoreError.E_CHAOS.ToString() };

	try {	
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {
		var s3Req = new PutObjectRequest
		{
		    BucketName  = data.s3Bucket,
		    Key         = data.s3Path,
		    ContentType = ((StoreData.Type)data.dataType).ToContentType(),
		    ContentBody = encoding.GetString(Convert.FromBase64String(data.dataBody))
		};
		var s3Resp = await s3Client.PutObjectAsync(s3Req);
		resp.status = StoreError.E_OK.ToString();
	    }
	}
	catch(Exception e)
	{
	    m_ctx.Log(e.ToString());
	    resp.status = StoreError.E_CRITICAL.ToString();
	}
	
	return resp;
    }
    
    
}
}
#endif
