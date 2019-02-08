using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using am;

namespace kde.tech
{
public class ScTestSetting: ScBase<ScTestSettingFlow>
{
    
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
	
	await StartFlow();
    }
    
}
}
