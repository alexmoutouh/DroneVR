using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceAnimation : MonoBehaviour {

	private bool fly = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {

		if (fly)
		{
			this.transform.Rotate(new Vector3(0, 99, 0));
		}
    }

	public void TurnOnOff()
	{
		fly = !fly;
	}
}
