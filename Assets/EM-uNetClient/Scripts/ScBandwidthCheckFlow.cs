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

[Preserve]    
public class ScBandwidthCheckFlow: FlowNodeManager
{

    public ScBandwidthCheck sceneManager { get; set; }
    public UIGroupLock      uiLock       { get; set; }

    /*==========================================================================================*/
    
    [SerializeField]
    BandwidthClient m_downloadClient;
    [SerializeField]
    BandwidthClient m_uploadClient;

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
    TextMeshProUGUI m_settingTf;
    
    /*==========================================================================================*/
    
    public class Bar {
	public RectTransform rt;
	public Image         img;	
	public void Apply(int kbps){
	    uint color = 0xFFFFFFFF;
	    if(kbps < 200){
		color = 0xFFFF0000;
		s_size.y = 50f * ((float)kbps / 200f);
	    }
	    else if(kbps < 500){
		color = 0xFFFFFF00;
		s_size.y = 50f + 50f * (((float)kbps - 200f) / 300f);
	    }
	    else if(kbps < 1024){
		color = 0xFF00FF00;
		s_size.y = 100f + 50f * (((float)kbps - 500f) / 500f);
	    }
	    else if(kbps < 10240){
		color = 0xFF0000FF;
		s_size.y = 150f + 150f * (((float)kbps - 1024f) / 9216f);
	    }
	    else {
		color = 0xFF0000FF;
		s_size.y = 300f;
	    }
	    img.SetColor(color);
	    rt.sizeDelta = s_size;
	}
    }

    static readonly int MONITOR_WIDTH = 720;

    static Vector2 s_pos  = new Vector2(0,0);
    static Vector2 s_size = new Vector2(1,1);
    
    List<Bar>    m_barList;
    int          m_barNr;
    
    [SerializeField]
    GameObject   m_graph;
    [SerializeField]
    GameObject   m_barPf;
    
    void Start(){
	m_lastMetricLen = 0;
	m_arrowIdx = 0;
	m_bandwidthResultAvg = -1;
	m_bandwidthResultMed = -1;

	m_settingTf.text = "Test: Time " + sceneManager.system.bandwidthDwProfile.duration.ToString() + " sec - Size " + sceneManager.system.bandwidthDwProfile.datSize + "B";
	
	m_barList      = new List<Bar>();
	m_barNr        = MONITOR_WIDTH / 60 * 10;
	float interval = (float)MONITOR_WIDTH / (float)m_barNr;
	int   idx = 0;
	s_size.x = interval;
	s_pos.y  = 0;
	for(idx = 0; idx < m_barNr; ++idx){
	    var bar = new Bar();
	    var go  = m_barPf.InstantiateWithOrigName();
	    go.SetUIParent(m_graph);
	    bar.rt  = go.GetComponent<RectTransform>();
	    bar.img = go.GetComponent<Image>();
	    
	    s_pos.x = interval * idx;
	    bar.rt.anchoredPosition = s_pos;
	    m_barList.Add(bar);
	}	
    }

    async Task BandWidthDwTest(){
	m_testLimit = sceneManager.system.bandwidthDwProfile.duration;
	m_timeLimitTf.text = "|   残り " + m_testLimit.ToString() + " 秒";
	
	var countTask = CountDown(m_downloadClient.Abort);
	
	m_downloadClient.progressEvents.AddListener(DownloadProgressHandler);
	var url = sceneManager.system.staticConfig.apiInfo.list.FirstOrDefault(p => p.name == "bandwidth").url + "/" + sceneManager.system.bandwidthDwProfile.datSize + ".dat";
	var downloadRet = await m_downloadClient.Get(url);
	if(downloadRet == BandwidthClient.Status.E_OK){
	    m_bandwidthResultAvg = m_downloadClient.profile.avg;
	    m_bandwidthResultMed = m_downloadClient.profile.median;
	    UnityEngine.Debug.Log("AVG : " + m_bandwidthResultAvg.ToString() + " kbps");
	    UnityEngine.Debug.Log("MED : " + m_bandwidthResultMed.ToString() + " kbps");
	}
	m_testLimit = 0;
	await countTask;
    }
    async Task BandWidthUpTest(){
	m_testLimit = sceneManager.system.bandwidthUpProfile.duration;
	m_timeLimitTf.text = "|   残り " + m_testLimit.ToString() + " 秒";
	
	var countTask = CountDown(m_uploadClient.Abort);
	
	m_uploadClient.progressEvents.AddListener(UploadProgressHandler);
	var url = sceneManager.system.staticConfig.apiInfo.list.FirstOrDefault(p => p.name == "bandwidth").url + "/upload.php";
	var uploadRet = await m_uploadClient.Post(url, sceneManager.system.bandwidthUpProfile.datSize);
	if(uploadRet == BandwidthClient.Status.E_OK){
	    m_bandwidthResultAvg = m_uploadClient.profile.avg;
	    m_bandwidthResultMed = m_uploadClient.profile.median;
	    UnityEngine.Debug.Log("AVG : " + m_bandwidthResultAvg.ToString() + " kbps");
	    UnityEngine.Debug.Log("MED : " + m_bandwidthResultMed.ToString() + " kbps");
	}

	await countTask;
    }

    protected void OnDisable(){
	m_downloadClient.Abort();
	m_uploadClient.Abort();
    }

    public void Abort(){
	m_downloadClient.Abort();
	m_uploadClient.Abort();
	m_testLimit = 0;
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
    
    void DownloadProgressHandler(int metricSeq, float currentProgress){
	
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

	    var bar = m_barList[metric.seq % m_barNr];
	    bar.Apply(metric.kbps);
	}	
    }

    void UploadProgressHandler(int metricSeq, float currentProgress){
	
	m_clientBar.value = currentProgress * 100;
	m_serverBar.value = (1-currentProgress) * 100;

	var progress = (int)m_clientBar.value;
	m_progressTf.text = progress.ToString() + " %   |";
	
	if(m_uploadClient.profile.currentMetricNr == m_lastMetricLen){ return; }

	m_lastMetricLen = m_uploadClient.profile.currentMetricNr;
	
	m_arrow[m_arrowIdx % m_arrow.Count].SetTrigger("Move");
	m_arrowIdx++;
	
	var metric = m_uploadClient.profile.lastMetric;
	if(metric != null){
	    m_currentBandwidthTf.text = metric.kbps.ToString() + " kbps";
	    //UnityEngine.Debug.Log(metric.deltaMsec.ToString() + " : " + metric.bytes.ToString() + " : " + metric.kbps.ToString() + " kbps");

	    var bar = m_barList[metric.seq % m_barNr];
	    bar.Apply(metric.kbps);
	}	
    }
    
    [Preserve]
    public void StartBandwidthDwTest(FlowEvent.Data data){
	data.task = BandWidthDwTest();
    }
    [Preserve]
    public void StartBandwidthUpTest(FlowEvent.Data data){
	data.task = BandWidthUpTest();
    }
    [Preserve]
    public void CopyProfile(FlowEvent.Data data){
	if(m_downloadClient != null){
	    if(m_downloadClient.profile.status == BandwidthClient.Status.E_OK){
		sceneManager.system.bandwidthDwProfile = m_downloadClient.profile;
	    }
	}
    }
    

}
}
