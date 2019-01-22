using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit : MonoBehaviour {

    public Text TempsText;
    public int TempsEntreCheckpoint = 10;
    public int difficulte;

    private bool circuitActive = false;
    private bool circuitEnd;
    private bool circuitWon;
    private float tempsRestant = 1;
    private int tempsAffichage;
    private List<GameObject> listeCheckpoints;
    private Variables var;

    // Start is called before the first frame update
    void Start() {

        listeCheckpoints = new List<GameObject>();
        foreach(Transform checkpoint_Prefab in transform) {
            listeCheckpoints.Add(checkpoint_Prefab.gameObject);
        }

        var = GameObject.FindGameObjectWithTag("gamedata").GetComponent<Variables>();
        if(difficulte != var.Difficulty) {
            hideCircuit();
            return;
        } else
            circuitActive = true;

        changerTailleAnneau();
        circuitEnd = false;
        circuitWon = false;
        tempsRestant = TempsEntreCheckpoint;

        CheckPoint start = listeCheckpoints[0].transform.GetChild(0).GetComponent<CheckPoint>();
        start.isNext = true;
        start.isFinished = false;
        start.UpdateCouleur();

        for(int i = 1; i < listeCheckpoints.Count; i++) {
            CheckPoint checkpt = listeCheckpoints[i].transform.GetChild(0).GetComponent<CheckPoint>();
            checkpt.isNext = false;
            checkpt.isFinished = false;
            checkpt.UpdateCouleur();
        }
    }
    private void affichageTemps() {
        if(!circuitActive)
            return;

        tempsAffichage = Mathf.RoundToInt(tempsRestant);
        TempsText.text = (tempsAffichage + " s left");
        if(tempsRestant >= 0) {
            tempsRestant -= Time.deltaTime;
        }
    }

    private void echecCircuit() {
        if(!circuitActive)
            return;

        TempsText.text = ("Fail!");
        circuitEnd = true;
        circuitWon = false;
        for(int i = 0; i < listeCheckpoints.Count; i++) {
            CheckPoint checkpt = listeCheckpoints[i].transform.GetChild(0).GetComponent<CheckPoint>();
            checkpt.isNext = true;
            checkpt.isFinished = false;
            checkpt.UpdateCouleur();
        }
    }

    private void succesCircuit() {
        if(!circuitActive)
            return;

        TempsText.text = ("Success!");
        circuitEnd = true;
        circuitWon = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!circuitActive)
            return;

        if(!circuitEnd) {
            if(tempsRestant < 0)
                echecCircuit();
            else
                affichageTemps();
        }
    }

    public void completeCheckpoint(CheckPoint checkpoint) {

        if(!circuitActive)
            return;

        if(!circuitEnd && checkpoint.isNext && !checkpoint.isFinished) {
            tempsRestant = TempsEntreCheckpoint;

            GameObject checkpointPrefab = checkpoint.transform.parent.gameObject;

            int index = listeCheckpoints.IndexOf(checkpointPrefab);

            checkpoint.isNext = false;
            checkpoint.isFinished = true;
            checkpoint.UpdateCouleur();

            if(index != -1 && index + 1 < listeCheckpoints.Count) {
                CheckPoint nextCheckpt = listeCheckpoints[index + 1].transform.GetChild(0).GetComponent<CheckPoint>();
                nextCheckpt.isNext = true;
                nextCheckpt.UpdateCouleur();
            }

            if(index + 1 == listeCheckpoints.Count) {
                succesCircuit();
            }
        }
    }

    private void hideCircuit() {
        foreach(GameObject ckpt in listeCheckpoints) {
            ckpt.SetActive(false);
        }
    }

    private void changerTailleAnneau() {
        float scale = 0.0f;

        if(difficulte == 1)
            scale = 2f;
        else if(difficulte == 2)
            scale = 1f;
        else if(difficulte == 3)
            return;

        foreach(GameObject ckpt in listeCheckpoints) {
            ckpt.transform.localScale += new Vector3(scale, scale, scale);
        }
    }
}
