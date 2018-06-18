using UnityEngine;
using System.Collections;
// follows target with aeriel view, used for top down 
public class CameraLookAtScript : MonoBehaviour {

	public Transform target;
	public Vector3 offset;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = target.position + offset;
		transform.LookAt(target.transform);
	}
}
