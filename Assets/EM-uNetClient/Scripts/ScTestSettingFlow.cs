using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using TMPro;
using am;

namespace kde.tech
{
    
[Preserve]    
public class ScTestSettingFlow : FlowNodeManager
{
    
    public ScTestSetting sceneManager { get; set; }
    public UIGroupLock   uiLock       { get; set; }    
    public SimpleDialog  dialog       { get; set; }

    [SerializeField]
    List<FocusedButtonGroup> m_fields;

    void Start(){
	
	var bandwidthDwProfile = sceneManager.system.bandwidthDwProfile;
	var delayProfile = sceneManager.system.delayProfile;
	
	foreach(var field in m_fields){
	    switch(field.key){
		case "BandwidthTestDuration":
		    field.SetDefaultFocus(bandwidthDwProfile.duration);
		    break;
		case "BandwidthDataSize":
		    field.SetDefaultFocus(bandwidthDwProfile.datSize);
		    break;
		case "DelayTestDuration":
		    field.SetDefaultFocus(delayProfile.duration);
		    break;
		case "DelayTestPps":
		    field.SetDefaultFocus(delayProfile.pps);
		    break;
		case "DelayTestPktSize":
		    field.SetDefaultFocus(delayProfile.pktPayloadSize + (int)UDPingClient.HEADER_SIZE);
		    break;
	    }
	}
	
    }
    
    /*======================================================================================*/

    [Preserve]
    public void UnlockUI(FlowEvent.Data data){
	uiLock.UnLock();
    }

    [Preserve]
    public void SetFocusedValueToCTX(FlowEvent.Data data){

	var bandwidthDwProfile = sceneManager.system.bandwidthDwProfile;
	var delayProfile = sceneManager.system.delayProfile;
	
	foreach(var field in m_fields){
	    switch(field.key){
		case "BandwidthTestDuration":
		    bandwidthDwProfile.duration = field.focusedValue.num;
		    break;
		case "BandwidthDataSize":
		    bandwidthDwProfile.datSize = field.focusedValue.str;
		    break;
		case "DelayTestDuration":
		    delayProfile.duration = field.focusedValue.num;
		    break;
		case "DelayTestPps":
		    delayProfile.pps = field.focusedValue.num;
		    break;
		case "DelayTestPktSize":
		    delayProfile.pktPayloadSize = field.focusedValue.num - (int)UDPingClient.HEADER_SIZE;
		    break;
	    }
	}	
    }
    
}
}
