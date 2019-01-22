using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Utf8Json;

namespace kde.tech
{

public class UserData
{
    /*=========================================================*/
	
    // S3に格納されるJSON
    // Login時に必要：guid
    // 通常API時に必要 : uid + sessionCode
    public class Profile
    {
	[DataMember(Name = "guid")]
	public string guid { get; set; } = "";
	[DataMember(Name = "uid")]
	public string uid { get; set; } = "";
	[DataMember(Name = "name")]
	public string name { get; set; } = "";
	[DataMember(Name = "ctime")]
	public int ctime { get; set; } = 0;
	[DataMember(Name = "mtime")]
	public int mtime { get; set; } = 0;
    }

    public Profile profile { get; set; }
    
    public UserData(string guid = "", string uid = ""){
	profile = new Profile();
	if(guid != ""){
	    profile.guid = guid;
	    profile.uid  = ConvertGuid2Uid(profile.guid);	    
	}
	else if(uid != ""){
	    profile.uid  = uid;	    
	}
    }

    public static string GetS3PathFromGuid(string s3DirObject, string guid){
	return s3DirObject + UserData.ConvertGuid2Uid(guid) + ".json"; 
    }
    public static string GetS3PathFromUid(string s3DirObject, string uid){
	return s3DirObject + uid + ".json";  
    }
    
    public static UserData CreateNewUser(){
	var userData = new UserData();
	userData.profile.name   = "プレイヤー";
	userData.profile.guid   = Guid.NewGuid().ToString("N");
	userData.profile.uid    = ConvertGuid2Uid(userData.profile.guid);
	userData.profile.ctime  = (int)AmUtils.timestamp;
	userData.profile.mtime  = userData.profile.ctime;
	return userData;
    }

    public static string ConvertGuid2Uid(string guid){
	byte[] input        = Encoding.ASCII.GetBytes(guid);
	SHA256 sha          = new SHA256CryptoServiceProvider();
	byte[] hash_sha256  = sha.ComputeHash(input);
	string base64       = Convert.ToBase64String(hash_sha256).TrimEnd('=').Replace('+','-').Replace('/','_');
	string ret          = base64.Substring(0,12);
	return ret;
    }
    
    public string Export(){
	return JsonSerializer.ToJsonString(profile);
    }
    public void Import(string src){
	profile = JsonSerializer.Deserialize<Profile>(src);
    }

    /*======================================================================================*/

    public class Session {
	
	Profile m_profile;
	string  m_sessCode;
	int     m_ttlSec;
	
	public Session(Profile profile, string sessCode = ""){
	    m_profile  = profile;
	    m_ttlSec   = 60 * 60 * 24; 
	    if(sessCode == ""){
		SHA256 sha      = new SHA256CryptoServiceProvider();
		var seed        = Guid.NewGuid().ToString("N");
		byte[] input    = Encoding.ASCII.GetBytes(seed);
		var hash_sha256 = sha.ComputeHash(input);
		var base64      = Convert.ToBase64String(hash_sha256).TrimEnd('=').Replace('+','-').Replace('/','_');
		m_sessCode      = base64.Substring(0,16);		
	    }
	    else {
		m_sessCode = sessCode;
	    }
	}

	public int    ttlSec { get { return m_ttlSec; }}	
	public string sessCode { get { return m_sessCode; }}	
	public string keyField { get { return "sess:" + m_profile.uid; }}	
	public string valueField { get { return m_sessCode; }}
    }
    
    public class TakeoverCode {

	Profile m_profile;
	string  m_takeoverCode;
	int     m_ttlSec;

	public TakeoverCode(Profile profile, string takeoverCode = ""){
	    m_profile  = profile;
	    m_ttlSec   = 60 * 60 * 12; 
	    if(takeoverCode == ""){
		SHA256 sha      = new SHA256CryptoServiceProvider();
		var seed        = Guid.NewGuid().ToString("N");
		byte[] input    = Encoding.ASCII.GetBytes(seed);
		var hash_sha256 = sha.ComputeHash(input);
		var base64      = Convert.ToBase64String(hash_sha256).TrimEnd('=').Replace('+','-').Replace('/','_');
		m_takeoverCode  = base64.Substring(0,12);
	    }
	    else {
		m_takeoverCode = takeoverCode;
	    }
	}

	public int    ttlSec { get { return m_ttlSec; }}	
	public string takeoverCode { get { return m_takeoverCode; }}
	public string keyField { get { return "takeover:" + m_takeoverCode + ":" + m_profile.uid; }}
	public string valueField { get { return m_profile.guid; }}
    }
    
}
}
