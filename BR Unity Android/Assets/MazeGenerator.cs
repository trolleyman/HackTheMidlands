using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoardPiece {
	EMPTY,
	STRONG_WALL,
	WEAK_WALL,
	MONSTER_SPAWNER
}

public struct IVector2 {
	public int x;
	public int y;

	public IVector2(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public bool Equals(Object o) {
		return o is IVector2 && this == (IVector2)o;
	}
};

public class MazeGenerator : MonoBehaviour {

	public GameObject mazeParent;
	public GameObject strongWallPrefab;
	public GameObject weakWallPrefab;

	private BoardPiece[,] board;
	private const int BOARD_W = 6;
	private const int BOARD_H = 6;

	private const int START_X = 0;
	private const int START_Y = 0;

	private const int GOAL_X = BOARD_W;
	private const int GOAL_Y = BOARD_H / 2;

	private IVector2 goal;

	// Use this for initialization
	void Start () {
		goal = new IVector2 (GOAL_X, GOAL_Y);
		board = new BoardPiece[BOARD_W, BOARD_H];
		for (int x = 0; x < BOARD_W; x++)
			for (int y = 0; y < BOARD_H; y++)
				board [x, y] = BoardPiece.STRONG_WALL;

		Stack<IVector2> s = new Stack<IVector2> ();
		s.Push(new IVector2(START_X, START_Y));
		while (s.Count != 0) {
			IVector2 pos = s.Pop ();

			Debug.Log ("pos: " + pos.x + "," + pos.y + ", BOARD_W:" + BOARD_W);

			int numEmptyAdjacent = 0;
			if (pos.x != 0 && board [pos.x - 1, pos.y] == BoardPiece.EMPTY)
				numEmptyAdjacent++;
			if (pos.y != 0 && board [pos.x, pos.y - 1] == BoardPiece.EMPTY)
				numEmptyAdjacent++;
			if (pos.x != BOARD_W - 1 && board [pos.x + 1, pos.y] == BoardPiece.EMPTY)
				numEmptyAdjacent++;
			if (pos.y != BOARD_H - 1 && board [pos.x, pos.y + 1] == BoardPiece.EMPTY)
				numEmptyAdjacent++;
			
			if (numEmptyAdjacent <= 1)
				GetNeighbours (pos, s);
		}

		// Display maze
		for (int x = 0; x < BOARD_W; x++) {
			for (int y = 0; y < BOARD_H; y++) {
				switch (board[x, y]) {
				case BoardPiece.STRONG_WALL: {
						GameObject strongWall = Instantiate (strongWallPrefab, new Vector3 (x, 1.0f, y), Quaternion.identity) as GameObject;
						strongWall.transform.parent = mazeParent.transform;
						break;
					}
				case BoardPiece.WEAK_WALL: {
						GameObject weakWall = Instantiate (weakWallPrefab, new Vector3 (x, 1.0f, y), Quaternion.identity) as GameObject;
						weakWall.transform.parent = mazeParent.transform;
						break;
					}
				default:
					break;
				}
			}
		}
	}

	void GetNeighbours(IVector2 pos, Stack<IVector2> s) {
		int i = 0;
		IVector2[] vs = new IVector2[4];

		if (pos.x > 0) {
			vs[i] = new IVector2(pos.x - 1, pos.y);
			if (vs [i].Equals(goal))
				s.Push (vs [i]);
			else
				i += 1;
		}
		if (pos.y > 0) {
			vs[i] = new IVector2(pos.x, pos.y - 1);
			if (vs [i].Equals(goal))
				s.Push (vs [i]);
			else
				i += 1;
		}
		if (pos.x < BOARD_W - 1) {
			vs[i] = new IVector2(pos.x + 1, pos.y);
			if (vs [i].Equals(goal))
				s.Push (vs [i]);
			else
				i += 1;
		}
		if (pos.y < BOARD_H - 1) {
			vs[i] = new IVector2(pos.x, pos.y + 1);
			if (vs [i].Equals(goal))
				s.Push (vs [i]);
			else
				i += 1;
		}

		Shuffle (vs, i);
		for (int j = 0; j < i; j++) {
			s.Push (vs [j]);
		}
	}

	void Shuffle<T> (T[] array, int n) {
		while (n > 1) {
			int k = Mathf.Min(Mathf.RoundToInt(Random.value * (n)), n - 1);
			Debug.Log ("k: " + k + ", n: " + n);
			n--;
			T temp = array [n];
			array [n] = array [k];
			array [k] = temp;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
