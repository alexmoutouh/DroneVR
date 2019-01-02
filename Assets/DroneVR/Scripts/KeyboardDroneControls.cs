using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardDroneControls : MonoBehaviour {

    public float speed = 20f;
    public float moveHorizontal;
    public float moveVertical;
    public float moveUp;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        moveHorizontal *= speed;
        moveVertical *= speed;

        if(Input.GetKey(KeyCode.Space)) {
            moveUp = 1.0f * speed / 2.0f;
        } else if(Input.GetKey(KeyCode.C)) {
            moveUp = -1.0f * speed / 2.0f;
        } else {
            moveUp = 0.0f;
        }

        Vector3 movement = new Vector3(moveHorizontal, (rb.mass * Mathf.Abs(Physics.gravity.y)) + moveUp, moveVertical);

        rb.AddForce(movement);
        //rb.AddRelativeForce(Vector3.up * (rb.mass * Mathf.Abs(Physics.gravity.y)));

    }
}
