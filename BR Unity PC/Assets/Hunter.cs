using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Hunter : NetworkBehaviour {

	NavMeshAgent agent;

	void OnTriggerEnter(Collider collider) {
		agent.SetDestination (collider.transform.position);
	}

	void OnTriggerExit(Collider collider) {
		agent.Stop ();
		agent.ResetPath ();
	}

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
	}

}
