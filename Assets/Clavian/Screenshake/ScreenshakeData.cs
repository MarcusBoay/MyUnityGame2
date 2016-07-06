//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ShakeData {
	[Tooltip("The name that will call this.")]
	public string name;
	[Tooltip("The strength of the shake, on individual axises, in localPosition.")]
	public Vector3 strength = Vector2.one; //Vector2.one can be used to get a nice 2d screenshake
	[Range(0.0001f, 10f)]
	[Tooltip("How long the screenshake will last for.")]
	public float time = 0.5f;
	[Tooltip("Intensity over time.")]
	public AnimationCurve curve = new AnimationCurve(new Keyframe(0,1,-1,-1), new Keyframe(1,0,-1,-1));
	[Tooltip("What decimal play the random values should be rounded to. 7 means disabled. Still not perfect because floats.")]
	[Range(0,7)]
	public int roundDecimals = 7;
}
[System.Serializable]
public class KickbackData {
	[Tooltip("The name that will call this.")]
	public string name;
	[Tooltip("The strength of the kickback, in localPosition.")]
	public float strength = 1f;
	[Range(0.0001f, 10f)]
	[Tooltip("How long the kickback will last for.")]
	public float time = 0.5f;
	[Tooltip("Distance over time.")]
	public AnimationCurve curve = new AnimationCurve(new Keyframe(0,1,-1,-1), new Keyframe(1,0,-1,-1));
	[Tooltip("What decimal play the random values should be rounded to. 7 means disabled. Still not perfect because floats.")]
	[Range(0,7)]
	public int roundDecimals = 7;
}
[CreateAssetMenu(fileName = "New Screenshake Data", menuName = "Clavian/Screenshake Data", order = 1)]
public class ScreenshakeData : ScriptableObject {
	[Header("Shakes")]
	public List<ShakeData> shakes = new List<ShakeData>(); //a list, so you can use Find()
	[Header("Kickbacks")]
	public List<KickbackData> kicks = new List<KickbackData>();
}
