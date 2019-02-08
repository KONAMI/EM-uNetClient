using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using am;
using Utf8Json;
using TMPro;

namespace kde.tech
{
public class ScTitle : ScBase<ScTitleFlow>
{

    [SerializeField]
    TextMeshProUGUI m_uidTf;
    [SerializeField]
    TextMeshProUGUI m_versionTf;
    
    protected override void Awake(){
	base.Awake();
	if(flowNodeManager != null){
	    flowNodeManager.sceneManager = this;
	    flowNodeManager.uiLock       = gameObject.GetComponent<UIGroupLock>();
	    flowNodeManager.dialog       = m_dialog;
	}	
    }
    
    protected async void Start(){

	FixCameraViewport();
	
	m_uidTf.text = "uid : " + system.userManager.userProfile.uid;
	m_versionTf.text = "version : " + Application.version;
	await StartFlow();
    }

}
}
