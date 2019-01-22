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
public class ScMenuFlow : FlowNodeManager
{
    
    public ScMenu       sceneManager { get; set; }
    public UIGroupLock  uiLock       { get; set; }    
    public SimpleDialog dialog       { get; set; }
    
    /*======================================================================================*/

    [Preserve]
    public void UnlockUI(FlowEvent.Data data){
	uiLock.UnLock();
    }
    
}
}
