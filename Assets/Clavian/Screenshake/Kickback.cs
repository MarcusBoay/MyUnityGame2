//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Kickback))]
[CanEditMultipleObjects] //why not
public class KickbackEditor : Editor{
	override public void OnInspectorGUI(){
		serializedObject.Update(); //for onvalidate stuff!
		SerializedProperty data = serializedObject.FindProperty("data");
		SerializedProperty dataName = serializedObject.FindProperty("dataName");
		SerializedProperty multiplier = serializedObject.FindProperty("multiplier");
		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.PropertyField(data); //so you can click thru to edit!
		EditorGUI.EndDisabledGroup();
		EditorGUILayout.PropertyField(dataName);
		EditorGUILayout.PropertyField(multiplier);
		serializedObject.ApplyModifiedProperties();
	}
}
[ExecuteInEditMode]
#endif
[AddComponentMenu("Utility/Kickback", 1)]
public class Kickback : MonoBehaviour {
	private Transform t;
	private Coroutine c;
	private Vector3 lastMovement;
	public ScreenshakeData data;
	public string dataName = "default"; //default tag
	public float multiplier = 1f; //default multiplier
	
	void Awake(){
		t = this.transform;
	}
	void OnEnable(){
		data = Resources.Load("ScreenshakeData") as ScreenshakeData;
		if(data == null){
			Debug.Log("Kickback data failed to load! make sure you have the resources folder with the file 'ScreenshakeData' in it!");
		}
	}
	public void Kick(Vector3 dir){ //call default 
		Kick(dataName,dir,multiplier);
	}
	public void Kick(string name, Vector3 dir){ //call default 
		Kick(name,dir,multiplier);
	}
	public void Kick(Vector3 dir, float multi){ //call default 
		Kick(dataName,dir,multi);
	}
	public void Kick(string name, Vector3 dir, float multi){
		if(c != null){
			StopCoroutine(c);
		}
		c = StartCoroutine(DoKick(name, dir, multi));
	}
	IEnumerator DoKick (string name, Vector3 dir, float multi) {
		KickbackData kick = data.kicks.Find(x => x.name == name);
		if(kick != null){
			float timer = 0f;
			float myStrength = kick.strength;
			while(timer < kick.time){
				t.localPosition -= lastMovement; //move back
				myStrength = kick.curve.Evaluate(timer / kick.time) * kick.strength * multi;
				lastMovement = t.localRotation * dir.normalized * myStrength;//relative to camera's rotation
				if(kick.roundDecimals != 7){
					lastMovement.x = (float)System.Math.Round(lastMovement.x, kick.roundDecimals);
					lastMovement.y = (float)System.Math.Round(lastMovement.y, kick.roundDecimals);
					lastMovement.z = (float)System.Math.Round(lastMovement.z, kick.roundDecimals);
				}
				t.localPosition += lastMovement;
				
				timer += Time.unscaledDeltaTime; //count up using unscaled time.
				yield return null;
			}
			t.localPosition -= lastMovement; //move back
			lastMovement = Vector3.zero;
		}else{
			Debug.Log("A Kickback named '" + name + "' was not found!");
		}
	}
}