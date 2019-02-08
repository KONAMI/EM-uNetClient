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

    UDPingClient.Profile m_delayProfile = null;
    public UDPingClient.Profile delayProfile {
        get {
            if(m_delayProfile == null){ m_delayProfile = new UDPingClient.Profile(); }
            return m_delayProfile;
        }
	set {
	    m_delayProfile = value;
	}
    }

    BandwidthClient.Profile m_bandwidthDwProfile = null;
    public BandwidthClient.Profile bandwidthDwProfile {
        get {
            if(m_bandwidthDwProfile == null){ m_bandwidthDwProfile = new BandwidthClient.Profile(); }
            return m_bandwidthDwProfile;
        }
	set {
	    m_bandwidthDwProfile = value;
	}
    }

    BandwidthClient.Profile m_bandwidthUpProfile = null;
    public BandwidthClient.Profile bandwidthUpProfile {
        get {
            if(m_bandwidthUpProfile == null){ m_bandwidthUpProfile = new BandwidthClient.Profile(); }
            return m_bandwidthUpProfile;
        }
	set {
	    m_bandwidthUpProfile = value;
	}
    }
    
    void Awake(){

	DontDestroyOnLoad(gameObject);
	
	{
#if UNITY_ANDROID
	    float originRatio  = 16f / 9f;		    
	    float currentRatio = (float)Screen.width / (float)Screen.height;
	    int   width        = 1024;
	    int   height       = 576;
	    if(currentRatio > originRatio){ width  = (int)(1024 * (currentRatio / originRatio)); }
	    else                          { height = (int)(576 * (originRatio / currentRatio)); }
	    Screen.SetResolution(width, height, true);	    
#else
	    Screen.fullScreen = false;
	    Screen.SetResolution(1024, 576, false);
#endif
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

