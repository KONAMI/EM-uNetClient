using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;
using Utf8Json;

namespace kde.tech
{

[Serializable, CreateAssetMenu( fileName = "FuncObjUserManager", menuName = "FuncObj/UserManager", order = 1500 )]
public class FuncObjUserManager : FuncObjBase
{

    public FuncObjStaticConfig config { get; set; }
    public UserData.Profile      userProfile { get; set; } = new UserData.Profile();
    public string                sessCode { get; set; } = "";
    public AuthError             errno { get; set; } = AuthError.E_CHAOS; 
    public string guid { set { userProfile.guid = value; }}
    public string uid { set { userProfile.uid = value; }}
    public string name { set { userProfile.name = value; }}
    public bool isNewUser { get { return (userProfile.guid == "") ? true : false; }}

    public async Task<AuthError> Login(){
	var ret        = AuthError.E_CHAOS;
	var authClient = new AuthClient(config.apiInfo.list.FirstOrDefault(p => p.name == "auth").url,
					config.apiInfo.key);
	var resp       = await authClient.Login(userProfile.guid);
	errno = (AuthError)Enum.Parse(typeof(AuthError), resp.status, true);

	Debug.Log("Auth::Login : " + resp.status + " [ " +  resp.meta + " ]");
	
	if(errno == AuthError.E_OK){
	    sessCode = resp.sessCode;
	    userProfile = resp.userProfile;
	}

	ret = errno;
	
	return ret;	
    }

    public async Task<AuthError> Regist(){
	var ret        = AuthError.E_CHAOS;
	var authClient = new AuthClient(config.apiInfo.list.FirstOrDefault(p => p.name == "auth").url,
					config.apiInfo.key);
	var resp       = await authClient.Regist();
	errno = (AuthError)Enum.Parse(typeof(AuthError), resp.status, true);

	Debug.Log("Auth::Regist : " + resp.status + " [ " +  resp.meta + " ]");
	
	if(errno == AuthError.E_OK){
	    sessCode = resp.sessCode;
	    userProfile = resp.userProfile;
	}

	ret = errno;
	
	return ret;	
    }

    // Dummy
    public override async Task Invoke(){	
	await Task.Run(() => {});
    }    
    
}
}
