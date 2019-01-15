using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceAnimation : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        this.transform.Rotate(new Vector3(0, 50, 0));
        //this.transform.localRotation = Quaternion.Euler(0, 10, 0);
    }
}
