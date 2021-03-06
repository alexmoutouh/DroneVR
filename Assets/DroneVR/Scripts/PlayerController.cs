﻿using UnityEngine.UI;
using UnityEngine;
using NewtonVR;

public class PlayerController : MonoBehaviour {
    public bool VRBehaviour;
    public Text debug;
    public DroneController Controller;

    // Update is called once per frame
    void FixedUpdate() {
        if(VRBehaviour) {
            if(!Controller.Active) {
                Vector3 front = NVRPlayer.Instance.Head.transform.forward;
                front.y = 0;
                Vector3 right = NVRPlayer.Instance.Head.transform.right;
                right.y = 0;

                Vector2 leftAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

                this.transform.Translate((front * leftAxis.y + right * leftAxis.x) * Time.deltaTime * 2, Space.Self);
            }
        } else {
            float moveUp = 0.0f;
            if(Input.GetKey(KeyCode.P))
                moveUp = 5;
            else if(Input.GetKey(KeyCode.M))
                moveUp = -5;
            else
                moveUp = 0.0f;

            float moveFront = 0.0f;
            if(Input.GetKey(KeyCode.I))
                moveFront = 5;
            else if(Input.GetKey(KeyCode.K))
                moveFront = -5;
            else
                moveFront = 0.0f;

            float moveRight = 0.0f;
            if(Input.GetKey(KeyCode.J))
                moveRight = -5;
            else if(Input.GetKey(KeyCode.L))
                moveRight = 5;
            else
                moveRight = 0.0f;

            this.transform.Translate(new Vector3(moveRight, moveUp, moveFront) * Time.deltaTime);

            if(Input.GetKey(KeyCode.U))
                this.transform.Rotate(-Vector3.up, Space.World);
            else if(Input.GetKey(KeyCode.O))
                this.transform.Rotate(Vector3.up, Space.World);
        }
    }
}
