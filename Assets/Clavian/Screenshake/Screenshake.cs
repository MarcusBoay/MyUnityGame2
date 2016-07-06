//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Screenshake))]
[CanEditMultipleObjects] //why not
public class ScreenshakeEditor : Editor{
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
[AddComponentMenu("Utility/Screenshake", 1)]
public class Screenshake : MonoBehaviour {
	private Transform t;
	private Coroutine c;
	private Vector3 lastMovement = Vector3.zero; //the amound of random shake done last frame
	public ScreenshakeData data;
	public string dataName = "default"; //default tag
	public float multiplier = 1f; //default multiplier
	
	void Awake(){
		t = this.transform;
	}
	void OnEnable(){
		data = Resources.Load("ScreenshakeData") as ScreenshakeData;
		if(data == null){
			Debug.Log("Screenshake data failed to load! make sure you have the resources folder with the file 'ScreenshakeData' in it!");
		}
	}
	public void Shake(){ //call default 
		Shake(dataName,multiplier);
	}
	public void Shake(float multi){ //call default 
		Shake(dataName,multi);
	}
	public void Shake(string name){ //default multiplier
		Shake(name,multiplier);
	}
	public void Shake(string name, float multi){
		if(c != null){
			StopCoroutine(c);
		}
		c = StartCoroutine(DoShake(name, multi));
	}
	IEnumerator DoShake (string name, float multi) {
		ShakeData shake = data.shakes.Find(x => x.name == name); //get the shake info!
		if(shake != null){
			float timer = 0f;
			Vector3 myStrength = shake.strength;
			while(timer < shake.time){
				t.localPosition -= lastMovement; //move back
				myStrength = new Vector3(shake.curve.Evaluate(timer / shake.time) * shake.strength.x * multi,
										shake.curve.Evaluate(timer / shake.time) * shake.strength.y * multi,
										shake.curve.Evaluate(timer / shake.time) * shake.strength.z * multi);
				lastMovement = t.localRotation * new Vector3(Random.Range(-myStrength.x,myStrength.x), 
															Random.Range(-myStrength.y,myStrength.y), 
															Random.Range(-myStrength.z,myStrength.z));//relative to camera's rotation
				if(shake.roundDecimals != 7){
					lastMovement.x = (float)System.Math.Round(lastMovement.x, shake.roundDecimals);
					lastMovement.y = (float)System.Math.Round(lastMovement.y, shake.roundDecimals);
					lastMovement.z = (float)System.Math.Round(lastMovement.z, shake.roundDecimals);
				}
				t.localPosition += lastMovement;
				
				timer += Time.unscaledDeltaTime; //count up using unscaled time.
				yield return null;
			}
			t.localPosition -= lastMovement; //move back
			lastMovement = Vector3.zero;
		}else{
			Debug.Log("A screenshake named '" + name + "' was not found!");
		}
	}
}