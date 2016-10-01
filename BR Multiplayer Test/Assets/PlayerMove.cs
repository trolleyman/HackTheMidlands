using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Only move if we are controlling the local cube
		if (!isLocalPlayer)
			return;
		
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;
		transform.Translate(x, 0, z);
	}

	public override void OnStartLocalPlayer() {
		// Make the local cube red
		GetComponent<MeshRenderer>().material.color = Color.red;
	}

}
