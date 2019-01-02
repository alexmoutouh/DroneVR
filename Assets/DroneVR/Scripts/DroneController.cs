using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NewtonVR;

public class DroneController : MonoBehaviour {
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
	void Update() {
		Vector3 direction = new Vector3();
		if(interact.AttachedHands.Count == 2) {
			this.Active = true;
			this.transform.SetParent(player.transform);

			// Déplacement du drone sur le plan (X, Z)
			Vector2 leftAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
			direction.x = leftAxis.x;
			direction.z = leftAxis.y;
			/*DroneControlled.X = leftAxis.x;
			DroneControlled.Z = leftAxis.y;*/

			// Montee, descente du drone
			float trigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger); 
			if(trigger > 0.0) {
				direction.y = -trigger;
			} else {
				trigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
				direction.y = trigger;
			}

			// rotation
			Vector2 rightAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);			
			DroneControlled.Rot = rightAxis.x;

			/*debug.text = "left sticks : " + leftAxis + " | right sticks : " + rightAxis + "\n" + 
			" Drone pos : " + this.DroneControlled.transform.position + " | Drone Rot : " + this.DroneControlled.transform.rotation;*/
			DroneControlled.Direction = direction;
		} else if(interact.AttachedHands.Count == 1) {
			this.Active = false;
			this.transform.SetParent(player.transform);
		} else {
			this.Active = false;
			this.transform.parent = null;
		}
	}
}
