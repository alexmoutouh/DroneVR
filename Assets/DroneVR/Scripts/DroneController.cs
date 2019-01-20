using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NewtonVR;

public class DroneController : MonoBehaviour {
    public bool VRBehaviour = false; // Controles VR 

    public Text debug;
    public bool Active { get; private set; }

    private bool taken;
    private Vector3 initialPos;
    private Quaternion initialRot;
    private GameObject player;
    private NVRInteractableItem interact;

    public Drone DroneControlled;

    private void ResetController() {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        this.transform.position = this.initialPos;
        this.transform.rotation = this.initialRot;
    }

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        interact = this.GetComponent<NVRInteractableItem>();

        this.taken = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.initialPos = this.transform.position;
        this.initialRot = this.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 movement = new Vector3();
        float rot = 0.0f;

        if(VRBehaviour) {
            if(interact.AttachedHands.Count == 2) {
                this.Active = true;
                this.transform.SetParent(player.transform);

                // Déplacement du drone sur le plan (X, Z)
                Vector2 leftAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                movement.x = leftAxis.x;
                movement.z = leftAxis.y;

                // Montee, descente du drone
                float trigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
                if(trigger > 0.0) {
                    movement.y = 50f * -trigger;
                } else {
                    trigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
                    if(trigger > 0.0) {
                        movement.y = 50f * trigger;
                    } else {
                        movement.y = 0f;
                    }
                }

                // rotation
                Vector2 rightAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
                rot = rightAxis.x;

                debug.text = "direction : " + movement + "\n" +
                " Drone pos : " + this.DroneControlled.transform.position + " | Drone Rot : " + this.DroneControlled.transform.rotation;
            } else if(interact.AttachedHands.Count == 1) {
                this.Active = false;
                this.transform.SetParent(player.transform);
                this.GetComponent<Rigidbody>().useGravity = true;
            } else {
                this.Active = false;
                this.transform.parent = null;

                if(OVRInput.Get(OVRInput.Button.One)) {
                    this.ResetController();
                }
            }
        } else {
			if (Input.GetKey(KeyCode.B))
				DroneControlled.TurnOnOff();

			float moveUp; // Input monter / descendre
            if(Input.GetKey(KeyCode.Space))
                moveUp = 50.0f * DroneControlled.Speed;
            else if(Input.GetKey(KeyCode.C))
                moveUp = -50.0f * DroneControlled.Speed;
            else
                moveUp = 0.0f;

            movement = new Vector3(Input.GetAxis("Horizontal"), moveUp, Input.GetAxis("Vertical"));

            if(Input.GetKey(KeyCode.A))
                rot = -2.0f;
            else if(Input.GetKey(KeyCode.E))
                rot = 2.0f;
            else
                rot = 0.0f;
        }

        DroneControlled.ApplyForces(movement, rot);
    }
}
