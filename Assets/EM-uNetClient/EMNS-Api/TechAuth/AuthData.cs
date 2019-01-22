using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.IO;
using Utf8Json;

namespace kde.tech
{

public class AuthData
{

    /*=========================================================*/

    public class Request
    {
	[DataMember(Name = "role")]
	public string role { get; set; } = "server";
	[DataMember(Name = "mode")]
	public string mode { get; set; } = "";
	[DataMember(Name = "userProfile")]
	public UserData.Profile userProfile { get; set; }  = new UserData.Profile();
	// Takeover時のみ使用
	[DataMember(Name = "sessCode")]
	public string sessCode { get; set; } = "";	
	[DataMember(Name = "takeoverCode")] 
	public string takeoverCode { get; set; }  = "";
    }
    
    public class Response
    {
	[DataMember(Name = "status")]
	public string status { get; set; }	
	[DataMember(Name = "meta")]
	public string meta { get; set; } = "dummy";
	[DataMember(Name = "sessCode")]  
	public string sessCode { get; set; } = "";	
	[DataMember(Name = "userProfile")]
	public UserData.Profile userProfile { get; set; } = new UserData.Profile();
	[DataMember(Name = "takeoverCode")] 
	public string takeoverCode { get; set; } = "";
    }
    
    /*=========================================================*/
    
    
}
}
