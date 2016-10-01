using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	public GameObject StrongWall;
	public GameObject WeakWall;
	public GameObject Wall;
	public GameObject Goal;
	public GameObject Empty;

	private bool isDown1;
	private Vector3 mousePosFirst;
	private Vector3 mousePosSecond;

	private GameObject[,] board;
	private const int BOARD_X_OFFSET = 10;
	private const int BOARD_Z_OFFSET = 5;
	private const int BOARD_W = 20;
	private const int BOARD_H = 10;


	// Use this for initialization
	void Start () {
		board = new GameObject[BOARD_W, BOARD_H];

		for (int x = 0; x < BOARD_W; x++) {
			for (int z = 0; z < BOARD_H; z++) {
				board [x, z] = null;
			}
		}

		for (int x = 0; x < BOARD_W; x++) {
			createObject(x, 0, StrongWall);
			Debug.Log (x + ", 0");
		}
		for (int x = 0; x < BOARD_W; x++) {
			createObject(x, BOARD_H - 1, StrongWall);
			Debug.Log (x + ", 9");
		}
		for (int z = 0; z < BOARD_H; z++) {
			createObject(0, z, StrongWall);
		}
		for (int z = 0; z < BOARD_H; z++) {
			createObject(BOARD_W - 1, z, StrongWall);
		}
	}

	void createObject(int x, int z, GameObject go) {
		if (board [x, z] == null) {

			if (go == StrongWall) {
				board [x, z] = buildStrongWall (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
			} else if (go == WeakWall) {
				board [x, z] = buildWeakWall (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
			} else if (go == Empty) {
				Debug.Log ("Nothing to remove");
			}
		} else {
			if (go == Empty) {
				board [x, z] = removeWall (x, z);
			} else {
				Debug.Log ("Not a valid position");
			}
		}
			
	}

	public GameObject removeWall(int x, int y) {
		Destroy (board [x, y]);
		return null;
	}

	public GameObject buildStrongWall(float x, float z) {
		Vector3 pos = new Vector3(x, 0.0f, z);
		GameObject strongWall = Instantiate (StrongWall, pos, Quaternion.identity) as GameObject;
		return strongWall;
	}

	public GameObject buildWeakWall(float x, float z) {
		Vector3 pos = new Vector3(x, 0.0f, z);
		GameObject weakWall = Instantiate (WeakWall, pos, Quaternion.identity) as GameObject;
		return weakWall;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			var mousePosSecond = Input.mousePosition;
			mousePosSecond.z = 10.0f;
			mousePosSecond = Camera.main.ScreenToWorldPoint(mousePosSecond);
			int x = (int)Mathf.Round(mousePosSecond.x);
			int z = (int)Mathf.Round(mousePosSecond.z);
			Debug.Log ("Second point recorded" + mousePosSecond);

			createObject (x + BOARD_X_OFFSET, z + BOARD_Z_OFFSET, WeakWall);
		}
		if (Input.GetMouseButton (1)) {
			var mousePosSecond = Input.mousePosition;
			mousePosSecond.z = 10.0f;
			mousePosSecond = Camera.main.ScreenToWorldPoint(mousePosSecond);
			int x = (int)Mathf.Round(mousePosSecond.x);
			int z = (int)Mathf.Round(mousePosSecond.z);
			Debug.Log ("Second point recorded" + mousePosSecond);

			createObject (x + BOARD_X_OFFSET, z + BOARD_Z_OFFSET, Empty);
		}
	}
}
