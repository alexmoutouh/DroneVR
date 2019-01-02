using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using NewtonVR;

public class PlayerController : MonoBehaviour {
	public Text debug;
	public DroneController Controller;

	// Update is called once per frame
	void Update() {
		if(!Controller.Active) {
            Vector3 front = NVRPlayer.Instance.Head.transform.forward;
            front.y = 0;
            Vector3 right = NVRPlayer.Instance.Head.transform.right;
            right.y = 0;

            Vector2 leftAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
			
			this.transform.Translate((front * leftAxis.y + right * leftAxis.x) * Time.deltaTime * 5);
		}
		
		debug.text = "forward : " + NVRPlayer.Instance.Head.transform.forward + " right : " + NVRPlayer.Instance.Head.transform.right;
	}
}
