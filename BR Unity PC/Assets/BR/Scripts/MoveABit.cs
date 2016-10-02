using UnityEngine;
using System.Collections;

public class MoveABit : MonoBehaviour {

	private Vector3 m_Start;
	private float m_Time;

	// Use this for initialization
	void Start () {
		m_Start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		m_Time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		m_Time += Time.deltaTime;
		transform.position = new Vector3 (
			m_Start.x + Mathf.Sin (m_Time),
			m_Start.y + Mathf.Cos (m_Time * 1.1f),
			m_Start.z + Mathf.Sin (m_Time * 0.8f)
		);
	}
}
