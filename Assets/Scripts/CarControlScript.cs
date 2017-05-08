﻿using UnityEngine;
using System.Collections;

public class CarControlScript : MonoBehaviour {

		public WheelCollider FrontLeftWheel;
		public WheelCollider FrontRightWheel;

		// These variables are for the gears, the array is the list of ratios. The script
		// uses the defined gear ratios to determine how much torque to apply to the wheels.
		public float[] GearRatio;
		int CurrentGear = 0;

		// These variables are just for applying torque to the wheels and shifting gears.
		// using the defined Max and Min Engine RPM, the script can determine what gear the
		// car needs to be in.
		public float EngineTorque = 600.0f;
		public float MaxEngineRPM = 3000.0f;
		public float MinEngineRPM = 1000.0f;

		private float EngineRPM = 0.0f;

		private Rigidbody rigidBody;
		private AudioSource audioSource;


		void  Start (){

			rigidBody = GetComponent<Rigidbody> ();
			audioSource = GetComponent<AudioSource> ();

			// I usually alter the center of mass to make the car more stable. I'ts less likely to flip this way.
			rigidBody.centerOfMass = new Vector3 (rigidBody.centerOfMass.x, -1.5f, rigidBody.centerOfMass.z);

			// Needed for better RPM readings for heavy vehciles
			FrontLeftWheel.ConfigureVehicleSubsteps (5f, 10, 10);
		}

		void  FixedUpdate (){

			// // This is to limith the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
			// // but it's easy, and it doesn't interfere with the physics processing.
			// rigidBody.drag = rigidBody.velocity.magnitude / 500;

			// // Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function
			// EngineRPM = (FrontLeftWheel.rpm + FrontRightWheel.rpm)/2 * GearRatio[CurrentGear];
			 //ShiftGears();

			// // set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
			// // up to twice it's pitch, where it will suddenly drop when it switches gears.
			// audioSource.pitch = Mathf.Clamp (Mathf.Abs(EngineRPM / MaxEngineRPM) + 1.0f, 0f, 2.0f) ;

			// finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
			// multiplied by the user input variable.
			 FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
			// FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");

			//FrontLeftWheel.motorTorque = Input.GetAxis("Vertical") * EngineTorque * Time.deltaTime * 250.0;

			// the steer angle is an arbitrary value multiplied by the user input.
			FrontLeftWheel.steerAngle = 10 * Input.GetAxis("Horizontal");
			FrontRightWheel.steerAngle = 10 * Input.GetAxis("Horizontal");
		}

		void  ShiftGears (){
			// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
			// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
			int AppropriateGear = CurrentGear;

			if ( EngineRPM >= MaxEngineRPM ) {

				for ( int i= 0; i < GearRatio.Length; i ++ ) {
					if ( FrontLeftWheel.rpm * GearRatio[i] < MaxEngineRPM ) {
						AppropriateGear = i;
						break;
					}
				}

				CurrentGear = AppropriateGear;
			}

			if ( EngineRPM <= MinEngineRPM ) {
				AppropriateGear = CurrentGear;

				for ( int j= GearRatio.Length-1; j >= 0; j -- ) {
					if ( FrontLeftWheel.rpm * GearRatio[j] > MinEngineRPM ) {
						AppropriateGear = j;
						break;
					}
				}

				CurrentGear = AppropriateGear;
			}
		}
}
