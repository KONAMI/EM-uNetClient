using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using TMPro;
using am;

namespace kde.tech
{
    
[Preserve]    
public class ScDelayCheckFlow : FlowNodeManager
{

    public ScDelayCheck sceneManager { get; set; }
    public UIGroupLock  uiLock       { get; set; }

    [SerializeField]
    TextMeshProUGUI m_lossTf;
    [SerializeField]
    TextMeshProUGUI m_delayTf;
    [SerializeField]
    TextMeshProUGUI m_timeTf;
    [SerializeField]
    TextMeshProUGUI m_settingTf;

    /*==========================================================================================*/
    
    public class Bar {
	public RectTransform rt;
	public Image         img;	
	public void Apply(int rtt){
	    uint color = 0xFFFFFFFF;
	    if(rtt < 0){
		color = 0xFF999999;
		s_size.y = 340f;
	    }
	    if(rtt < 100){
		color = 0xFF0000FF;
		s_size.y = 60f * ((float)rtt / 100f);
	    }
	    else if(rtt < 300){
		color = 0xFF00FF00;
		s_size.y = 60f + 120f * (((float)rtt - 100f) / 200f);
	    }
	    else if(rtt < 500){
		color = 0xFFFFFF00;
		s_size.y = 180f + 120f * (((float)rtt - 300f) / 200f);
	    }
	    else if(rtt < UDPingClient.TIMEOUT_MSEC){
		color = 0xFFFF0000;
		s_size.y = 300f + 40f * (((float)rtt - 500f) / ((float)UDPingClient.TIMEOUT_MSEC - 500f));
	    }
	    else {
		color = 0xFFFFFFFF;
		s_size.y = 340f;
	    }
	    img.SetColor(color);
	    rt.sizeDelta = s_size;
	}
    }

    static readonly int MONITOR_WIDTH = 720;
    static readonly int SEEK_BUF_TIMEOUT_FRAME = 20;

    static Vector2 s_pos  = new Vector2(0,0);
    static Vector2 s_size = new Vector2(1,1);
    
    UDPingClient m_client = null;
    byte[]       m_payload;
    List<Bar>    m_barList;
    int          m_barNr;
    
    [SerializeField]
    GameObject   m_graph;
    [SerializeField]
    GameObject   m_barPf;
	
    void Start(){

	var url = sceneManager.system.staticConfig.apiInfo.list.FirstOrDefault(p => p.name == "delay").url;
	var elm = url.Split(':');	
	m_client = new UDPingClient(elm[0], int.Parse(elm[1]));
	if(m_client.lastNetError != SocketError.Success){
	    UnityEngine.Debug.Log("DelayClient Initialize Fail.");
	    m_client.Dispose();
	    m_client = null;
	    return;
	}
	m_payload = new byte[UDPingClient.RECV_BUFFER_SIZE];

	m_client.profile.pps = sceneManager.system.delayProfile.pps;
	m_client.profile.pktPayloadSize = sceneManager.system.delayProfile.pktPayloadSize;
	m_client.profile.duration = sceneManager.system.delayProfile.duration;
	
	m_settingTf.text = "Test: " + m_client.profile.duration.ToString() + " sec - " + m_client.profile.pktSize.ToString() + "B";
	
	m_barList      = new List<Bar>();
	m_barNr        = MONITOR_WIDTH / 60 * m_client.profile.pps;
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
    
    void Update(){

	int lastRtt = 0;
	
	if(m_client != null){
	    for(;;){
		ushort pktType     = 0;
		int    seq         = -1;
		int    payloadSize = m_payload.Length;
		m_client.RecvTestPacket(ref pktType, ref seq, m_payload, ref payloadSize);
		if(m_client.lastNetError == SocketError.Success){
		    var node = m_client.profile.RecvHandler(seq);
		    if(node != null){
			var bar = m_barList[seq % m_barNr];
			bar.Apply(node.rtt);
			lastRtt = node.rtt;
		    }
		}
		else { break; }
	    }
	}
	
	{
	    int idx = 0;
	    int nr  = (UDPingClient.TIMEOUT_MSEC / 1000 * m_client.profile.pps) + SEEK_BUF_TIMEOUT_FRAME;
	    int baseSeq = m_client.profile.currentSeq;
	    
	    for(idx = 0; idx < nr; ++idx){
		var seq = baseSeq - idx;
		if(seq < 0){ break; }
		var bar = m_barList[seq % m_barNr];

		if(idx >= (nr - SEEK_BUF_TIMEOUT_FRAME)){
		    var node = m_client.profile.TimeoutHandler(seq);
		    if(node != null){
			bar.Apply(node.rtt);			
		    }
		}
		//else {}
	    }	    	    
	}

	if(lastRtt > 0){
	    m_delayTf.text = lastRtt.ToString() + " msec";
	}

	var loss = m_client.profile.loss * 100;
	if(loss > 0f){
	    m_lossTf.text = "Loss " + loss.ToString() + " % |";
	}
	
	var time = (m_client.profile.duration * 1000) - m_client.profile.now;
	if(time < 0){ time = 0; }
	m_timeTf.text = "| 残: " + time.ToString();
    }

    protected override void OnDisable(){
	if(m_client != null){
	    m_client.Abort();
	    m_client.Dispose();
	    m_client = null;
	}
	m_payload = null;
	base.OnDisable();
    }
    
    /*==========================================================================================*/

    public void Abort(){ m_client.Abort(); }
    
    [Preserve]
    public void UnLockUI(FlowEvent.Data data){ uiLock.UnLock(); }
    
    [Preserve]
    public void DelayTest(FlowEvent.Data data){
	if(m_client != null){
	    data.task = m_client.UDPing();
	}
    }

    [Preserve]
    public void CopyProfile(FlowEvent.Data data){
	if(m_client != null){
	    if(m_client.profile.status == UDPingClient.Status.E_OK){
		sceneManager.system.delayProfile = m_client.profile;
	    }
	}
    }
    
    /*
    [Preserve]
    public void SetDialogMessage(FlowEvent.Data data){
	m_logConsole.text = data.str;
    }
    */
    /*
    [Preserve]
    public void BtActionHandler(FlowEvent.Data data){
	Debug.Log(data.str);
	switch(data.str){
	    case "Dump":          DumpInfo();          break;
	}
    }
    */
    
    /*==========================================================================================*/
    
}
}
