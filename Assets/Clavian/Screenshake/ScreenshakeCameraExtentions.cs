﻿//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
//Allows you to call t.GetComponent<Camera>().Screenshake();
//instead of t.GetComponent<Screenshake>().Shake();
//because why not????????
public static class ScreenshakeCameraExtentions {
	public static void Shake (this Camera cam) {
		Screenshake shake = cam.transform.GetComponent<Screenshake>();
		if(shake != null){
			shake.Shake();
		}else{
			Debug.Log("Camera doesn't have Screenshake component, adding one!");
			cam.transform.gameObject.AddComponent<Screenshake>().Shake(); //add and shake
		}
	}
	public static void Shake (this Camera cam, float multi) {
		Screenshake shake = cam.transform.GetComponent<Screenshake>();
		if(shake != null){
			shake.Shake(multi);
		}else{
			Debug.Log("Camera doesn't have Screenshake component, adding one!");
			cam.transform.gameObject.AddComponent<Screenshake>().Shake(multi); //add and shake
		}
	}
	public static void Shake (this Camera cam, string name) {
		Screenshake shake = cam.transform.GetComponent<Screenshake>();
		if(shake != null){
			shake.Shake(name);
		}else{
			Debug.Log("Camera doesn't have Screenshake component, adding one!");
			cam.transform.gameObject.AddComponent<Screenshake>().Shake(name); //add and shake
		}
	}
	public static void Shake (this Camera cam, string name, float multi) {
		Screenshake shake = cam.transform.GetComponent<Screenshake>();
		if(shake != null){
			shake.Shake(name,multi);
		}else{
			Debug.Log("Camera doesn't have Screenshake component, adding one!");
			cam.transform.gameObject.AddComponent<Screenshake>().Shake(name,multi); //add and shake
		}
	}

	
	public static void Kick (this Camera cam, Vector3 dir) {
		Kickback kick = cam.transform.GetComponent<Kickback>();
		if(kick != null){
			kick.Kick(dir);
		}else{
			Debug.Log("Camera doesn't have Kickback component, adding one!");
			cam.transform.gameObject.AddComponent<Kickback>().Kick(dir); //add and shake
		}
	}
	public static void Kick (this Camera cam, string name, Vector3 dir) {
		Kickback kick = cam.transform.GetComponent<Kickback>();
		if(kick != null){
			kick.Kick(name, dir);
		}else{
			Debug.Log("Camera doesn't have Kickback component, adding one!");
			cam.transform.gameObject.AddComponent<Kickback>().Kick(name, dir); //add and shake
		}
	}
	public static void Kick (this Camera cam, Vector3 dir, float multi) {
		Kickback kick = cam.transform.GetComponent<Kickback>();
		if(kick != null){
			kick.Kick(dir, multi);
		}else{
			Debug.Log("Camera doesn't have Kickback component, adding one!");
			cam.transform.gameObject.AddComponent<Kickback>().Kick(dir, multi); //add and shake
		}
	}
	public static void Kick (this Camera cam, string name, Vector3 dir, float multi) {
		Kickback kick = cam.transform.GetComponent<Kickback>();
		if(kick != null){
			kick.Kick(name, dir, multi);
		}else{
			Debug.Log("Camera doesn't have Kickback component, adding one!");
			cam.transform.gameObject.AddComponent<Kickback>().Kick(name, dir, multi); //add and shake
		}
	}
}