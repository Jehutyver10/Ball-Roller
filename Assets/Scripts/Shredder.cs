﻿using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider collider){
		if(collider.GetComponent<Projectile>()){
			print(collider.gameObject.name + " destroyed by " + gameObject.name);
			Destroy(collider.gameObject);
		}
	}
}
