using UnityEngine;
using System.Collections;

public class SleepController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
}
