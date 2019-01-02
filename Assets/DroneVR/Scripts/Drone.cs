using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
	public float Speed = 5000.0f;
	public float RotationSpeed = 5000.0f;

	public Vector3 Direction {get; set;}
	/*public float X {get; set;}
	public float Y {get; set;}
	public float Z {get; set;}*/
	public float Rot {get; set;}

	void Start() {
		this.Direction = new Vector3();
	}
	
	void Update() {
		/*float xpos = this.transform.position.x;
		float ypos = this.transform.position.y;
		float zpos = this.transform.position.z;

		this.transform.position = new Vector3(
			xpos + X * Time.deltaTime * Speed, 
			ypos + Y * Time.deltaTime * Speed, 
			zpos + Z * Time.deltaTime * Speed
		);*/

		this.transform.Translate(this.Direction * Time.deltaTime * this.Speed);
		this.transform.Rotate(Vector3.up * Rot /** Time.deltaTime*/ * this.RotationSpeed);
	}
}
