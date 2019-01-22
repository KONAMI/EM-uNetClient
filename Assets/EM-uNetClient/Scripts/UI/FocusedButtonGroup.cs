using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using am;

namespace kde.tech
{
public class FocusedButtonGroup : UIBtBase
{
    [Serializable]
    public class Data {
	public bool   b;
	public int    num;
	public string str;
    }
    
    [Serializable]
    public class BtDef {
	public bool    isFocused;
	public Vector2 focusPos;
	public Data    value;
    }

    [SerializeField]
    string m_key;
    public string key { get { return m_key; }}
    
    [SerializeField]
    RectTransform m_focusTf;
    
    [SerializeField]
    List<BtDef> m_btDef;

    public Data focusedValue { get { return m_btDef.FirstOrDefault((_p) => _p.isFocused).value; }}
    
    public void Awake(){
	BtDef activeBt = null;
	if(m_btDef.FirstOrDefault((_p) => _p.isFocused) == null){	    
	    if(m_btDef.Count > 0){ activeBt = m_btDef[0]; }	    	    
	}
	if(activeBt != null){
	    activeBt.isFocused = true;
	    m_focusTf.anchoredPosition = activeBt.focusPos;
	}
    }
    
    public void OnClickHandler(int idx){
	
	if((idx < 0) || (idx >= m_btDef.Count)){ return; }
	
	BtDef activeBt = m_btDef[idx];
	
	if(activeBt.isFocused){ return; }
	var prevFocued = m_btDef.FirstOrDefault((_p) => _p.isFocused);
	if(prevFocued != null){prevFocued.isFocused = false; }

	activeBt.isFocused = true;
	m_focusTf.anchoredPosition = activeBt.focusPos;
	    
    }

    
    
}    
}
