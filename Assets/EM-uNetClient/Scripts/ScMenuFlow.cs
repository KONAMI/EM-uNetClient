using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using Utf8Json;
using TMPro;
using am;

namespace kde.tech
{
    
[Preserve]    
public class ScMenuFlow : FlowNodeManager
{
    
    public ScMenu       sceneManager { get; set; }
    public UIGroupLock  uiLock       { get; set; }    
    public SimpleDialog dialog       { get; set; }
    
    /*======================================================================================*/

    [Preserve]
    public void UnlockUI(FlowEvent.Data data){
	uiLock.UnLock();
    }

    public void ApplyPi(){

	var param = new PiData.NetworkParams();

	if(sceneManager.system.bandwidthDwProfile.status == BandwidthClient.Status.E_OK){	
	    param.bandUp = sceneManager.system.bandwidthDwProfile.avg;
	    param.bandDw = sceneManager.system.bandwidthDwProfile.avg;
	}
	if(sceneManager.system.delayProfile.status == UDPingClient.Status.E_OK){
	    param.delayUp = sceneManager.system.delayProfile.avg;
	    param.delayDw = sceneManager.system.delayProfile.avg;
	}
	
	Socket sock = null;
	bool   ret  = false; 
	try {
	    var endPoint = new IPEndPoint(IPAddress.Parse("192.168.20.1"), 10393) as EndPoint;
	    sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
	    sock.ReceiveTimeout = 1000;
	    var sendBuf = Encoding.ASCII.GetBytes(JsonSerializer.ToJsonString(param));
	    var sendRet = sock.SendTo(sendBuf, 0, sendBuf.Length, SocketFlags.None, endPoint);	    
	    if(sendRet < 0){ goto wayout; }
	    //UnityEngine.Debug.Log("Send Ret : " + sendRet.ToString());
	}
	catch (SocketException e){ UnityEngine.Debug.Log("Socket Error : " + e.ToString()); }
	catch (Exception e){ UnityEngine.Debug.Log("Fatal Error : " + e.ToString()); }	    	    

      wayout:
	
	if(sock != null){ sock.Close();	}	
    }
    
}
}
