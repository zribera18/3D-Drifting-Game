using UnityEngine;
using System.Collections;

public class SimpleCarScript : MonoBehaviour {

	public float maxTorque = 500f;
	public float maxSteerAngle = 45f;
	public WheelCollider[] wheelCollider = new WheelCollider[4];
	public Transform[] wheelMesh = new Transform[4];

	public Rigidbody rb;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().centerOfMass = new Vector3 (GetComponent<Rigidbody>().centerOfMass.x, -1.5f, GetComponent<Rigidbody>().centerOfMass.z);
	}

	// Update is called once per frame
	void Update () {
		UpdateMeshPosition();
	}

	void FixedUpdate () {

	  float steer = Input.GetAxis("Horizontal") * maxSteerAngle;
	  float torque = Input.GetAxis("Vertical") * maxTorque;

	  wheelCollider[0].steerAngle = steer;
	  wheelCollider[1].steerAngle = steer;

	  //Sets which wheels move forward or backwards.
	  //FWD
	  // for (var i = 0; i < 2; i++)
	  // {
	  //     wheelCollider[i].motorTorque = torque;
	  // }

	  //RWD
	  for (int i = 2; i < 4; i++)
	  {
	      wheelCollider[i].motorTorque = torque;
	  }

	  //AWD
	  // for (var i = 0; i < 4; i++)
	  // {
	  //     wheelCollider[i].motorTorque = torque;
	  // }

	  if (Input.GetKey("left shift")) {
	    maxTorque = 700.0f;
	    print("spaceeee");
	  } else {
	    maxTorque = 500.0f;
	  }

	  if (Input.GetKey(KeyCode.Space)) {
	    wheelCollider[2].brakeTorque = 1500.0f;
	    wheelCollider[3].brakeTorque = 1500.0f;

	  } else {
	    wheelCollider[2].brakeTorque = 0;
	    wheelCollider[3].brakeTorque = 0;
	  }
	}

	void UpdateMeshPosition()
	{
	    for (int i = 0; i < 4; i++)
	    {
	      Vector3 pos;
	      Quaternion quat;

	        //Gets the current position of the physics WheelColliders.
	        wheelCollider[i].GetWorldPose(out pos, out quat);

	        ///Sets the mesh to match the position and rotation of the physics WheelColliders.
	        wheelMesh[i].position = pos;
	        wheelMesh[i].rotation = quat;
	        Debug.Log(wheelMesh[i].rotation);
	    }
	}
}
