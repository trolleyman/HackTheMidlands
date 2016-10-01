using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		var x = Input.GetAxis ("Horizontal");
		var z = Input.GetAxis ("Vertical");
		transform.Translate (x, 0, z);
	}
}
