using UnityEngine;
using System.Collections;

public class DestructibleSkeleton : MonoBehaviour {

	public float maxHealth;
	private float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	public void Damage(float damage) {
		health -= damage;

		if (health <= 0.0f)
			Destroy (this.gameObject);

		float progress = (1.0f - Mathf.Min (1.0f, health / maxHealth));

		ParticleSystem s = GetComponent<ParticleSystem> ();
		if (s != null) {
			s.startSize = progress;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
