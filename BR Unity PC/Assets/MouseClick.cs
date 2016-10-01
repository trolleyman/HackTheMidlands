using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (Input.mousePosition (0)) {
			Vector3 a = Input.mousePosition;
		}
		Debug.Log (a);
	}
}
