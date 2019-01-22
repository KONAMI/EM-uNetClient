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
using TMPro;
using am;

namespace kde.tech
{
    
[Preserve]    
public class ScTitleFlow : FlowNodeManager
{
    [SerializeField]
    string      m_loadInfoLoopTriggerKey; 
    [SerializeField]
    string      m_loginTriggerKey;
    [SerializeField]
    string      m_errorTriggerKey;
	
    public ScTitle      sceneManager { get; set; }
    public UIGroupLock  uiLock       { get; set; }    
    public SimpleDialog dialog       { get; set; }

    [SerializeField]
    GameObject   m_resetGuidBt;
    
    public void ResetGuid(){
	sceneManager.system.userManagerHelper.ResetProfile();
	dialog.SetMsg("ユーザ情報をリセットしました");
    }
    
    
    /*======================================================================================*/
    
    [Preserve]
    public void LoadInfo(FlowEvent.Data data){
	if(sceneManager.system.staticConfig.currentSetting.apiUrl == ""){
	    dialog.SetTitle("Build Error Detect");
	    dialog.SetMsg("InfoAPI URL is NULL !!");
	    dialog.SetLoading(false);
	}
	else {
	    data.task = sceneManager.system.staticConfig.Invoke();
	    dialog.SetTitle("Check AppInfo");
	    dialog.SetMsg("NowLoading...");
	    dialog.SetLoading(true);
	}
    }
    
    [Preserve]
    public void LoadInfoCheck(FlowEvent.Data data){
	if(sceneManager.system.staticConfig.isSuccess){
	    flowNode.BreakLoop(m_loadInfoLoopTriggerKey);
	    var apiInfo = sceneManager.system.staticConfig.apiInfo;
	    foreach(var api in apiInfo.list){
		Debug.Log(api.name + " > " + api.url);
	    }
	    Debug.Log("ApiKey > " + apiInfo.key);

	    // 各種APIClientにStaticConfigをセットして回る
	    sceneManager.system.userManager.config = sceneManager.system.staticConfig;
	}
	else {
	    Debug.Log("Loadinfo Fail. >> Retry.");
	}
    }

    /*======================================================================================*/
    
    [Preserve]
    public void WaitLoginAction(FlowEvent.Data data){
	m_resetGuidBt.SetActive(true);
	dialog.SetLoading(false);
	dialog.SetTitle("Get Ready !");
	dialog.SetMsg("Please Press Login Button !");
	dialog.SetModeOk(() => {
		flowNode.RecieveTriggerHandler(m_loginTriggerKey);
	    }, "LOGIN");	
    }

    [Preserve]
    public void Login(FlowEvent.Data data){
	m_resetGuidBt.SetActive(false);
	data.task = sceneManager.system.userManagerHelper.Login();
	dialog.SetTitle("Login");
	dialog.SetMsg("NowLoading...");
	dialog.SetLoading(true);
    }

    [Preserve]
    public void LoginCheck(FlowEvent.Data data){
	dialog.SetLoading(false);
	if(sceneManager.system.userManager.errno == AuthError.E_OK){
	    sceneManager.system.userManagerHelper.SaveProfile
		(
		 sceneManager.system.userManager.userProfile.guid,
		 sceneManager.system.userManager.userProfile.uid,
		 sceneManager.system.userManager.userProfile.name,
		 sceneManager.system.userManager.sessCode
		 );
	    dialog.SetMsg("認証完了");
	}
	else {
	    flowNode.nextFlowAfterAbort = "ErrorDialog";
	    flowNode.RecieveAbortHandler();
	}
    }

    [Preserve]
    public void LoadItemMaster(FlowEvent.Data data){
	//data.task = sceneManager.system.userManagerHelper.Login();
    }


    
    /*======================================================================================*/
    
    [Preserve]
    public void SetDialogTitle(FlowEvent.Data data){ dialog.SetTitle(data.str); }
    [Preserve]
    public void SetDialogMsg(FlowEvent.Data data){ dialog.SetMsg(data.str); }
    [Preserve]
    public void SetDialogOkBtLabel(FlowEvent.Data data){ dialog.SetOkBtLabel(data.str); }
    
    [Preserve]
    public void ErrorQuit(FlowEvent.Data data){
	dialog.SetTitle("通信エラー");
	dialog.SetMsg("アプリを終了します");
	dialog.SetModeOk(() => {
		flowNode.RecieveTriggerHandler(m_errorTriggerKey);
	    }, "終了");	
    }

    
}
}
