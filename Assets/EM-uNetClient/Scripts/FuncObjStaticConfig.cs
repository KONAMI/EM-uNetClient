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
using System.Net.Http;
using Utf8Json;

namespace kde.tech
{

[Serializable, CreateAssetMenu( fileName = "FuncObjStaticConfig", menuName = "FuncObj/StaticConfig", order = 1500 )]
public class FuncObjStaticConfig : FuncObjBase
{

    public enum ApiEnv {
	DEV,
	RELEASE
    }

    public ApiEnv currentApiEnv;
    public bool isDebugMode;
    
    [Serializable]
    public class EmbededSetting : ListNodeBase {
	public string   apiUrl;
	public ApiEnv   env;
	public int      revision;	
	public string   version;
	public string   versionLabel;
    }
    
    public List<EmbededSetting> setting = new List<EmbededSetting>();
    public EmbededSetting currentSetting {
	get {
	    EmbededSetting ret = null;
	    foreach(var node in setting){ if(node.env == currentApiEnv){ ret = node; break; }}
	    return ret;
	}
    }
    
    InfoClient m_infoClient      = null;    
    InfoData.Response m_infoResp = null;
    public bool isSuccess {
	get {
	    if((m_infoResp == null) || (m_infoResp.status != InfoError.E_OK.ToString())){ return false; }
	    return true; 
	}
    }
    public InfoData.ApiInfo apiInfo {
	get {
	    if(!isSuccess){ return null; }
	    return m_infoResp.apiInfo;
	}
    }
    public override async Task Invoke(){
	m_infoClient = new InfoClient(currentSetting.apiUrl);
	//await Task.Run(() => {   });
	var ret = await m_infoClient.LoadInfo();
	Debug.Log("InfoClient Ret : " + ret.status.ToString());
	m_infoResp = ret;	    
    }    
    
}
}
