using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Utf8Json;

namespace kde.tech
{

public class InfoData
{

    /*=========================================================*/

    public class Request
    {
	[DataMember(Name = "role")]
	public string role { get; set; } = "server";
	[DataMember(Name = "mode")]
	public string mode { get; set; }	
    }
    
    public class Response
    {
	[DataMember(Name = "status")]
	public string status { get; set; }	
	[DataMember(Name = "apiInfo")]
	public ApiInfo apiInfo { get; set; }
    }
    
    public class ApiInfo
    {
	public class Api {
	    [DataMember(Name = "name")]
	    public string name { get; set; }	
	    [DataMember(Name = "url")]
	    public string url { get; set; }	
	}
	
	[DataMember(Name = "lastupdate")]
	public int lastupdate { get; set; }	
	[DataMember(Name = "key")]
	public string key { get; set; }	
	[DataMember(Name = "list")]
	public List<Api> list { get; set; }	
    }
    
    /*=========================================================*/
        
}
}
