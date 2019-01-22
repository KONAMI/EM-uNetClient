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

namespace kde.tech
{

public class InfoClient
{

    string m_apiUrl;
    
    public InfoClient(string apiUrl){
	m_apiUrl = apiUrl;
    }

    public async Task<InfoData.Response> LoadInfo(){
	var resp = new InfoData.Response(){ status = InfoError.E_CHAOS.ToString() };
	
	using (var client = new HttpClient()){
	    
	    var response = await client.PostAsync
		(
		 m_apiUrl
		 , new StringContent
		 (
		  JsonSerializer.ToJsonString(new InfoData.Request(){ mode = "load"})
		  ,new UTF8Encoding()
		  ,"application/json"
		  )
		 );
	    var responseContent = await response.Content.ReadAsStringAsync();
	    
	    if(!String.IsNullOrEmpty(responseContent)){
		resp = JsonSerializer.Deserialize<InfoData.Response>(responseContent);		
	    }
	    else {
		resp.status = InfoError.E_CRITICAL.ToString();
	    }
	}
	
	return resp;
    } 


    
}
}
