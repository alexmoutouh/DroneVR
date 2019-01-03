using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NewtonVR;

public class DroneController : MonoBehaviour {
    public bool VRBehaviour = false; // Controles VR 

    public Text debug;
	public bool Active {get; private set;}

	private GameObject player;
	private NVRInteractableItem interact;

	public Drone DroneControlled;

	// Use this for initialization
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		interact = this.GetComponent<NVRInteractableItem>();
	}

	// Update is called once per frame
	void FixedUpdate() {
        Vector3 direction = new Vector3();
        if(VRBehaviour) {
            if(interact.AttachedHands.Count == 2) {
                this.Active = true;
                this.transform.SetParent(player.transform);

                // Déplacement du drone sur le plan (X, Z)
                Vector2 leftAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                direction.x = leftAxis.x;
                direction.z = leftAxis.y;

                // Montee, descente du drone
                float trigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
                if(trigger > 0.0) {
                    direction.y = -trigger;
                } else {
                    trigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
                    if(trigger > 0.0) {
                        direction.y = trigger;
                    }
                }
                
                DroneControlled.Direction = direction;

                // rotation
                Vector2 rightAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
                DroneControlled.Rot = rightAxis.x;

                debug.text = "direction : " + direction + "\n" +
                " Drone pos : " + this.DroneControlled.transform.position + " | Drone Rot : " + this.DroneControlled.transform.rotation;
            } else if(interact.AttachedHands.Count == 1) {
                this.Active = false;
                this.transform.SetParent(player.transform);
            } else {
                this.Active = false;
                this.transform.parent = null;
            }
        } else {

			float moveHorizontal;    // Input mouvement horizontale
			float moveVertical;      // Input mouvement verticale
			float moveUp;            // Input monter / descendre
			float moveSpin;          // Input tourner le drone sur lui meme

			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");

			if (Input.GetKey(KeyCode.Space))
				moveUp = 50.0f * DroneControlled.SPEED;

			else if (Input.GetKey(KeyCode.C))
				moveUp = -50.0f * DroneControlled.SPEED;

			else
				moveUp = 0.0f;

			if (Input.GetKey(KeyCode.Q))
				moveSpin = -2.0f;

			else if (Input.GetKey(KeyCode.E))
				moveSpin = 2.0f;

			else
				moveSpin = 0.0f;

			DroneControlled.ApplyForces(moveHorizontal, moveVertical, moveUp, moveSpin);
        }
	}
}
