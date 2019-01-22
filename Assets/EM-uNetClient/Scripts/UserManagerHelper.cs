using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;
using Utf8Json;
using am;

namespace kde.tech
{    

// SystemManagerに付与しておく
public class UserManagerHelper : MonoBehaviour
{
    static readonly string LOCAL_STORE_GUID_KEY = "PLAYER_GUID";
    static readonly string LOCAL_STORE_UID_KEY  = "PLAYER_UID";
    static readonly string LOCAL_STORE_NAME_KEY = "PLAYER_NAME";
    static readonly string LOCAL_STORE_SESS_KEY = "PLAYER_SESS"; // debug
    
    [SerializeField]
    FuncObjUserManager m_userManagerPf;
    FuncObjUserManager m_userManager = null;
    public FuncObjUserManager userManager {
        get {
            if(m_userManager == null){ m_userManager = Instantiate(m_userManagerPf) as FuncObjUserManager; }
            return m_userManager;
        }
    }

    void Awake(){
	userManager.guid = PlayerPrefs.GetString(LOCAL_STORE_GUID_KEY, "");
	userManager.uid  = PlayerPrefs.GetString(LOCAL_STORE_UID_KEY, "");
	userManager.name = PlayerPrefs.GetString(LOCAL_STORE_NAME_KEY, "");
	userManager.sessCode = PlayerPrefs.GetString(LOCAL_STORE_SESS_KEY, "");
	Debug.Log("Check Local Store");
	Debug.Log(userManager.userProfile.guid);
	Debug.Log(userManager.userProfile.uid);
	Debug.Log(userManager.userProfile.name);
	Debug.Log(userManager.sessCode);
    }
    
    public Task<AuthError> Login(){
	return (userManager.isNewUser) ? userManager.Regist() : userManager.Login();
    }
    
    public void SaveProfile(string guid, string uid, string name, string sessCode){
	PlayerPrefs.SetString(LOCAL_STORE_GUID_KEY, guid);
	PlayerPrefs.SetString(LOCAL_STORE_UID_KEY, uid);
	PlayerPrefs.SetString(LOCAL_STORE_NAME_KEY, name);
	PlayerPrefs.SetString(LOCAL_STORE_SESS_KEY, sessCode);
	PlayerPrefs.Save();
    }

    public void ResetProfile(){
	PlayerPrefs.SetString(LOCAL_STORE_GUID_KEY, "");
	PlayerPrefs.SetString(LOCAL_STORE_UID_KEY, "");
	PlayerPrefs.SetString(LOCAL_STORE_NAME_KEY, "");
	PlayerPrefs.SetString(LOCAL_STORE_SESS_KEY, "");
	PlayerPrefs.Save();
	userManager.userProfile = new UserData.Profile(); // すでにある情報もクリア
    }
    
}
}
