using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using am;

namespace kde.tech
{
    
public class ScMenu: ScBase<ScMenuFlow>
{

    [SerializeField]
    TextMeshProUGUI m_bandwidthSummaryTf;
    [SerializeField]
    TextMeshProUGUI m_delaySummaryTf;
    
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
	
	if(system.delayProfile.status == UDPingClient.Status.E_OK){
	    m_delaySummaryTf.text = "RTT AVG " + system.delayProfile.avg + " msec";
	}
	else {
	    m_delaySummaryTf.text = "RTT AVG - msec";	    
	}
	
	if(system.bandwidthDwProfile.status == BandwidthClient.Status.E_OK){
	    m_bandwidthSummaryTf.text = "DW AVG " + system.bandwidthDwProfile.avg + " kbps";
	}
	else {
	    m_bandwidthSummaryTf.text = "DW AVG - kbps";
	}
	
	await StartFlow();
    }
    
}
}
