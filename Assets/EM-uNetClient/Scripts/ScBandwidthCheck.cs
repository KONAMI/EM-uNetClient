using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using am;

namespace kde.tech
{
public class ScBandwidthCheck: ScBase<ScBandwidthCheckFlow>
{
    
    protected override void Awake(){
	base.Awake();
	if(flowNodeManager != null){
	    flowNodeManager.sceneManager = this;
	    flowNodeManager.uiLock       = gameObject.GetComponent<UIGroupLock>();
	}	
    }
    
    protected async void Start(){
	
	FixCameraViewport();
	
	await StartFlow();
    }    
}
}
