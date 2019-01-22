using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Utf8Json;
using am;

namespace kde.tech
{
    
public class SystemManager : MonoBehaviour
{

    ENPManager m_enpManager;
    public ENPManager enpManager { get { return m_enpManager; }}

    UserManagerHelper m_userManagerHelper;
    public UserManagerHelper userManagerHelper { get {
	    if(m_userManagerHelper == null){ m_userManagerHelper = GetComponent<UserManagerHelper>(); }
	    return m_userManagerHelper;
	}}
    public FuncObjUserManager userManager { get { return userManagerHelper.userManager; }}
    
    [SerializeField]
    FuncObjStaticConfig m_staticConfigPf;
    FuncObjStaticConfig m_staticConfig = null;
    public FuncObjStaticConfig staticConfig {
        get {
            if(m_staticConfig == null){ m_staticConfig = Instantiate(m_staticConfigPf) as FuncObjStaticConfig; }
            return m_staticConfig;
        }
    }
    
    void Awake(){

	DontDestroyOnLoad(gameObject);
	
	{
#if UNITY_ANDROID     
	    Screen.SetResolution(1024, 576, true);
#else
	    Screen.fullScreen = false;
	    Screen.SetResolution(1024, 576, false);
#endif
	}

	if(m_enpManager == null){
	    var pf = Resources.Load("Prefabs/ENPManager") as GameObject;
	    var go = Instantiate(pf) as GameObject;
	    go.name = pf.name;
	    go.transform.parent = gameObject.transform;
	    m_enpManager = go.GetComponent<ENPManager>();
	}

#if ENABLE_IL2CPP
	Utf8Json.Resolvers.CompositeResolver.RegisterAndSetAsDefault
	    (
	     Utf8Json.Resolvers.GeneratedResolver.Instance
	     ,Utf8Json.Resolvers.BuiltinResolver.Instance
	     ,Utf8Json.Unity.UnityResolver.Instance
	     );
#endif
	
    }
}
}

