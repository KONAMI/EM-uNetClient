using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using am;

namespace kde.tech
{

[CustomEditor(typeof(FuncObjBase))]
public class FuncObjInspector<T> : Editor where T : FuncObjBase
{
    
    protected Stack<Action> m_callbackQueue;
    protected bool          m_isTermPollCallbackQueue;

    protected virtual void OnEnable()
    {
	m_callbackQueue   = new Stack<Action>();
	m_isTermPollCallbackQueue = false;
    }

    protected virtual void DrawCustomInspector(){
	DrawDefaultInspector(); 
    }
    
    public override void OnInspectorGUI()
    {
	var fo = target as FuncObjBase;
	DrawInvokeMenu(fo);
	DrawCustomInspector();
    }

    void PollCallbackQueue(){
	while(m_callbackQueue.Count > 0){ (m_callbackQueue.Pop())(); }
	if(! m_isTermPollCallbackQueue){ EditorApplication.delayCall += PollCallbackQueue; }
    }

    protected static T SearchFo(){
	//Debug.Log(typeof(T).ToString());
	var guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).ToString());
	if (guids.Length == 0){ return null; }
	var path = AssetDatabase.GUIDToAssetPath(guids[0]);
	return AssetDatabase.LoadAssetAtPath<T>(path);	
    }

    public static async void Invoke(){	
	var fo = SearchFo();
	if(fo == null){ Debug.Log("FuncObj NotFound."); }
	else {
	    //Debug.Log("LAMBDA ASYNC ST");
	    await fo.Invoke();
	    //Debug.Log("LAMBDA ASYNC ED");
	}
    }
    
    /*=================================================================================================*/

    protected virtual void DrawListNode(FuncObjBase.ListNodeBase node, Type nodeT){
	var fo = target as FuncObjBase;
	DrawSimpleTextField(fo, "Label :", ref node.label);
    }
    protected virtual void DrawList<NodeT>(T fo, List<NodeT> list) where NodeT : FuncObjBase.ListNodeBase, new() {
	
	for(int idx = 0; idx < list.Count; ++idx){
	    var node = list[idx];
	    
	    DrawListNode(node, typeof(NodeT));
	    
	    EditorGUILayout.BeginHorizontal();
	    {
		if(GUILayout.Button("↑", EditorStyles.miniButton)){ 
		    if(idx > 0){
			list.RemoveAt(idx); 
			list.Insert(idx-1, node);
			EditorUtility.SetDirty(fo);
		    }
		}
		if(GUILayout.Button("↓", EditorStyles.miniButton)){ 
		    if(idx < (list.Count - 1)){
			list.RemoveAt(idx); 
			list.Insert(idx+1, node);
			EditorUtility.SetDirty(fo);
		    }
		}
		if(GUILayout.Button("Remove", EditorStyles.miniButton)){ 
		    list.RemoveAt(idx); 
		    --idx;
		    EditorUtility.SetDirty(fo);
		}		    
	    }
	    EditorGUILayout.EndHorizontal();
	    
	    GUILayout.Space(5);	
	    GUILayout.Box(GUIContent.none, HrStyle.EditorLine, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
	    GUILayout.Space(5);	    
	}

	EditorGUILayout.BeginHorizontal();
	{
	    if(GUILayout.Button("Add", EditorStyles.miniButton)){		
		list.Add(new NodeT());
		EditorUtility.SetDirty(fo);
	    }
	    if(GUILayout.Button("Save", EditorStyles.miniButton)){
		m_callbackQueue.Push(() => {		
			EditorUtility.SetDirty(fo);		
			AssetDatabase.SaveAssets();
			EditorUtility.DisplayDialog
			("FuncObjInspector :: Save", 
			 "Save Success.", "OK"); 
		    });
		m_isTermPollCallbackQueue = false;
		EditorApplication.delayCall += PollCallbackQueue;		
	    }
	}
	EditorGUILayout.EndHorizontal();		
    }
    
    /*=================================================================================================*/
    
    protected virtual void DrawInvokeMenu(FuncObjBase fo){

	DrawSimpleLabelField("Invoke Menu");
	EditorGUILayout.BeginHorizontal();
	{
	    if(GUILayout.Button("Invoke", EditorStyles.miniButton)){
		m_callbackQueue.Push(async () => {
			m_isTermPollCallbackQueue = true;
			await fo.Invoke();
			EditorUtility.DisplayDialog(fo.ToString() + "::Invoke", "Complete", "OK");
			
		    });		
		m_isTermPollCallbackQueue = false;
		EditorApplication.delayCall += PollCallbackQueue;
	    }
	}       
	EditorGUILayout.EndHorizontal();
	GUILayout.Space(5);	
	//GUILayout.Box(GUIContent.none, HrStyle.EditorLine, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
	//GUILayout.Space(5);	
    }

    protected void DrawSimpleLabelField(string label, string value = "",
					GUIStyle style = null, float defaultLabelWidth = 80f)
    {
	EditorGUILayout.BeginHorizontal();
	{
	    if(style == null){ style = EditorStyles.label; }
	    EditorGUILayout.LabelField(label, style, GUILayout.Width(defaultLabelWidth));
	    if(value != ""){
		EditorGUILayout.LabelField(value);
	    }
	}
	EditorGUILayout.EndHorizontal();	
    }
    
    protected void DrawSimpleTextField(FuncObjBase fo, string label, ref string value,
				       float defaultLabelWidth = 80f)
    {
	EditorGUILayout.BeginHorizontal();
	{
	    EditorGUILayout.LabelField(label, GUILayout.Width(defaultLabelWidth));
	    var input = EditorGUILayout.TextField(value);
	    if(input != value){
		Undo.RegisterCompleteObjectUndo(fo, label + " Change");
		value = input;
		EditorUtility.SetDirty(fo);
	    }
	}
	EditorGUILayout.EndHorizontal();	
    }
    
    protected void DrawSimpleIntField(FuncObjBase fo, string label, ref int value,
				       float defaultLabelWidth = 80f)
    {
	EditorGUILayout.BeginHorizontal();
	{
	    EditorGUILayout.LabelField(label, GUILayout.Width(defaultLabelWidth));
	    var input = EditorGUILayout.IntField(value);
	    if(input != value){
		Undo.RegisterCompleteObjectUndo(fo, label + " Change");
		value = input;
		EditorUtility.SetDirty(fo);
	    }
	}
	EditorGUILayout.EndHorizontal();	
    }

    protected void DrawSimpleBoolField(FuncObjBase fo, string label, ref bool value,
				       float defaultLabelWidth = 80f)
    {
	EditorGUILayout.BeginHorizontal();
	{
	    EditorGUILayout.LabelField(label, GUILayout.Width(defaultLabelWidth));
	    var input = EditorGUILayout.Toggle(value);
	    if(input != value){
		Undo.RegisterCompleteObjectUndo(fo, label + " Change");
		value = input;
		EditorUtility.SetDirty(fo);
	    }
	}
	EditorGUILayout.EndHorizontal();	
    }

    protected void DrawSimpleEnumField<EnumT>(FuncObjBase fo, string label, ref EnumT value,
					      float defaultLabelWidth = 80f) where EnumT : struct
    {
	EditorGUILayout.BeginHorizontal();
	{
	    EditorGUILayout.LabelField(label, GUILayout.Width(defaultLabelWidth));

	    // @TODO cache
	    // var input = EditorGUILayout.EnumPopup(value) as EnumT; // これで通ればなぁ...
	    var enumPopup = typeof(EditorGUILayout).GetMethod("EnumPopup", BindingFlags.Static | BindingFlags.Public, null, new System.Type[]{ typeof(System.Enum), typeof(GUILayoutOption[])}, null);
	    var input = (EnumT)enumPopup.Invoke(null, new object[]{ value, null });
	    if(!input.Equals(value)){
		Undo.RegisterCompleteObjectUndo(fo, label + " Change");
		value = input;
		EditorUtility.SetDirty(fo);
	    }
	}
	EditorGUILayout.EndHorizontal();	
    }
    
    protected void DrawSimpleObjectField<ObjT>(FuncObjBase fo, string label, ref ObjT value,
					       float defaultLabelWidth = 80f) where ObjT : UnityEngine.Object
    {
	EditorGUILayout.BeginHorizontal();
	{
	    EditorGUILayout.LabelField(label, GUILayout.Width(defaultLabelWidth));
	    var input = EditorGUILayout.ObjectField(value, typeof(ObjT), true) as ObjT;
	    if(input != value){
		Undo.RegisterCompleteObjectUndo(fo, label + " Change");
		value = input;
		EditorUtility.SetDirty(fo);
	    }
	}
	EditorGUILayout.EndHorizontal();	
    }
    
}
}    

/*
 * Local variables:
 * compile-command: ""
 * End:
 */
