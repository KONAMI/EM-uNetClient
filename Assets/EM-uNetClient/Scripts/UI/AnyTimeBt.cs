using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using am;

namespace kde.tech
{

public class AnyTimeBt : UIBtBase
{
    [SerializeField]
    string m_flowTriggerKey;
    [SerializeField]
    string m_flowParamKey;
    
    Animator m_animator;
    Button   m_button;

    FlowNodeManager  m_flowManager;

    int m_releaseSec;
    
    void Start(){
	m_releaseSec   = -1;
	m_animator     = gameObject.GetComponent<Animator>();
	m_button       = gameObject.GetComponent<Button>();
	m_flowManager = GameObject.Find("FlowManager").GetComponent<FlowNodeManager>();
	if(!m_button.interactable){ m_animator.Play("Disabled"); }
    }

    public void OnClickHandler(){
	if(!TryLock()){ IgnoreUIActionHandler(); return; }
	m_animator.Play("Pressed");
	//m_button.interactable = false;
	m_flowManager.flowNode.RecieveTriggerHandler(m_flowTriggerKey);
	//UnLock(0.5f);
    }
    
    public void OnClickHandler(string value){
	if(!TryLock()){ IgnoreUIActionHandler(); return; }
	m_animator.Play("Pressed");
	//m_button.interactable = false;
	m_flowManager.flowNode.RecieveParamHandler(m_flowParamKey, value);
	m_flowManager.flowNode.RecieveTriggerHandler(m_flowTriggerKey);
	//UnLock(0.5f);
    }

    public void OnClickHandler(bool value){
	if(!TryLock()){ IgnoreUIActionHandler(); return; }
	m_animator.Play("Pressed");
	//m_button.interactable = false;
	m_flowManager.flowNode.RecieveParamHandler(m_flowParamKey, value);
	m_flowManager.flowNode.RecieveTriggerHandler(m_flowTriggerKey);
	//UnLock(0.5f);
    }
    
    public void OnClickHandler(int value){
	if(!TryLock()){ IgnoreUIActionHandler(); return; }
	m_animator.Play("Pressed");
	//m_button.interactable = false;
	m_flowManager.flowNode.RecieveParamHandler(m_flowParamKey, value);
	m_flowManager.flowNode.RecieveTriggerHandler(m_flowTriggerKey);
	//UnLock(0.5f);
    }

    public void OnClickAbortHandler(){
	if(!TryLock()){ IgnoreUIActionHandler(); return; }
	m_animator.Play("Pressed");
	//m_button.interactable = false;
	m_flowManager.flowNode.RecieveAbortHandler();
	//UnLock(0.5f);
    }
    
}
}
