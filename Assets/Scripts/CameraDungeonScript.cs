using UnityEngine;
using System.Collections;

// fixed camera position, keeps target in center of view
public class CameraDungeonScript : MonoBehaviour {

	public GameObject target;
	public float damping;
	public Vector3 offset;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void LateUpdate () {
		Vector3 desiredPosition = target.transform.position + offset;
		Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
		transform.position = position;

		transform.LookAt(target.transform.position);
	}
}
