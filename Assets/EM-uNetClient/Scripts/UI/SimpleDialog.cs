using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace kde.tech
{

public class SimpleDialog :  MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI m_titleTf;    
    [SerializeField]
    TextMeshProUGUI m_msgTf;
    [SerializeField]
    TextMeshProUGUI m_okBtTf;

    [SerializeField]
    GameObject m_loadingObj; 

    [SerializeField]
    GameObject m_okBt;
    [SerializeField]
    Animator m_okBtAnimator;
    Action m_fOkBtHandler;
    [SerializeField]
    GameObject m_yesBt;
    [SerializeField]
    Animator m_yesBtAnimator;
    Action m_fYesBtHandler;
    [SerializeField]
    GameObject m_noBt;
    [SerializeField]
    Animator m_noBtAnimator;    
    Action m_fNoBtHandler;
   
    public void SetModeOk(Action fOkBtHandler, string btLabel = ""){
	m_okBt.gameObject.SetActive(true);
	m_yesBt.gameObject.SetActive(false);
	m_noBt.gameObject.SetActive(false);
	m_fOkBtHandler = fOkBtHandler;
	if(btLabel != ""){ m_okBtTf.text = btLabel; }
    }
    public void SetModeYesNo(Action fYesBtHandler, Action fNoBtHandler){
	m_okBt.gameObject.SetActive(false);
	m_yesBt.gameObject.SetActive(true);
	m_noBt.gameObject.SetActive(true);
	m_fYesBtHandler = fYesBtHandler;
	m_fNoBtHandler = fNoBtHandler;
    }
    public void SetTitle(string title){ m_titleTf.text = title; }
    public void SetMsg(string msg){ m_msgTf.text = msg; }
    public void SetOkBtLabel(string label){ m_okBtTf.text = label; }
    public void SetLoading(bool isEnable){
	m_loadingObj.SetActive(isEnable);
	if(isEnable){
	    m_okBt.gameObject.SetActive(false);
	    m_yesBt.gameObject.SetActive(false);
	    m_noBt.gameObject.SetActive(false);	    
	}
    }
    
    public void BtOkHandler(){
	m_okBtAnimator.Play("Pressed");
	if(m_fOkBtHandler != null){	    
	    m_fOkBtHandler();
	    m_fOkBtHandler = null;
	}
    }
    public void BtYesHandler(){
	m_yesBtAnimator.Play("Pressed");
	if(m_fYesBtHandler != null){
	    m_fYesBtHandler();
	    m_fYesBtHandler = null;
	}
    }
    public void BtNoHandler(){
	m_noBtAnimator.Play("Pressed");	
	if(m_fNoBtHandler != null){
	    m_fNoBtHandler();
	    m_fNoBtHandler = null;
	}
    }
    
}
}
