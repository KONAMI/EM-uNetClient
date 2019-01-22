using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace kde.tech
{
public class UIGroupLock : MonoBehaviour
{

    bool m_lockObj = true;

    public bool TryLock(){
	if(m_lockObj){ return false; }
	m_lockObj = true;
	return true;
    }

    public void UnLock(){
	m_lockObj = false;
    }

    public IEnumerator UnLock(float delayedSec){
	yield return new WaitForSeconds(delayedSec);
	UnLock();
    }
}    
}
