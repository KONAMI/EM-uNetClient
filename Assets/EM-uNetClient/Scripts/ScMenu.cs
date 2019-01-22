using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using am;

namespace kde.tech
{
    
public class ScMenu: ScBase<ScMenuFlow>
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
	await StartFlow();
    }
    
}
}
