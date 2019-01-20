using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit : MonoBehaviour {

    public Text TempsText;
    public int TempsEntreCheckpoint = 10;
	public int difficulte;

	private bool circuitActiver = false;
	private bool circuitFin;
	private bool circuitGagner;
	private float tempsRestant = 1;
	private int tempsAffichage;
	private List<GameObject> listeCheckpoints;
	private Variables var;

    // Start is called before the first frame update
    void Start() {

		listeCheckpoints = new List<GameObject>();
		foreach (Transform checkpoint_Prefab in transform)
		{
			listeCheckpoints.Add(checkpoint_Prefab.gameObject);
		}

		var = new Variables();
		if (difficulte != var.Get_difficulte())
		{
			hideCircuit();
			return;
		}
		else
			circuitActiver = true;

		changerTailleAnneau();
		circuitFin = false;
        circuitGagner = false;
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
		if (!circuitActiver)
			return;

        tempsAffichage = Mathf.RoundToInt(tempsRestant);
        TempsText.text = ("Temps restant : " + tempsAffichage + " secondes");
        if(tempsRestant >= 0) {
            tempsRestant -= Time.deltaTime;
        }
    }

    private void echecCircuit() {
		if (!circuitActiver)
			return;

		TempsText.text = ("Trop tard !");
        circuitFin = true;
        circuitGagner = false;
        for(int i = 0; i < listeCheckpoints.Count; i++) {
            CheckPoint checkpt = listeCheckpoints[i].transform.GetChild(0).GetComponent<CheckPoint>();
            checkpt.isNext = true;
            checkpt.isFinished = false;
            checkpt.UpdateCouleur();
        }
    }

    private void succesCircuit() {
		if (!circuitActiver)
			return;

		TempsText.text = ("Vous avez réussi ! Bravo !");
        circuitFin = true;
        circuitGagner = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
		if (!circuitActiver)
			return;

		if (!circuitFin) {
            if(tempsRestant < 0)
                echecCircuit();
            else
                affichageTemps();
        }
    }

    public void completeCheckpoint(CheckPoint checkpoint) {

		if (!circuitActiver)
			return;

		if (!circuitFin && checkpoint.isNext && !checkpoint.isFinished) {
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

	private void hideCircuit()
	{
		foreach (GameObject ckpt in listeCheckpoints)
		{
			ckpt.SetActive(false);
		}
	}

	private void changerTailleAnneau()
	{
		float scale = 0.0f;

		if (difficulte == 1)
			scale = 2f;
		else if (difficulte == 2)
			scale = 1f;
		else if (difficulte == 3)
			return;

		foreach (GameObject ckpt in listeCheckpoints)
		{
			ckpt.transform.localScale += new Vector3(scale, scale, scale);
		}
	}
}
