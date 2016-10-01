using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {


		 
		if (Input.GetMouseButtonDown(0)) {
			Vector3 a = Input.mousePosition;
			Debug.Log (a);
			Debug.Log ("First point recorded");

			IEnumerator WaitForInstruction;
			if (Input.GetMouseButtonDown (0)) {
				Vector3 b = Input.mousePosition;
				Debug.Log (b);
				Debug.Log ("Second point recorded");
			}

			if (Input.GetMouseButtonDown (1)) {
				Debug.Log ("Line cancelled");
			}

		}

	}
}
