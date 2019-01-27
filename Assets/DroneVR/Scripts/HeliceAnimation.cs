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
        if(this.angle != 0)
            this.transform.Rotate(new Vector3(0, this.angle, 0));
    }

    /// <summary>
    /// Coroutine de lancement de l'helice. Controle la vitesse de rotation de l'helice.
    /// </summary>
    private IEnumerator StartFlying() {
        while(angle < 100) {
            angle += 10f;

            if(angle > 50)
                this.Fly = true;

            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Coroutine de l'arret de l'helice. Controle la vitesse de rotation de l'helice.
    /// </summary>
    private IEnumerator StopFlying() {
        while(angle > 0) {
            angle -= 10f;

            if(angle < 50)
                this.Fly = false;

            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Demarre/arrete l'helice.
    /// </summary>
    public void TurnOnOff() {
        if(Fly)
            StartCoroutine(this.StopFlying());
        else
            StartCoroutine(this.StartFlying());
    }
}
