using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {
	public KeyCode forward;
	public KeyCode backward;
	public KeyCode left;
	public KeyCode right;

	public KeyCode fire;
	public GameObject laserPrefab;
	public GameObject laserBlindnessPrefab;
	public float laserLength;
	public float verticalOffset;
	public float laserFadeTime;

	public float walkSpeed;

	private GameObject laser;
	private GameObject laserBlindness;
	private float laserFadeStart;
	private const int IGNORE_RAYCAST_LAYER = 2;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 origin = Camera.main.transform.position + Vector3.down * verticalOffset;
		Quaternion rotation = Camera.main.transform.rotation;

		Vector3 move = new Vector3 (0, 0, 0);
		if (Input.GetKey (forward))
			move += rotation * Vector3.forward;
		if (Input.GetKey (backward))
			move += rotation * Vector3.back;
		if (Input.GetKey (left))
			move += rotation * Vector3.left;
		if (Input.GetKey (right))
			move += rotation * Vector3.right;

		move = move * Time.deltaTime * walkSpeed;
		GetComponent<CharacterController>().Move(move);

		if (Input.GetKeyDown (fire) || GvrController.ClickButtonDown) {
			if (laser != null)
				Destroy (laser);
			if (laserBlindness != null)
				Destroy (laserBlindness);

			Ray ray = new Ray ();
			ray.origin = origin;
			ray.direction = rotation * Vector3.forward;

			int ignoreRaycastMask = ~(1 << IGNORE_RAYCAST_LAYER);

			float distance;
			RaycastHit info = new RaycastHit ();
			if (Physics.Raycast (ray, out info, laserLength, ignoreRaycastMask)) {
				distance = info.distance;
			} else {
				distance = laserLength;
			}

			laser = Instantiate (laserPrefab);
			laser.transform.position = (origin + (ray.direction * distance / 2.0f));
			laser.transform.rotation = rotation * Quaternion.FromToRotation(Vector3.up, Vector3.forward);
			laser.transform.localScale = new Vector3 (0.2f, distance, 0.2f);

			laserBlindness = Instantiate (laserBlindnessPrefab);
			laserBlindness.transform.parent = Camera.main.transform;
			laserBlindness.transform.position = (origin + (ray.direction * 0.5f));
			laserBlindness.transform.rotation = rotation;
			laserBlindness.transform.localScale = new Vector3 (2.0f, 2.0f, 0.1f);
			laserFadeStart = Time.timeSinceLevelLoad;

			//Debug.Log ("Distance: " + info.distance);
			//Debug.Log ("Direction: " + ray.direction);

		} else {
			if (laser != null)
				Destroy (laser);

			if (laserBlindness != null) {
				float a = 1 - ((Time.timeSinceLevelLoad - laserFadeStart) / laserFadeTime);
				Material mat = laserBlindness.GetComponent<MeshRenderer> ().material;
				Color col = mat.color;
				col.a = a;
				mat.color = col;

				// Debug.Log ("A: " + mat.color.a);

				if (mat.color.a <= 0.0f) {
					Destroy (laserBlindness);
				}
			}
		}
	}
}
