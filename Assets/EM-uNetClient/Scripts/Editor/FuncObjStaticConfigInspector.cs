using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using am;

namespace kde.tech
{

[CustomEditor(typeof(FuncObjStaticConfig))]
public class FuncObjStaticConfigInspector : FuncObjInspector<FuncObjStaticConfig>
{
    
    protected override void DrawCustomInspector(){
	var fo = target as FuncObjStaticConfig;

	GUILayout.Box(GUIContent.none, HrStyle.EditorLine, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
	GUILayout.Space(5);		
	DrawSimpleLabelField("EnvSetting", "", EditorStyles.boldLabel);
	DrawSimpleEnumField(fo, "Env", ref fo.currentApiEnv);
	DrawSimpleBoolField(fo, "DebugMode", ref fo.isDebugMode);
	GUILayout.Space(5);	
	GUILayout.Box(GUIContent.none, HrStyle.EditorLine, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
	GUILayout.Space(5);	

	DrawList<FuncObjStaticConfig.EmbededSetting>(fo, fo.setting);

	/*
	  if(GUILayout.Button("Test", EditorStyles.miniButton)){ FuncObjS3ProxyClientInspector.Invoke(); }
	*/
    }
    
    protected override void DrawListNode(FuncObjBase.ListNodeBase nodeBase, Type nodeT){	
	var fo = target as FuncObjStaticConfig;
	var node = nodeBase as FuncObjStaticConfig.EmbededSetting;
	base.DrawListNode(node, nodeT);	
	DrawSimpleEnumField(fo, "Env", ref node.env);
	DrawSimpleTextField(fo, "ApiUrl", ref node.apiUrl);
	DrawSimpleIntField(fo, "Revision", ref node.revision);
	DrawSimpleTextField(fo, "Version", ref node.version);
	DrawSimpleTextField(fo, "VersionLabel", ref node.versionLabel);
    }
    
}
}    

/*
 * Local variables:
 * compile-command: ""
 * End:
 */
