using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseClick : MonoBehaviour {

	public GameObject StrongWall;
	public GameObject WeakWall;
	public GameObject Wall;
	public GameObject Goal;
	public GameObject Monster;
	public GameObject MonsterMarker;
	public GameObject Empty;

	private bool isDown1;
	private Vector3 mousePosFirst;
	private Vector3 mousePosSecond;

	private GameObject[,] board;
	private const int BOARD_X_OFFSET = 10;
	private const int BOARD_Z_OFFSET = 5;
	private const int BOARD_W = 20;
	private const int BOARD_H = 10;

	private int CURRENT_WWALL = 30;
	private int CURRENT_SWALL = 20;
	private int CURRENT_GOAL = 1;
	private int CURRENT_MONSTER = 1;

	private const int sWall = 1, wWall = 2, goal = 3, monster = 4;
	private int currentBlock = sWall;


	// Use this for initialization
	void Start () {
		board = new GameObject[BOARD_W, BOARD_H];

		for (int x = 0; x < BOARD_W; x++) {
			for (int z = 0; z < BOARD_H; z++) {
				board [x, z] = null;
			}
		}

		for (int x = 0; x < BOARD_W; x++) {
			createObject(x, 0, Wall);
			Debug.Log (x + ", 0");
		}
		for (int x = 0; x < BOARD_W; x++) {
			createObject(x, BOARD_H - 1, Wall);
			Debug.Log (x + ", 9");
		}
		for (int z = 0; z < BOARD_H; z++) {
			createObject(0, z, Wall);
		}
		for (int z = 0; z < BOARD_H; z++) {
			createObject(BOARD_W - 1, z, Wall);
		}

	}

	void createObject(int x, int z, GameObject go) {
		if (board [x, z] == null) {

			if (go == StrongWall && CURRENT_SWALL > 0) {
				board [x, z] = buildStrongWall (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
				CURRENT_SWALL--;
			} else if (go == WeakWall && CURRENT_WWALL > 0) {
				board [x, z] = buildWeakWall (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
				CURRENT_WWALL--;
			} else if (go == Goal && CURRENT_GOAL > 0) {
				board [x, z] = buildGoal (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
				CURRENT_GOAL--;
			} else if (go == Wall) {
				board [x, z] = buildWall (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
			} else if (go == Monster && CURRENT_MONSTER > 0) {
				board [x, z] = buildMonster (x - BOARD_X_OFFSET, z - BOARD_Z_OFFSET);
				CURRENT_MONSTER--;
			} else if (go == Empty) {
				Debug.Log ("Nothing to remove");
			}
		} else {
			if (go == Empty && (board [x, z].tag != "Invulnerable")) {
				if (board [x, z].tag == "WeakWall") {
					CURRENT_WWALL++;
				} else if (board [x, z].tag == "StrongWall") {
					CURRENT_SWALL++;
				} else if (board [x, z].tag == "Goal") {
					CURRENT_GOAL++;
				} else if (board [x, z].tag == "Monster") {
					CURRENT_MONSTER++;
				}
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

	public GameObject buildWall(float x, float z) {
		Vector3 pos = new Vector3(x, 0.0f, z);
		GameObject wall = Instantiate (Wall, pos, Quaternion.identity) as GameObject;
		return wall;
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

	public GameObject buildGoal(float x, float z) {
		Vector3 pos = new Vector3(x, 0.0f, z);
		GameObject gl = Instantiate (Goal, pos, Quaternion.identity) as GameObject;
		return gl;
	}

	public GameObject buildMonster(float x, float z) {
		Vector3 pos = new Vector3(x, 0.0f, z);
		GameObject mon = Instantiate (Monster, pos, Quaternion.identity) as GameObject;
		pos.y = -0.95f;
		GameObject monMark = Instantiate (MonsterMarker, pos, Quaternion.identity) as GameObject;
		mon.transform.parent = monMark.transform;
		return monMark;
	}

	public GameObject intToGO(int current) {
		if (current == sWall) {
			return StrongWall;
		} else if (current == wWall) {
			return WeakWall;
		} else if (current == goal) {
			return Goal;
		} else {
			return Monster;
		}
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

			createObject (x + BOARD_X_OFFSET, z + BOARD_Z_OFFSET, intToGO(currentBlock));
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
		if (Input.GetKeyDown ("1")) {
			currentBlock = sWall;
			Debug.Log ("Strong wall selected");
		}
		if (Input.GetKeyDown ("2")) {
			currentBlock = wWall;
			Debug.Log ("Weak wall selected");
		}
		if (Input.GetKeyDown ("3")) {
			currentBlock = goal;
			Debug.Log ("Goal selected");
		}
		if (Input.GetKeyDown ("4")) {
			currentBlock = monster;
			Debug.Log ("Monster selected");
		}
	}
}
