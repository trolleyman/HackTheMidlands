﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TrackPlayer : NetworkBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform.position, -Vector3.up);
	}
}
