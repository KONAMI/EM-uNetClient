using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using am;

namespace kde.tech
{

/// <summary>
///   EM-uNetPi Client データ/ステート管理用の Managerクラス
/// </summary>
public class ENPManager : MonoBehaviour, IFlowEventHandler
{

    public class BandwidthTestCTX {
	/// <summary>テストの最長計測時間</summary>
	public int duration = 30;
    }
    BandwidthTestCTX m_bandwitdhTestCTX;
    public BandwidthTestCTX bandwitdhTestCTX {
	get {
	    if(m_bandwitdhTestCTX == null){ m_bandwitdhTestCTX = new BandwidthTestCTX(); }
	    return m_bandwitdhTestCTX;
	}
    }
   
    public class DelayTestCTX {
	/// <summary>テストの最長計測時間</summary>
	public int duration = 60; 
	/// <summary>秒間あたりの計測Packet送信数</summary>
	public int pps = 10; 
	/// <summary>計測Packetのデータサイズ</summary>
	public int pktSize = 128;
    }
    DelayTestCTX m_delayTestCTX;
    public DelayTestCTX delayTestCTX {
	get {
	    if(m_delayTestCTX == null){ m_delayTestCTX = new DelayTestCTX(); }
	    return m_delayTestCTX;
	}
    }

    public class SaveCTX {
	public bool isSaveLocal     = true;
	public bool isSaveRemote    = false;
	public bool isSaveAuto      = true;
	public bool isRecordGpsInfo = true;
    }
    SaveCTX m_saveCTX;
    public SaveCTX saveCTX {
	get {
	    if(m_saveCTX == null){ m_saveCTX = new SaveCTX(); }
	    return m_saveCTX;
	}
    }

    [SerializeField]
    FlowNode m_bandwitdhTestFlowPf;
    FlowNode m_bandwitdhTestFlow;
    
    [SerializeField]
    FlowNode m_delayTestFlowPf;
    FlowNode m_delayTestFlow;

    void Awake(){
	DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    ///   初期化（計測済みのデータもここでリセットされる）
    /// </summary>
    public void Initialize(){
	m_bandwitdhTestFlow = Instantiate(m_bandwitdhTestFlowPf) as FlowNode;
	m_delayTestFlow     = Instantiate(m_delayTestFlowPf) as FlowNode;
    }

    /// <summary>
    ///   計測済みデータ、及び、計測設定を初期化する
    /// </summary>
    public void ResetCTX(){
	m_bandwitdhTestCTX  = null;
	m_delayTestCTX      = null;
	m_saveCTX           = null;	
    }

    public void OnRecieveFlowEvent(FlowEvent evt){
	Debug.Log("OnRecieveFlowEvent : " + evt.data.type.ToString());
	// リフレクションで関数を呼ぶ。
    }
    
}
}
    
