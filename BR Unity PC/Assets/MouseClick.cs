using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	private bool isDown1;
	private Vector3 mousePosFirst;
	private Vector3 mousePosSecond;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !isDown1) {
			isDown1 = true;
			var mousePosFirst = Input.mousePosition;
			mousePosFirst.z = 10.0f;
			mousePosFirst = Camera.main.ScreenToWorldPoint(mousePosFirst);
			Debug.Log ("First point recorded" + mousePosFirst);
		}
		if (Input.GetMouseButtonUp (0) && isDown1) {
			isDown1 = false;

			var mousePosSecond = Input.mousePosition;
			mousePosSecond.z = 10.0f;
			mousePosSecond = Camera.main.ScreenToWorldPoint(mousePosSecond);
			mousePosSecond.x = Mathf.Round(mousePosSecond.x);
			mousePosSecond.z = Mathf.Round(mousePosSecond.z);
			Debug.Log ("Second point recorded" + mousePosSecond);

			GameObject wall = GameObject.CreatePrimitive (PrimitiveType.Cube);
			wall.transform.position = mousePosSecond;
			wall.GetComponent<Renderer>().material.color = Color.grey;
		}
			
	}
}
