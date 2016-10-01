using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

	// Update is called once per frame
	void Update () {
		Debug.Log ("Update");
		Debug.Log ("Going to move");
		var x = Input.GetAxis ("Horizontal");
		var z = Input.GetAxis ("Vertical");
		transform.Translate (x, 0, z);
	}
}
