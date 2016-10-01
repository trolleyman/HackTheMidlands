using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Hunter : NetworkBehaviour {

	float time = 0.0f;
	public NavMeshAgent agent;
	public GameObject player;

	// Use this for initialization
	void Start () {
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 1000f)
			HuntPlayer ();
	}

	void HuntPlayer () {
		agent.SetDestination (player.transform.position);
	}
}
