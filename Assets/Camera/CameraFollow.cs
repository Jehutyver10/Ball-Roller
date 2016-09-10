﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Animator anim;
	public float damping = 1, dampDelay = 2, slowDamp = 10, adjustedDamp = 100;
	public float camCorrection;
	public float smoothTime = 0.3F;

	private float dampTime, checkTime;
	private GameObject player;
	private Vector3 offset, velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		dampTime = Time.time;
		player = FindObjectOfType<PlayerController>().gameObject;
		anim = GetComponent<Animator>();
		offset =  player.transform.position - transform.position;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		Follow();
	}

	void Follow(){
	//sets the angle of the player's to be the camera's so that the camera is behind the player.
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = player.transform.eulerAngles.y;
		//print("Current angle: " + currentAngle + ", Desired Angle: " + desiredAngle);
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.fixedTime * damping);
		Quaternion rotation = Quaternion.Euler(0, angle, 0);

		transform.position = Vector3.SmoothDamp(transform.position, player.transform.position - (rotation * offset), ref velocity, smoothTime);
		transform.LookAt(player.transform);
//		Quaternion fixedRotation =  Quaternion.Euler(transform.eulerAngles.x + camCorrection, transform.eulerAngles.y, transform.eulerAngles.z);
//		transform.rotation = fixedRotation;
	}

	public void AdjustDamping(){
		if(damping <adjustedDamp){
			checkTime = Time.time;
			if(checkTime - dampTime >= dampDelay){
				damping = adjustedDamp;
			}
		}
	}

	public void SlowDamping(){
		damping = slowDamp;
		dampTime = Time.time;
	}
}
