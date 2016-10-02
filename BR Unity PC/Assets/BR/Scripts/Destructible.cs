using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {
	public Material destroy1;
	public Material destroy2;
	public Material destroy3;
	public Material destroy4;
	public Material destroy5;
	public Material destroy6;
	public Material destroy7;
	public Material destroy8;
	public Material destroy9;

	private Material[] destroy;

	public float maxHealth;
	private float health;
	public float Health {
		get { return this.health; }
	}

	// Use this for initialization
	void Start () {
		health = maxHealth;
		destroy = new Material[] {
			destroy1,
			destroy2,
			destroy3,
			destroy4,
			destroy5,
			destroy6,
			destroy7,
			destroy8,
			destroy9
		};

		MeshRenderer r = GetComponent<MeshRenderer> ();
		Material[] mats = new Material[] { r.material };
		mats.SetValue (destroy [0], 1);
		r.materials = mats;
	}

	public void Damage (float damageBy) {
		health -= damageBy;

		if (health <= 0.0f) {
			Destroy (this.gameObject);
			return;
		}

		float progress = (1.0f - Mathf.Min (1.0f, health / maxHealth));
		int stage = Mathf.FloorToInt(progress * 9.0f);
		MeshRenderer r = GetComponent<MeshRenderer> ();
		// Debug.Log ("stage: " + stage);
		// Debug.Log ("destroy: " + destroy [stage]);
		Material[] mats = r.materials;
		mats.SetValue (destroy [stage], 1);
		r.materials = mats;

		ParticleSystem s = GetComponent<ParticleSystem> ();
		if (s != null) {
			s.startSize = progress;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}

