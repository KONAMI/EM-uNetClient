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

public class AuthClient
{

    string m_apiUrl;
    string m_apiKey;

    public AuthClient(string apiUrl, string apiKey){
	m_apiUrl = apiUrl;
	m_apiKey = apiKey;
    }
    
#if INCLUDE_AWS_CODE
    ILambdaContext m_ctx;
    public AuthClient(string apiUrl, string apiKey, ILambdaContext ctx){
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

    public async Task<AuthData.Response> Regist(){	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	using (var client = new HttpClient()){
	    
            try {

		var request = new HttpRequestMessage(HttpMethod.Post, m_apiUrl);
		request.Content = new StringContent
		    (
		     JsonSerializer.ToJsonString(new AuthData.Request(){
			     role      = "server",
			     mode      = "regist"
			 })
		     ,Encoding.UTF8, @"application/json"
		     );
		
		request.Headers.Add(@"x-api-key", m_apiKey);
		
		var response = await client.SendAsync(request);		
                var responseContent = await response.Content.ReadAsStringAsync();
    
                if(!String.IsNullOrEmpty(responseContent)){
		    Log("Got Resp >> " + responseContent);
                    resp = JsonSerializer.Deserialize<AuthData.Response>(responseContent);
                }
                else {
                    resp.status = AuthError.E_CRITICAL.ToString();
                }
            }
            catch(Exception e){
                Log("Exception : " + e.ToString());
                resp.status = AuthError.E_CRITICAL.ToString();
            }
	}
	return resp;
    }

    public async Task<AuthData.Response> Login(string guid){	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	using (var client = new HttpClient()){
	    
            try {

		var request = new HttpRequestMessage(HttpMethod.Post, m_apiUrl);
		request.Content = new StringContent
		    (
		     JsonSerializer.ToJsonString(new AuthData.Request(){
			     role      = "server",
			     mode      = "login",
			     userProfile = new UserData.Profile(){ guid = guid }
			 })
		     ,Encoding.UTF8, @"application/json"
		     );
		
		request.Headers.Add(@"x-api-key", m_apiKey);
		
		var response = await client.SendAsync(request);		
                var responseContent = await response.Content.ReadAsStringAsync();
    
                if(!String.IsNullOrEmpty(responseContent)){
		    Log("Got Resp >> " + responseContent);
                    resp = JsonSerializer.Deserialize<AuthData.Response>(responseContent);
                }
                else {
                    resp.status = AuthError.E_CRITICAL.ToString();
                }
            }
            catch(Exception e){
                Log("Exception : " + e.ToString());
                resp.status = AuthError.E_CRITICAL.ToString();
            }
	}
	return resp;
    }

    public async Task<AuthData.Response> CreateTakeoverCode(string uid, string sessCode){	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	using (var client = new HttpClient()){
	    
            try {

		var request = new HttpRequestMessage(HttpMethod.Post, m_apiUrl);
		request.Content = new StringContent
		    (
		     JsonSerializer.ToJsonString(new AuthData.Request(){
			     role      = "server",
			     mode      = "createTakeoverCode",
			     userProfile = new UserData.Profile(){ uid = uid },
			     sessCode = sessCode
			 })
		     ,Encoding.UTF8, @"application/json"
		     );
		
		request.Headers.Add(@"x-api-key", m_apiKey);
		
		var response = await client.SendAsync(request);		
                var responseContent = await response.Content.ReadAsStringAsync();
    
                if(!String.IsNullOrEmpty(responseContent)){
		    Log("Got Resp >> " + responseContent);
                    resp = JsonSerializer.Deserialize<AuthData.Response>(responseContent);
                }
                else {
                    resp.status = AuthError.E_CRITICAL.ToString();
                }
            }
            catch(Exception e){
                Log("Exception : " + e.ToString());
                resp.status = AuthError.E_CRITICAL.ToString();
            }
	}
	return resp;
    }
    
    public async Task<AuthData.Response> ConsumeTakeoverCode(string uid, string takeoverCode){	
	var resp = new AuthData.Response(){ status = AuthError.E_CHAOS.ToString() };
	using (var client = new HttpClient()){
	    
            try {

		var request = new HttpRequestMessage(HttpMethod.Post, m_apiUrl);
		request.Content = new StringContent
		    (
		     JsonSerializer.ToJsonString(new AuthData.Request(){
			     role      = "server",
			     mode      = "consumeTakeoverCode",
			     userProfile = new UserData.Profile(){ uid = uid },
			     takeoverCode = takeoverCode
			 })
		     ,Encoding.UTF8, @"application/json"
		     );
		
		request.Headers.Add(@"x-api-key", m_apiKey);
		
		var response = await client.SendAsync(request);		
                var responseContent = await response.Content.ReadAsStringAsync();
    
                if(!String.IsNullOrEmpty(responseContent)){
		    Log("Got Resp >> " + responseContent);
                    resp = JsonSerializer.Deserialize<AuthData.Response>(responseContent);
                }
                else {
                    resp.status = AuthError.E_CRITICAL.ToString();
                }
            }
            catch(Exception e){
                Log("Exception : " + e.ToString());
                resp.status = AuthError.E_CRITICAL.ToString();
            }
	}
	return resp;
    }
    
}
}
