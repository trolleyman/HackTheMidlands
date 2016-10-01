using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Builder : MonoBehaviour {

	public NavMeshAgent agent;

	public bool PathExists(NavMeshAgent playerAgent, Transform destination) {
		agent.SetDestination (destination.position);
		return agent.hasPath;
	}

	public bool CanBuildAt(Transform location) {
		return PathExists (agent, location);
	}

}