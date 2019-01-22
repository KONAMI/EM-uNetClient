using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;
using System.Net.Http;
using Utf8Json;

#if INCLUDE_AWS_CODE
using Amazon.Lambda.Core;
#else
using UnityEngine;
#endif

namespace kde.tech
{

public class StoreClient
{

    string m_apiUrl;
    string m_apiKey;
    
    public StoreClient(string apiUrl, string apiKey){
	m_apiUrl = apiUrl;
	m_apiKey = apiKey;
    }

#if INCLUDE_AWS_CODE
    ILambdaContext m_ctx;
    public StoreClient(string apiUrl, string apiKey, ILambdaContext ctx){
        m_apiUrl = apiUrl;
	m_apiKey = apiKey;
        m_ctx    = ctx;
    }
    void Log(string msg){
        m_ctx.Log(msg);
    }
#else
    void Log(string msg){
        UnityEngine.Debug.Log(msg);
    }
#endif
    
    public async Task<StoreData.Response> Load(string sessCode,
					       string dataName, int dataIndex = 1){
	var resp = new StoreData.Response(){ status = StoreError.E_CHAOS.ToString() };

	Log(m_apiUrl);
	Log(m_apiKey);
	
	using (var client = new HttpClient()){
	    	    
            try {

		var request = new HttpRequestMessage(HttpMethod.Post, m_apiUrl);
		request.Content = new StringContent
		    (
		     JsonSerializer.ToJsonString(new StoreData.Request(){
			     role      = "server",
			     mode      = "load",
			     sessCode  = sessCode,
			     dataName  = dataName,
			     dataIndex = dataIndex
			 })
		     ,new UTF8Encoding()
		     ,"application/json"
		     );
		
		request.Headers.Add(@"x-api-key", m_apiKey);
		
		var response = await client.SendAsync(request);
		
                var responseContent = await response.Content.ReadAsStringAsync();
    
                if(!String.IsNullOrEmpty(responseContent)){
		    Log("Got Resp >> " + responseContent);
                    resp = JsonSerializer.Deserialize<StoreData.Response>(responseContent);
                }
                else {
                    resp.status = StoreError.E_CRITICAL.ToString();
                }
            }
            catch(Exception e){
                Log("Exception : " + e.ToString());
                resp.status = StoreError.E_CRITICAL.ToString();
            }
	}
	return resp;
    }

    public async Task<StoreData.Response> Save(string sessCode,
					       string dataBody, StoreData.Type dataType, 
					       string dataName, int dataIndex = 1){
	var resp = new StoreData.Response(){ status = StoreError.E_CHAOS.ToString() };

	Log(m_apiUrl);
	Log(m_apiKey);
	
	using (var client = new HttpClient()){
	    	    
            try {

		var request = new HttpRequestMessage(HttpMethod.Post, m_apiUrl);
		request.Content = new StringContent
		    (
		     JsonSerializer.ToJsonString(new StoreData.Request(){
			     role      = "server",
			     mode      = "save",
			     sessCode  = sessCode,
			     dataName  = dataName,
			     dataIndex = dataIndex,
			     data      = dataBody,
			     dataType  = (int)dataType
			 })
		     ,new UTF8Encoding()
		     ,dataType.ToContentType()
		     );
		
		request.Headers.Add(@"x-api-key", m_apiKey);
		
		var response = await client.SendAsync(request);
		
                var responseContent = await response.Content.ReadAsStringAsync();
    
                if(!String.IsNullOrEmpty(responseContent)){
		    Log("Got Resp >> " + responseContent);
                    resp = JsonSerializer.Deserialize<StoreData.Response>(responseContent);
                }
                else {
                    resp.status = StoreError.E_CRITICAL.ToString();
                }
            }
            catch(Exception e){
                Log("Exception : " + e.ToString());
                resp.status = StoreError.E_CRITICAL.ToString();
            }
	}
	return resp;
    }

    
}
}
