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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Utf8Json;

namespace kde.tech
{

public class InfoServer
{
    
    public InfoServer(){
    }

    public async Task<InfoData.Response> LoadInfo(LambdaInfoArg data, ILambdaContext ctx){
	var resp = new InfoData.Response(){ status = InfoError.E_CHAOS.ToString() };

	try{	    
	    using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.APNortheast1))
	    {
		var s3Req = new GetObjectRequest
		{
		    BucketName = data.s3Bucket,
		    Key        = data.s3InfoObject
		};		
		var s3Resp = await s3Client.GetObjectAsync(s3Req);
		
		using(var s3Stream = new StreamReader(s3Resp.ResponseStream)){
		    var json    = s3Stream.ReadToEnd();
		    resp.apiInfo = JsonSerializer.Deserialize<InfoData.ApiInfo>(json);
		    resp.status = InfoError.E_OK.ToString();
		}
	    }
	}
	catch (Exception ex){
	    ctx.Log(ex.Message);	
	    resp.status = InfoError.E_CRITICAL.ToString();
	}
		
	return resp;
    }
    
    
}
}
#endif
