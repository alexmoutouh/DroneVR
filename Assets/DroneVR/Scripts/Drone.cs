using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
    public bool IsFlying { get; private set; }
    private float cooldown = 0f; // temps d'attente restant avant le prochain on/off du drone possible

    private Rigidbody rb;
    private AudioSource sonDrone;
    private List<HeliceAnimation> helices;

    public float Speed = 1f;   // vitesse du drone
    public float MaxTilt = 50f;    // Inclinaison max du drone
    public float Stability = 20f;   // Niveau de stabilité du drone
    public Camera camera; // camera avant du drone

    private Variables variables = new Variables();

    private void FixRanges(ref Vector3 euler) {
        if(euler.x < -180)
            euler.x += 360;
        else if(euler.x > 180)
            euler.x -= 360;

        if(euler.y < -180)
            euler.y += 360;
        else if(euler.y > 180)
            euler.y -= 360;

        if(euler.z < -180)
            euler.z += 360;
        else if(euler.z > 180)
            euler.z -= 360;
    }

    /// <summary>
    /// Coroutine du lancement du drone. Controle le fadein du son et l'etat isFliying du drone.
    /// </summary>
    private IEnumerator StartFlying() {
        sonDrone.Play();
        this.sonDrone.volume = 0;
        while(this.sonDrone.volume < 1) {
            this.sonDrone.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        IsFlying = true;
    }

    /// <summary>
    /// Coroutine de l'arret du drone. Controle le fadeout du son et l'etat isFliying du drone.
    /// </summary>
    private IEnumerator StopFlying() {
        this.sonDrone.volume = 1;
        while(this.sonDrone.volume > 0) {
            this.sonDrone.volume -= 0.1f;

            if(this.sonDrone.volume < 0.5f)
                IsFlying = false;

            yield return new WaitForSeconds(0.1f);
        }

        sonDrone.Stop();
    }

    /// <summary>
    /// Allume/eteint le drone.
    /// </summary>
    public void TurnOnOff() {
        if(this.cooldown > 0)
            return;

        this.cooldown = 5f;

        foreach(HeliceAnimation h in helices) {
            h.TurnOnOff();
        }

        if(IsFlying) {
            StartCoroutine(StopFlying());
        } else {
            sonDrone.time = 2f;
            StartCoroutine(StartFlying());
        }
    }

    /// <summary>
    /// Effectue la rotation de la camera du drone.
    /// </summary>
    /// <param name="axis">Angles d'euler</param>
    public void RotateCamera(Vector3 axis) {
        if((-0.7 <= this.camera.transform.localRotation.x && this.camera.transform.localRotation.x <= 0.7) ||
        (this.camera.transform.localRotation.x <= -0.7 && axis.x > 0) ||
        (0.7 <= this.camera.transform.localRotation.x && axis.x < 0)) {
            this.camera.transform.Rotate(axis, Space.Self);
        }
    }

    /// <summary>
    /// Applique les forces de mouvement et les rotations au drone.
    /// </summary>
    /// <param name="movement">Vecteur3 de mouvement</param>
    /// <param name="rot">composante y du vecteur de rotation</param>
    public void ApplyForces(Vector3 movement, float rot) {
        if(!IsFlying)
            return;

        //Stabilise le drone lorsqu'il bouge sur tous les axes
        Vector3 orientation = rb.transform.localRotation.eulerAngles;
        orientation.y = 0;
        FixRanges(ref orientation);

        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 right = new Vector3(1, 0, 0);
        Vector3 front = new Vector3(0, 0, 1);
        Vector3 rear = new Vector3(0, 0, -1);

        //Position		gauche, droite, avant, arriere		du drone
        Vector3 pos_left = rb.transform.position + rb.transform.TransformDirection(left);
        Vector3 pos_right = rb.transform.position + rb.transform.TransformDirection(right);
        Vector3 pos_front = rb.transform.position + rb.transform.TransformDirection(front);
        Vector3 pos_rear = rb.transform.position + rb.transform.TransformDirection(rear);

        //Quand le drone se deplace, il s'incline et baisse en Y. On doit alors compenser en force Y pour qu'il ne se baisse pas, il doit rester à la meme hauteur
        float mov_y = movement.y - (rb.velocity.y * 100f) + (rb.mass * Mathf.Abs(Physics.gravity.y));
        if(mov_y < 0) mov_y = 0;

        //Vitesse de rotation (locale) du drone
        Vector3 localAngularVelocity = rb.transform.InverseTransformDirection(rb.angularVelocity);

        //Vitesse de deplacement désirée sur		GAUCHE / DROITE / AVANT / ARRIERE		.
        float desiredRight = movement.x + ((orientation.z + localAngularVelocity.z * Stability) / MaxTilt); // Calcul de l'angle	gauche	selon l'orientation et la vitesse de rotation du drone
        float desiredLeft = -movement.x - ((orientation.z + localAngularVelocity.z * Stability) / MaxTilt); // Calcul de l'angle	droit	selon l'orientation et la vitesse de rotation du drone
        float desiredRear = -movement.z + ((orientation.x + localAngularVelocity.x * Stability) / MaxTilt); // Calcul de l'angle	avant	selon l'orientation et la vitesse de rotation du drone 
        float desiredFront = movement.z - ((orientation.x + localAngularVelocity.x * Stability) / MaxTilt); // Calcul de l'angle	arriere	selon l'orientation et la vitesse de rotation du drone

        //Vitesse de rotation du drone sur lui-meme sur l'axe Y
        float desiredSpin = rot - localAngularVelocity.y;

        //Calcul de la force a appliquer au drone par la gauche / la droite / l'avant / l'arriere
        //On applique la force par rapport a l'axe Y (vert) local du drone
        //CONSTAMMENT, le drone est poussé par 4 force : GAUCHE/DROITE/AVANT/ARRIERE. Donc pour qu'il ne monte pas, on lui ajoute, pour chaque force, 1/4 de force vers le haut, pour qu'il flotte
        Vector3 force_left = rb.transform.up * (mov_y / 4f + desiredLeft);
        Vector3 force_right = rb.transform.up * (mov_y / 4f + desiredRight);
        Vector3 force_front = rb.transform.up * (mov_y / 4f + desiredFront);
        Vector3 force_rear = rb.transform.up * (mov_y / 4f + desiredRear);

        //Vitesse de deplacement du drone
        force_left.x *= Speed;
        force_right.x *= Speed;
        force_front.z *= Speed;
        force_rear.z *= Speed;

        //Pour que le drone se DEPLACE et s'INCLINE en meme temps -> AddForceAtPosition
        //Par exemple, si a GAUCHE du drone, je lui ajoute une force vers la DROITE, le drone ira vers la DROITE en se penchant vers la DROITE
        rb.AddForceAtPosition(force_left, pos_right);
        rb.AddForceAtPosition(force_right, pos_left);
        rb.AddForceAtPosition(force_front, pos_rear);
        rb.AddForceAtPosition(force_rear, pos_front);

        //Effet de vent sur le drone
        if (variables.getWind() != new Vector3(0, 0, 0))
            rb.AddForce(variables.getWind());
        

        // Rotation GAUCHE / DROITE
        rb.AddForceAtPosition(rb.transform.right * desiredSpin, rb.transform.position + rb.transform.forward);
        rb.AddForceAtPosition(-rb.transform.right * desiredSpin, rb.transform.position - rb.transform.forward);
    }

    void Awake() {
        sonDrone = GetComponent<AudioSource>();
        helices = new List<HeliceAnimation>();
        helices.Add(transform.GetChild(0).GetComponent<HeliceAnimation>());
        helices.Add(transform.GetChild(1).GetComponent<HeliceAnimation>());
        helices.Add(transform.GetChild(2).GetComponent<HeliceAnimation>());
        helices.Add(transform.GetChild(3).GetComponent<HeliceAnimation>());

        this.rb = GetComponent<Rigidbody>();
        this.IsFlying = false;
    }

    private void FixedUpdate() {
        if(sonDrone.time > 75) {
            sonDrone.time = 5f;
        }

        if(this.cooldown > 0) {
            this.cooldown -= Time.deltaTime;
        }
    }
}
