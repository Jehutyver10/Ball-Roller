﻿using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	public bool canBeHomedInOn, alive = false;
	public float shotsPerSecond, meleeLimit, detectionRange;
	public GameObject laser;
	Health health;


	private GameObject target;
	// Use this for initialization

	void Awake(){
		SetTarget();
	}
	void Start () {
		health = GetComponent<Health>();
	}

	// Update is called once per frame
	void Update(){

		transform.LookAt(target.transform);
		float probability = Time.deltaTime * shotsPerSecond;
		if(Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) < detectionRange){//check if player is within enemy's detection range
			alive = true;
		}else{	
			alive = false;
		}
		if(Vector3.Distance(target.transform.position, transform.position) > meleeLimit && alive){ //check if outside melee limit
			if(Random.value < probability){
				Shoot();
				print("shooting");
			}else{
				print("Melee attacking");
			}
		}

		//TODO remove this in final version; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			alive = !alive;
		}
	}

	void SetTarget(){
		target = GameObject.FindObjectOfType<PlayerController>().gameObject;
	}
	void Shoot(){
		GameObject shot = Instantiate(laser, transform.position + transform.forward, Quaternion.identity) as GameObject;
		shot.transform.parent = this.transform;
		shot.GetComponent<Projectile>().SetShooter(this.gameObject);
		//Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		//shot.transform.rotation = q * shot.transform.rotation;
		shot.GetComponent<Rigidbody>().AddForce(transform.forward * shot.GetComponent<Projectile>().speed);

	}

}
