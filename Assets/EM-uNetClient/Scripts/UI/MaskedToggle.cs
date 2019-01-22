using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using am;

namespace kde.tech
{
public class MaskedToggle : MonoBehaviour
{

    [SerializeField]
    GameObject m_mask;

    Toggle m_toggle;

    void Awake(){
	m_toggle = gameObject.GetComponent<Toggle>();	
    }

    public void OnValueChange(){
	m_mask.SetActive(!m_toggle.isOn);
    }
    
}    
}
