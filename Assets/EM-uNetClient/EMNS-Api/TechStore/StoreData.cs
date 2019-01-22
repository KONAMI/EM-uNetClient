using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.IO;
using Utf8Json;

namespace kde.tech
{

public class StoreData
{
    private static readonly Encoding encoding = Encoding.UTF8;

    /*=========================================================*/

    public class Request
    {
	[DataMember(Name = "role")]
	public string role { get; set; } = "server";
	[DataMember(Name = "mode")]
	public string mode { get; set; }
	[DataMember(Name = "sessCode")]
	public string sessCode { get; set; } = "";
	[DataMember(Name = "dataName")]
	public string dataName { get; set; } = "";
	[DataMember(Name = "dataIndex")]
	public int dataIndex { get; set; } = 1;
	// Save Mode Only
	[DataMember(Name = "dataBody")]
	public string dataBody { get; set; } = "";
	[DataMember(Name = "dataType")]
	public int dataType { get; set; } = 0;
	
	/*==[ HelperMethod ]==================================*/
	[IgnoreDataMember]
	public string data {
	    set { dataBody = Convert.ToBase64String(encoding.GetBytes(value)); }
	}
    }
    
    public class Response
    {
	[DataMember(Name = "status")]
	public string status { get; set; }	
	[DataMember(Name = "meta")]
	public string meta { get; set; } = "dummy";
	[DataMember(Name = "dataBody")]
	public string dataBody { get; set; } = "";

	/*==[ HelperMethod ]==================================*/
	[IgnoreDataMember]
	public string data {
	    get {
		if(dataBody == ""){ return ""; }
		else { return encoding.GetString(Convert.FromBase64String(dataBody)); }
	    }
	}
    }

    /*=========================================================*/

    public enum Type {
	TEXT   = 0,
	CSV    = 1,
	JSON   = 2,
	JPEG   = 3,
	PNG    = 4,
    }
    
    /*=========================================================*/
    
    public StoreData(){
    }   
    
}

public static class StoreDataExtention
{
    public static string ToContentType(this StoreData.Type type){
	string ret = "";
	switch(type){
	    case StoreData.Type.TEXT: ret = "text/plain";       break;
	    case StoreData.Type.CSV:  ret = "text/csv";         break;
	    case StoreData.Type.JSON: ret = "application/json"; break;
	    case StoreData.Type.JPEG: ret = "image/jpeg";       break;
	    case StoreData.Type.PNG:  ret = "image/png";        break;
	}
	return ret;
    }

    public static string ToExtention(this StoreData.Type type){
	string ret = "";
	switch(type){
	    case StoreData.Type.TEXT: ret = ".txt";  break;
	    case StoreData.Type.CSV:  ret = ".csv "; break;
	    case StoreData.Type.JSON: ret = ".json"; break;
	    case StoreData.Type.JPEG: ret = ".jpg";  break;
	    case StoreData.Type.PNG:  ret = ".png";  break;
	}
	return ret;
    }    
}    
}
