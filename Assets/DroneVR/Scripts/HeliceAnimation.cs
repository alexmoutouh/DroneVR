using System.Collections;
using UnityEngine;

public class HeliceAnimation : MonoBehaviour {
    public bool Fly { get; private set; }
    private float angle = 0;

    void Start() {
        this.Fly = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.transform.Rotate(new Vector3(0, this.angle, 0));
    }

    private IEnumerator StartFlying() {
        while(angle < 100) {
            angle += 10f;

            if(angle > 50)
                this.Fly = true;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator StopFlying() {
        while(angle > 0) {
            angle -= 10f;

            if(angle < 50)
                this.Fly = false;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void TurnOnOff() {
        if(Fly)
            StartCoroutine(this.StopFlying());
        else
            StartCoroutine(this.StartFlying());
    }
}
