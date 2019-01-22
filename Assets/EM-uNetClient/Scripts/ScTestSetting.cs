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
public class ScTestSetting: ScBase<FlowNodeManager>
{
    [SerializeField]
    List<FocusedButtonGroup> m_fields;

    [Preserve]
    public void SetFocusedValueToCTX(FlowEvent.Data data){

	Debug.Log("SetFocusedValueToCTX");
	
	var bCTX = system.enpManager.bandwitdhTestCTX;
	var dCTX = system.enpManager.delayTestCTX;
	var sCTX = system.enpManager.saveCTX;
	
	foreach(var field in m_fields){
	    switch(field.key){
		case "BandwidthTestDuration": bCTX.duration = field.focusedValue.num; break;
		case "DelayTestDuration":     dCTX.duration = field.focusedValue.num; break;
		case "DelayTestPps":          dCTX.pps      = field.focusedValue.num; break;
		case "DelayTestPktSize":      dCTX.pktSize  = field.focusedValue.num; break;
		case "SaveConfigLocation":
		    sCTX.isSaveLocal  = true;
		    sCTX.isSaveRemote = (field.focusedValue.num == 1) ? true : false;
		    break;
		case "SaveConfigUseGps":      sCTX.isSaveAuto = field.focusedValue.b; break;
		case "SaveConfigIsSaveAuto":  sCTX.isSaveAuto = field.focusedValue.b; break; 
	    }
	}
    }
    
}
}
