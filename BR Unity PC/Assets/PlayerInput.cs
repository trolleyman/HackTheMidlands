using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) GameObject.Destroy( GameObject.Find ("Cube"));
		var x = Input.GetAxis ("Horizontal") * 0.1f;
		var z = Input.GetAxis ("Vertical") * 0.1f;
		transform.Translate (x, 0, z);
	}

}
