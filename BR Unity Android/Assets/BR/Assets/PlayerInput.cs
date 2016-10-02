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
		var z = Input.GetAxis ("Vertical") * 0.1f;
		var x = Input.GetAxis ("Horizontal") * 0.1f;
		transform.Translate (x, 0, z);
	}
}
