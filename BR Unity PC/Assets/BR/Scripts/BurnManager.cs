using UnityEngine;
using System.Collections;

public class BurnManager : MonoBehaviour {
	private float flameLevel;

	// Use this for initialization
	void Start () {
		UpdateFlameLevel (0.0f);
	}

	void UpdateFlameLevel(float newLevel) {
		flameLevel = newLevel;

		ParticleSystem ps = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		UpdateFlameLevel (Mathf.Sin(Time.realtimeSinceStartup));
	}
}
