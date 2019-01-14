using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit : MonoBehaviour
{	
	public	Text	TempsText;
	public	int		TempsEntreCheckpoint = 10;
	public	float	tempsRestant = 1;
	public	int		tempsAffichage;
	public	bool	circuitFin;
	public	bool	circuitGagner;

	private List<GameObject> listeCheckpoints;

	// Start is called before the first frame update
	void Start()
    {
		circuitFin		= false;
		circuitGagner	= false;
		tempsRestant	= TempsEntreCheckpoint;

		listeCheckpoints = new List<GameObject>();

		foreach (Transform checkpoint_Prefab in transform)
		{
			listeCheckpoints.Add(checkpoint_Prefab.gameObject);
		}

		CheckPoint start = listeCheckpoints[0].transform.GetChild(0).GetComponent<CheckPoint>();
		start.isNext		= true;
		start.isFinished	= false;
		start.UpdateCouleur();

		for (int i = 1; i < listeCheckpoints.Count; i++)
		{
			CheckPoint checkpt = listeCheckpoints[i].transform.GetChild(0).GetComponent<CheckPoint>();
			checkpt.isNext		= false;
			checkpt.isFinished	= false;
			checkpt.UpdateCouleur();
		}
	}
	private void affichageTemps()
	{
		tempsAffichage = Mathf.RoundToInt(tempsRestant);
		TempsText.text = ("Temps restant : " + tempsAffichage + " secondes");
		if (tempsRestant >= 0)
		{
			tempsRestant -= Time.deltaTime;
		}
	}

	private void echecCircuit()
	{
		TempsText.text = ("Trop tard !");
		circuitFin		= true;
		circuitGagner	= false;
		for (int i = 0; i < listeCheckpoints.Count; i++)
		{
			CheckPoint checkpt = listeCheckpoints[i].transform.GetChild(0).GetComponent<CheckPoint>();
			checkpt.isNext = true;
			checkpt.isFinished = false;
			checkpt.UpdateCouleur();
		}
	}

	private void succesCircuit()
	{
		TempsText.text = ("Vous avez réussi ! Bravo !");
		circuitFin = true;
		circuitGagner = true;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (!circuitFin)
		{
			if (tempsRestant < 0)
				echecCircuit();
			else
				affichageTemps();
		}
    }

	public void completeCheckpoint(CheckPoint checkpoint)
	{
		
		if (!circuitFin && checkpoint.isNext && !checkpoint.isFinished)
		{
			tempsRestant = TempsEntreCheckpoint;

			GameObject checkpointPrefab = checkpoint.transform.parent.gameObject;

			int index = listeCheckpoints.IndexOf(checkpointPrefab);

			checkpoint.isNext = false;
			checkpoint.isFinished = true;
			checkpoint.UpdateCouleur();

			if (index != -1 && index + 1 < listeCheckpoints.Count)
			{
				CheckPoint nextCheckpt = listeCheckpoints[index + 1].transform.GetChild(0).GetComponent<CheckPoint>();
				nextCheckpt.isNext = true;
				nextCheckpt.UpdateCouleur();
			}

			if (index + 1 == listeCheckpoints.Count)
			{
				succesCircuit();
			}
		}	
	}
}
