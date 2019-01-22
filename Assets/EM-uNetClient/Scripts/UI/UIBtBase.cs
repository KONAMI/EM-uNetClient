using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace kde.tech
{
public class UIBtBase : MonoBehaviour
{

    [SerializeField]
    protected UIGroupLock m_lock;

    protected bool TryLock(){
	if(m_lock == null){ return true; }
	return m_lock.TryLock();
    }

    protected void UnLock(){
	if(m_lock != null){ m_lock.UnLock(); }
    }

    protected void UnLock(float delayedSec){
	if(m_lock != null){ StartCoroutine(m_lock.UnLock(delayedSec)); }
    }
    
    protected virtual void IgnoreUIActionHandler(){}
    
}    
}
