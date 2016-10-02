using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Hunter : NetworkBehaviour {

	NavMeshAgent agent;
	float time = 0f;
	public float fov = 60.0f;

	bool SetDestination(Transform destination) {
		time = 0f;
		if (LineOfSight (destination)) {
			agent.SetDestination (destination.position);
			return agent.hasPath;
		} else
			return false;
	}

	bool LineOfSight (Transform target) {
		RaycastHit hit = new RaycastHit();
		return Vector3.Angle (target.position - transform.position, transform.forward) <= fov
			&& Physics.Linecast (transform.position, target.position, out hit)
			&& hit.collider.transform == target;
	}

	void OnCollisionEnter(Collision collider) {
		Debug.Log ("COllision enter");
		GameObject.Destroy (collider.gameObject);
	}

	void OnTriggerStay(Collider collider) {
		time += Time.deltaTime;
		if (time >= 0.25f) SetDestination (collider.transform);
	}

	void OnTriggerEnter(Collider collider) {
		SetDestination (collider.transform);
	}

	void OnTriggerExit(Collider collider) {
		time = 0f;
		agent.Stop ();
		agent.ResetPath ();
	}

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
	}

}
