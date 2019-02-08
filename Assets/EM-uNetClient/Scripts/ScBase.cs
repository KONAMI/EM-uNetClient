using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using am;

namespace kde.tech
{
public class ScBase<FlowNodeManagerT>: MonoBehaviour where FlowNodeManagerT : FlowNodeManager
{

    protected SystemManager m_pSystem;
    public SystemManager system { get { return m_pSystem; }}

    protected FlowNodeManagerT m_pFlowNodeManager;
    public FlowNodeManagerT flowNodeManager { get { return m_pFlowNodeManager; }}
    public FlowNode flowNode { get { return (m_pFlowNodeManager == null) ? null : m_pFlowNodeManager.flowNode; }}
    public bool m_isFlowAutoStart = true;

    [SerializeField]
    protected SimpleDialog m_dialog;

    [SerializeField]
    protected Camera m_uiCamera;
    
    protected virtual void Awake(){
	{
	    var go = GameObject.Find("SystemManager");
	    if(go == null){
		var pf = Resources.Load("Prefabs/SystemManager") as GameObject;
		go = Instantiate(pf) as GameObject;
		go.name = pf.name;
	    }
	    m_pSystem = go.GetComponent<SystemManager>();
	}
	{
	    var go = GameObject.Find("FlowManager");
	    if(go != null){
		m_pFlowNodeManager = go.GetComponent<FlowNodeManagerT>();
	    }
	}
    }
    
    protected virtual async Task StartFlow(){
	if((flowNodeManager != null) && m_isFlowAutoStart){ await m_pFlowNodeManager.StartFlow(); }
    }

    protected void FixCameraViewport(){
#if UNITY_ANDROID
	if(m_uiCamera != null){
	    float targetRatio = 16f / 9f;	
	    float currentRatio = Screen.width * 1f / Screen.height;
	    float ratio = targetRatio / currentRatio;
	    if(ratio > 1f){
		float rectY = (ratio - 1.0f) / 2f;
		m_uiCamera.rect = new Rect (0f, rectY, 1f, ratio);
	    }
	    else {
		float rectX = (1.0f - ratio) / 2f;
		m_uiCamera.rect = new Rect (rectX, 0f, ratio, 1f);
	    }
	}
#endif	
    }
    
    /*
     * 継承先のSceneManagerで実装する
     * ==================================================================
     protected override void Awake(){
       base.Awake();
       if(flowNodeManager != null){
         flowNodeManager.sceneManager = this;
         flowNodeManager.uiLock       = gameObject.GetComponent<UIGroupLock>();
       }	
     }
     protected async void Start(){
       await StartFlow();
     }
     * ==================================================================
     */
    
}
}
