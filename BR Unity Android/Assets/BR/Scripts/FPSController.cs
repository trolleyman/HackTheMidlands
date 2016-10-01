using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {
	public KeyCode forward;
	public KeyCode backward;
	public KeyCode left;
	public KeyCode right;

	public KeyCode fire;
	public GameObject laserPrefab;
	public float laserLength;
	public float laserDPS;
	public float verticalOffset;
	public float horizontalOffset;

	public float walkSpeed;

	private GameObject laser;
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

		if (Input.GetKey (fire) || GvrController.ClickButton) {
			if (laser != null)
				Destroy (laser);

			Ray ray = new Ray ();
			ray.origin = origin;
			ray.direction = rotation * Vector3.forward;

			int ignoreRaycastMask = ~(1 << IGNORE_RAYCAST_LAYER);

			float distance;
			RaycastHit info = new RaycastHit ();
			if (Physics.Raycast (ray, out info, laserLength, ignoreRaycastMask)) {
				distance = info.distance;
				Destructible d = info.collider.gameObject.GetComponent(typeof(Destructible)) as Destructible;
				//Debug.Log (d);
				if (d != null) {
					d.Damage (laserDPS * Time.deltaTime);
					Debug.Log (d.Health);
				}
			} else {
				distance = laserLength;
			}
			distance += horizontalOffset;

			laser = Instantiate (laserPrefab);
			laser.transform.position = (origin + (ray.direction * (distance / 2.0f - horizontalOffset)));
			laser.transform.rotation = rotation * Quaternion.FromToRotation(Vector3.up, Vector3.forward);
			laser.transform.localScale = new Vector3 (0.2f, distance, 0.2f);

			//Debug.Log ("Distance: " + info.distance);
			//Debug.Log ("Direction: " + ray.direction);

		} else {
			if (laser != null)
				Destroy (laser);
		}
	}
}
