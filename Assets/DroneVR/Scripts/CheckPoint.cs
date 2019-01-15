using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public bool isNext;
    public bool isFinished;

    public Color couleur;
    // Start is called before the first frame update
    void Start() {
        couleur = transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void FixedUpdate() {

    }

    public void UpdateCouleur() {
        if(!isNext && !isFinished) {
            couleur.a = 0.3f;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = couleur;
        } else if(isFinished) {
            couleur.a = 0.0f;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = couleur;
        } else {
            couleur.a = 1f;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = couleur;
        }
    }

}
