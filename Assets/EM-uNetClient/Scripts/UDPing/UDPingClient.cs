using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Diagnostics;
using System.Runtime.Serialization;
using Utf8Json;
using am;

namespace kde.tech
{

public class UDPingClient : IDisposable
{

    /*======================================================================================*/
    
    Socket      m_sock;
    SocketError m_lastNetError;
    public SocketError lastNetError { get { return m_lastNetError; }}
    AmByteArray m_sendBuf;
    AmByteArray m_recvBuf;
    IPEndPoint  m_sendAddr;
    IPEndPoint  m_recvAddr;
    
    public UDPingClient(string serverAddr, int serverPort, int bindBasePort = 5730){
	int  retry = 0;
	bool isError = false;

	m_sendBuf = new AmByteArray(SEND_BUFFER_SIZE);
	m_recvBuf = new AmByteArray(RECV_BUFFER_SIZE);	
	
	m_sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

	try {
	    m_sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
	    m_sock.Blocking = false;
	    m_sendAddr = new IPEndPoint(IPAddress.Parse(serverAddr), serverPort);
	    m_recvAddr = new IPEndPoint(IPAddress.Any, 0);
	}
	catch (SocketException e){
	    m_lastNetError = e.SocketErrorCode;
	    isError = true;
	    goto wayout;
	}
	catch (Exception e){
	    m_lastNetError = SocketError.SocketError;
	    isError = true;
	    goto wayout;
	}
	
	while(retry < 10){
	    var bindAddr = new IPEndPoint(IPAddress.Any, bindBasePort + retry);
	    try {
		m_sock.Bind(bindAddr);
		break;
	    }
	    catch (SocketException e){
		if(e.SocketErrorCode == SocketError.AddressAlreadyInUse){ ++retry; continue; }
		else {
		    m_lastNetError = e.SocketErrorCode;
		    isError = true;
		    break;
		}
	    }
	    catch (Exception e){
		m_lastNetError = SocketError.SocketError;
		isError = true;
		break;
	    }
	}

    wayout:
	
	if(isError){
	    if(m_sock != null){ Dispose(); }
	}
	else {
	    m_lastNetError = SocketError.Success; 
	}
    }

    public void Dispose(){
	if(m_sock != null){
	    m_sock.Close();
	    m_sock = null;
	}		
    }
    
    public int SendTo(byte [] buffer, int offset, ref int size, 
		      SocketFlags socketFlags, EndPoint remoteEP){
	int sendRet = -1;

	try {
	    sendRet = m_sock.SendTo(buffer, offset, size, socketFlags, remoteEP);	
	    size    = sendRet;
	    m_lastNetError = SocketError.Success;
	}
	catch(SocketException e){
	    //if(e.SocketErrorCode == SocketError.WouldBlock){}
	    sendRet = -1;	    
	    m_lastNetError = e.SocketErrorCode;	    
	}
	catch(Exception e){
	    sendRet = -2;
	    m_lastNetError = SocketError.SocketError;
	}

	return sendRet;
    }
    
    public int RecvFrom(byte [] buffer, int offset, ref int size, 
			SocketFlags socketFlags, ref IPEndPoint remoteEP){
	int recvRet = -1;
	try {
	    var fromAddr = remoteEP as EndPoint;
	    recvRet = m_sock.ReceiveFrom(buffer, offset, size, socketFlags, ref fromAddr);
	    size    = recvRet;
	    m_lastNetError = SocketError.Success;
	}
	catch(SocketException e){
	    //if(e.SocketErrorCode == SocketError.WouldBlock){}
	    recvRet = -1;
	    m_lastNetError = e.SocketErrorCode;	    
	}
	catch(Exception e){
	    recvRet = -2;
	    m_lastNetError = SocketError.SocketError;
	}
	
	return recvRet;
    }
    
    /*======================================================================================*/

    public static readonly int    PRE_WARM_PACKET_NR = 10;
    public static readonly int    RECV_BUFFER_SIZE = 2000;
    public static readonly int    SEND_BUFFER_SIZE = 2000;
    public static readonly uint   MAGIC_COOKIE     = 0x52554450; 
    public static readonly ushort MSG_TYPE_DEFAULT = 0x0001;
    public static readonly ushort MSG_TYPE_KEY     = 0x0002;
    public static readonly ushort MSG_TYPE_ECHO    = 0x0003;
    public static readonly ushort MSG_TYPE_ACK     = 0x0010;
    public static readonly uint   HEADER_SIZE      = 12;
    public static readonly int    TIMEOUT_MSEC     = 3000;

    // dummy
    [Serializable]    
    public class PacketPayload {
	[DataMember(Name = "msg")]	
	public string msg = "";
    }
    
    public int SendTestPacket(ushort pktType, int seq, byte[] payload, int payloadSize){
	var    pkt   = m_sendBuf;
	int    len   = 0;

	pkt.position = 0;
	pkt.writeUnsignedInt(AmNetUtil.htonl(MAGIC_COOKIE));    len += 4;
	//pkt.writeUnsignedInt(MAGIC_COOKIE);                     len += 4;
	pkt.writeUnsignedShort(AmNetUtil.htons(MSG_TYPE_ECHO)); len += 2;
	pkt.writeUnsignedShort(AmNetUtil.htons((ushort)0));     len += 2;
	pkt.writeInt(AmNetUtil.htonl(seq));                     len += 4;
	if(payloadSize > 0){
	    AmByteArray.Memcpy(pkt, payload, payloadSize); len += payloadSize;
	}	
	pkt.position = 6;
	pkt.writeUnsignedShort(AmNetUtil.htons((ushort)len));
	
	return SendTo(pkt.data, 0, ref len, SocketFlags.None, m_sendAddr);
    }

    public int RecvTestPacket(ref ushort pktType, ref int seq, byte[] payload, ref int payloadSize){
	var bufSize = m_recvBuf.data.Length;
	var recvRet = RecvFrom(m_recvBuf.data, 0, ref bufSize, SocketFlags.None, ref m_recvAddr);
	
	if((m_lastNetError == SocketError.Success) && (recvRet >= HEADER_SIZE)){
	   m_recvBuf.position = 0;
	   var mc = AmNetUtil.ntohl(m_recvBuf.readUnsignedInt());
	   if(mc != MAGIC_COOKIE){
	       m_lastNetError = SocketError.SocketError;
	   }
	   else {
	       pktType     = AmNetUtil.ntohs(m_recvBuf.readUnsignedShort());
	       var pktlen  = AmNetUtil.ntohs(m_recvBuf.readUnsignedShort());
	       if(payloadSize < (pktlen - HEADER_SIZE)){
		   m_lastNetError = SocketError.SocketError;		   
	       }
	       else {
		   payloadSize = (int)pktlen - (int)HEADER_SIZE;
		   seq         = AmNetUtil.ntohl(m_recvBuf.readInt());
		   if(payloadSize > 0){
		       AmByteArray.Memcpy(payload, m_recvBuf, payloadSize);
		   }
		   m_lastNetError = SocketError.Success;
	       }
	   }	   
	}
	
	return recvRet;
    }    

    /*======================================================================================*/
    
    public enum Status {
	E_CHAOS,
	E_OK,
	E_INTR,
	E_CRITICAL,
    }

    public class ProgressEvent : UnityEvent<int, int, float>{}

    [Serializable]    
    public class Profile {

	public Status status { get; set; } = Status.E_CHAOS;
	
	public int pps { get; set; } = 10;
	public int pktPayloadSize { get; set; } = 52; // - HEADER_SIZE;
	public int duration { get; set; } = 30;
	
	public class Node {
	    public int  seq { get; set; } = -1;
	    public long sendTime { get; set; } = 0;
	    public long recvTime { get; set; } = 0;
	    public bool isTimeout { get; set; } = false;
	    
	    public int rtt {
		get {
		    if(isTimeout){ return -1; }		    
		    return (isTimeout) ? -1 : (int)(recvTime - sendTime);
		}
	    }
	}

	Stopwatch m_sw = new System.Diagnostics.Stopwatch();
	public long now { get { return m_sw.ElapsedMilliseconds; }}
	public int currentSeq { get; set; } = 0;
	public void BenchStart(int preWarmingNr = 10){
	    m_preWarmingNr = preWarmingNr;
	    m_nodeList.Clear();
	    m_sw.Start();
	}
	public void BenchEnd(){
	    m_sw.Stop();	    
	    m_sw.Reset();
	    // パケロスは除外しないとダメなので、終わった段階でRemoveする。
	    // 多分、手でList作った方がはやいなこれ...
	    m_sortedList = m_nodeList.OrderBy(n => n.rtt);
	}
	
	IEnumerable<Node> m_sortedList = null;
	    	
	List<Node> m_nodeList = new List<Node>();
	int m_preWarmingNr   = 10; // 先頭のN個はブレが大きいので落とす。

	public void SendHandler(int seq){
	    var now = m_sw.ElapsedMilliseconds;
	    m_nodeList.Add(new Node(){ seq = seq, sendTime = now });
	}
	
	public Node RecvHandler(int seq){
	    var now  = m_sw.ElapsedMilliseconds;	    
	    var node = m_nodeList.FirstOrDefault(x => x.seq == seq); // @todo 高速化
	    if(node != null){
		if(node.recvTime == 0){ node.recvTime = now; }
		UnityEngine.Debug.Log("RecvHandler : " + seq.ToString() + " > rtt " + node.rtt.ToString());
	    }
	    return node;
	}
	
	public Node TimeoutHandler(int seq){
	    var now  = m_sw.ElapsedMilliseconds;	    
	    var node = m_nodeList.FirstOrDefault(x => x.seq == seq); // @todo 高速化
	    if(node != null){
		if(node.recvTime == 0){ node.recvTime = -1; }
		else                  { node = null; }
	    }
	    return node;
	}

	public int pktSize {
	    get {
		return pktPayloadSize + (int)HEADER_SIZE;
	    }
	}
	
	public int median {
	    get {
		var nr = m_nodeList.Count;
		// @todo Timeoutを除外
		return (nr > 0) ? m_sortedList.ElementAt((int)nr/2).rtt : 0;
	    }
	}
	public int avg {
	    get {
		// -1を除外しないとダメなので、要手動計算
		return (int)m_nodeList.Average(n => n.rtt);		
	    }
	}
	public float loss {
	    get {
		var all     = (float)m_nodeList.Count;
		var timeout = (float)m_nodeList.Select(n => n.rtt < 0).Count();
		return (float)Math.Round(1f - timeout/all, 2, MidpointRounding.AwayFromZero);
	    }
	}
	
    }

    Profile m_proflie = new Profile();
    public Profile profile { get { return m_proflie; }}
    
    bool m_isAbort = false;
    public void Abort(){ m_isAbort = true; }
    
    //ProgressEvent m_progressEvents = new ProgressEvent();
    //public ProgressEvent progressEvents { get { return m_progressEvents; }}
    
    public async Task UDPing(){

	bool isError = false;
	int  nr      = profile.pps * profile.duration;
	m_isAbort   = false;
	
	profile.BenchStart();

	var startMicroSec = profile.now * 1000;

	var body           = new PacketPayload();
	if(profile.pktPayloadSize > 10){ body.msg = new string('X', profile.pktPayloadSize - 10); }

	var payload        = JsonSerializer.Serialize(body);
	var preWarmCnt     = PRE_WARM_PACKET_NR;
	
	for(;profile.currentSeq < nr; ++profile.currentSeq){
	    if(m_isAbort){ break; }
	    
	    SendTestPacket(MSG_TYPE_ECHO, profile.currentSeq, payload, payload.Length);
	    
	    if(preWarmCnt > 0){ --preWarmCnt; }
	    else              { profile.SendHandler(profile.currentSeq); }
	    
	    var deltaMicroSec = 1000000 * profile.currentSeq / profile.pps;
	    var waitByMSec    = (int)(((startMicroSec + deltaMicroSec) / 1000) - profile.now);
	    //UnityEngine.Debug.Log(profile.currentSeq.ToString() + " : " + waitByMSec.ToString());
	    if(waitByMSec > 0){ await Task.Delay(waitByMSec); }	    
	}
	
	// 最終PacketのTimeoutを待つためにWait
	await Task.Delay(TIMEOUT_MSEC);

	profile.BenchEnd();
	
	if     (m_isAbort){ profile.status = Status.E_INTR; }
	else if(isError)  { profile.status = Status.E_CRITICAL; }
	else              { profile.status = Status.E_OK; }	    
    }
    
}
}
