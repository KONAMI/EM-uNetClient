using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using Utf8Json;
using System.Diagnostics;

#pragma warning disable 168

namespace kde.tech
{

[Serializable, CreateAssetMenu( fileName = "BandwidthTester", menuName = "BandwidthTester", order = 1500 )]
public class BandwidthClient : ScriptableObject
{
    public enum Status {
	E_CHAOS,
	E_OK,
	E_INTR,
	E_CRITICAL,
    }
    
    public class ProgressEvent : UnityEvent<int, float>{}

    [SerializeField]
    int m_recvBufSize = 4096;

    public class Profile {

	public Status status { get; set; } = Status.E_CHAOS;
	
	public string datSize { get; set; } = "8M";
	public int duration { get; set; } = 20;
	
	public class Node {
	    public int seq { get; set; } = -1;	    
	    public int deltaMsec { get; set; } = 0;
	    public int bytes { get; set; } = 0;
	    public int kbps { get { return (deltaMsec > 0) ? (int)(8 * 1000 * bytes / 1024 / deltaMsec) : 0; }}
	}

	long m_prevMsec = 0;	
	Stopwatch m_sw = new System.Diagnostics.Stopwatch();
	public void BenchStart(int preWarmingNr = 10, int updateCalcRate = 10){
	    m_prevMsec = 0;
	    m_preWarmingNr = preWarmingNr;
	    m_updateCalcRate = updateCalcRate;
	    m_samplingBuff = 0;
	    m_nodeList.Clear();
	    m_sw.Start();
	}

	IEnumerable<Node> m_sortedList = null;
	    
	public void BenchEnd(){
	    m_sw.Stop();	    
	    m_sw.Reset();
	    //if((m_nodeList.Count % m_updateCalcRate) == 0){
	    m_sortedList = m_nodeList.OrderBy(n => n.kbps);
	    //}	    
	}
	int BenchDelta(int samplingMsec){
	    var now = m_sw.ElapsedMilliseconds;
	    var ret = (int)(now - m_prevMsec);
	    if(samplingMsec > ret){ return -1; }
	    m_prevMsec = now;
	    return ret;
	}

	List<Node> m_nodeList = new List<Node>();
	int m_updateCalcRate = 10;
	int m_preWarmingNr   = 10; // 先頭のN個はブレが大きいので落とす。
	Node m_lastNode = null;
	public Node lastMetric { get { return m_lastNode; }}
	public int currentMetricNr { get { return m_nodeList.Count; }}
	
	int m_samplingMsec = 100; // 10ミリ秒を割ったらバッファ
	int m_samplingBuff = 0; // バッファ中の累計
	
	public Node RecordHandler(int deltaSize){
	    // サンプリング頻度が1msec以下になることもあるので、バッファして、一定までは貯める。
	    var delta = BenchDelta(m_samplingMsec);
	    
	    m_samplingBuff += deltaSize;
	    
	    if(delta < 0){ return null; }
		
	    if(m_preWarmingNr > 0){
		m_samplingBuff = 0;
		m_preWarmingNr--;		
		return null;
	    }

	    var seq = m_nodeList.Count;
	    m_lastNode = new Node(){ seq = seq, deltaMsec = delta, bytes = m_samplingBuff };
	    m_nodeList.Add(m_lastNode);
	    m_samplingBuff = 0;

	    return m_lastNode;
	}

	public int median {
	    get {
		var nr = m_nodeList.Count;
		//return (nr > 0) ? m_nodeList[(int)nr].kbps : -1;
		return (nr > 0) ? m_sortedList.ElementAt((int)nr/2).kbps : 0;
	    }
	}
	public int avg {
	    get {
		return (int)m_nodeList.Average(n => n.kbps);		
	    }
	}
	
    }

    Profile m_proflie = new Profile();
    public Profile profile { get { return m_proflie; }}
    
    int m_loadbytes;
    public int loadbytes { get { return m_loadbytes; }}
    
    bool m_isAbort = false;
    public void Abort(){ m_isAbort = true; }
    
    ProgressEvent m_progressEvents = new ProgressEvent();
    public ProgressEvent progressEvents { get { return m_progressEvents; }}
    
    public async Task<Status> Get(string url){

	var isError = false;
	m_isAbort   = false;
	    
	profile.BenchStart();
	    
	using (var client = new HttpClient()){
		
	    var resp = await SendGetQuery(client, url);
	    if(resp == null){ isError = true; goto wayout; }
	    
	    var contentLength = resp.Headers.ContentLength;
	    long totalBytes   = 0;
	    
	    var respContent = await resp.ReadAsStreamAsync();
	    
	    byte[] buffer = new byte[m_recvBufSize];
	    int numBytes;
	    
	    do {
		try {
		    numBytes = await respContent.ReadAsync(buffer, 0, m_recvBufSize);
		}
		catch (Exception exception){
		    isError = true;				    
		    break;
		}
		totalBytes += numBytes;
		var progress = (float)totalBytes / (float)contentLength;
		m_loadbytes = (int)totalBytes;
		var node = profile.RecordHandler(numBytes);
		var seq = (node == null) ? -1 : node.seq;
		m_progressEvents.Invoke(seq, progress);
		if(m_isAbort || isError){ break; }
	    } while(numBytes > 0);
	}

    wayout:
	
	profile.BenchEnd();
	
	//if     (m_isAbort){ profile.status =  Status.E_INTR; }
	if     (m_isAbort){ profile.status = Status.E_OK; } // 暫定
	else if(isError)  { profile.status = Status.E_CRITICAL; }
	else              { profile.status = Status.E_OK; }

	return profile.status;
    }

    public async Task<Status> Post(string url, string datName){
	
	var isError = false;
	m_isAbort   = false;
	
	profile.BenchStart();

	await Task.Delay(1000);

    wayout:
	
	profile.BenchEnd();
	
	//if     (m_isAbort){ profile.status =  Status.E_INTR; }
	if     (m_isAbort){ profile.status = Status.E_OK; } // 暫定
	else if(isError)  { profile.status = Status.E_CRITICAL; }
	else              { profile.status = Status.E_OK; }

	return profile.status;
    }
    
    async Task<HttpContent> SendGetQuery(HttpClient client, string url){
	HttpResponseMessage resp = null;
	if(url == ""){ return null; }
       
	resp = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

	if(!resp.IsSuccessStatusCode){
	    UnityEngine.Debug.Log("> Fail - " + resp.ReasonPhrase + " ( " + resp.StatusCode.ToString() +" )");
	    return null;
	}
	
	return resp.Content;
    } 

    async Task<HttpContent> SendPostQuery(HttpClient client, string url, string datName){
	HttpResponseMessage resp = null;
	if(url == ""){ return null; }

	string postDat = "";
	switch(datName){
	    case "512K": postDat = new string('X', 1024*512);    break;
	    case "1M":   postDat = new string('X', 1024*1024);   break;
	    case "2M":   postDat = new string('X', 1024*1024*2); break;
	    case "4M":   postDat = new string('X', 1024*1024*4); break;
	    case "8M":   postDat = new string('X', 1024*1024*8); break;
	}
	
	using (var multipart = new MultipartFormDataContent())
	using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(postDat))) 
	{
	    multipart.Add(new StreamContent(ms));
	    
	    resp = await client.PostAsync(url, multipart);

	    if(!resp.IsSuccessStatusCode){
		UnityEngine.Debug.Log("> Fail - " + resp.ReasonPhrase + " ( " + resp.StatusCode.ToString() +" )");
		return null;
	    }
	}
	
	return resp.Content;
    } 
    
}
}

#pragma warning restore 168
