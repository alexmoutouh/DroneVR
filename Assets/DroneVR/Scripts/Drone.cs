using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
	public float Speed = 10f;
	public float RotationSpeed = 5000.0f;

    private Rigidbody rb;

    public Vector3 Direction {get; set;}
    public float Rot {get; set;}

	void Start() {
		this.Direction = new Vector3();
        this.rb = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate() {
        //this.transform.Translate(this.Direction * Time.deltaTime * this.Speed);
        Vector3 movement = this.Direction * this.Speed;
        movement.y += rb.mass * Mathf.Abs(Physics.gravity.y);
        rb.AddRelativeForce(movement);

        this.transform.Rotate(Vector3.up * Rot /** Time.deltaTime*/ * this.RotationSpeed);
    }
}
