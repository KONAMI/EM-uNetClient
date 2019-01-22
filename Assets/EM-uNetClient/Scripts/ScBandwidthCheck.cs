using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using am;

namespace kde.tech
{
public class ScBandwidthCheck: ScBase<FlowNodeManager>
{

    public TextMeshProUGUI m_currentConfig;

    [SerializeField]
    BandwidthClient m_downloadClient;

    //[SerializeField]
    //StunPingClient m_stunPingClient;
    
    [SerializeField]
    Slider m_clientBar;
    [SerializeField]
    Slider m_serverBar;
    
    [SerializeField]
    List<Animator> m_arrow; // 仮
    int m_arrowIdx;
    int m_testLimit;

    int m_bandwidthResultAvg;
    int m_bandwidthResultMed;
    
    [SerializeField]
    TextMeshProUGUI m_timeLimitTf;
    [SerializeField]
    TextMeshProUGUI m_currentBandwidthTf;
    [SerializeField]
    TextMeshProUGUI m_progressTf;
    [SerializeField]
    TextMeshProUGUI m_testLabelTf;
    [SerializeField]
    TextMeshProUGUI m_stepLabelTf;

    [SerializeField]
    GameObject m_delayBarPf;

    enum TestStep {
	INIT,
	TERM,
	BANDWIDTH,
	DELAY,
	SAVE	
    }
    TestStep m_step;
    
    protected async void Start(){
	

	m_lastMetricLen = 0;
	m_arrowIdx = 0;
	m_bandwidthResultAvg = -1;
	m_bandwidthResultMed = -1;
	m_step = TestStep.INIT;
	
	//m_arrowAnim = m_arrow.gameObject.GetComponent<Animator>();	
	//m_currentConfig.text = "[Test Config]\n- Duration: " + bCTX.duration.ToString() + " sec";
	
	//base.Start();

	for(;;){
	    while(m_step == TestStep.INIT){ await Task.Delay(100); }
	    
	    if(m_step == TestStep.TERM){ break; }
	    
	    switch(m_step){
		case TestStep.BANDWIDTH:
		    await BandWidthTest(); break;
	    }	    
	}
    }

    async Task BandWidthTest(){
	var bCTX = system.enpManager.bandwitdhTestCTX;
	m_testLimit = bCTX.duration;
	m_timeLimitTf.text = "|   残り " + m_testLimit.ToString() + " 秒";
	
	CountDown(m_downloadClient.Abort);
	while(m_testLimit > 0){
	    m_downloadClient.progressEvents.AddListener(DownloadProgressHandler);
	    var url = system.staticConfig.apiInfo.list.FirstOrDefault(p => p.name == "bandwidth").url;	    
	    var downloadRet = await m_downloadClient.Get(url);
	    if(downloadRet == BandwidthClient.Status.E_OK){
		m_bandwidthResultAvg = m_downloadClient.profile.avg;
		m_bandwidthResultMed = m_downloadClient.profile.median;
		UnityEngine.Debug.Log("AVG : " + m_bandwidthResultAvg.ToString() + " kbps");
		UnityEngine.Debug.Log("MED : " + m_bandwidthResultMed.ToString() + " kbps");
	    }
	    else {
		break;
	    }
	}
	    
	flowNode.RecieveTriggerHandler("BandwidthTestComplete");	
    }

    async Task SaveTest(){
	var sCTX = system.enpManager.saveCTX;
	await Task.Delay(1000);
	flowNode.RecieveTriggerHandler("SaveTestComplete");		
    }
    
    protected void OnDisable(){
	m_downloadClient.Abort();
	//m_stunPingClient.Abort();
	m_step = TestStep.TERM;
    }
    
    async Task CountDown(Action fCompleteCallback = null){
	while(m_testLimit > 0){
	    await Task.Delay(1000);
	    --m_testLimit;
	    m_timeLimitTf.text = "|   残り " + m_testLimit.ToString() + " 秒";
	}
	if(fCompleteCallback != null)
	    fCompleteCallback();
	
	return;
    }

    int m_lastMetricLen;
    
    void DownloadProgressHandler(int contentIdx, int contentTotal, float currentProgress){	
	//UnityEngine.Debug.Log(currentProgress.ToString());
	m_clientBar.value = currentProgress * 100;
	m_serverBar.value = (1-currentProgress) * 100;

	var progress = (int)m_clientBar.value;
	m_progressTf.text = progress.ToString() + " %   |";
	
	if(m_downloadClient.profile.currentMetricNr == m_lastMetricLen){ return; }

	m_lastMetricLen = m_downloadClient.profile.currentMetricNr;
	
	m_arrow[m_arrowIdx % m_arrow.Count].SetTrigger("Move");
	m_arrowIdx++;
	
	var metric = m_downloadClient.profile.lastMetric;
	if(metric != null){
	    m_currentBandwidthTf.text = metric.kbps.ToString() + " kbps";
	    //UnityEngine.Debug.Log(metric.deltaMsec.ToString() + " : " + metric.bytes.ToString() + " : " + metric.kbps.ToString() + " kbps");
	}	
    }
	
    [Preserve]
    public void StartBandwidthTest(FlowEvent.Data data){ m_step = TestStep.BANDWIDTH; }

    [Preserve]
    public void TermTest(FlowEvent.Data data){ m_step = TestStep.TERM; }
    
}
}
