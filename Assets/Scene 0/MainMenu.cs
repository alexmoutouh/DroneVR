using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Variables variables;

    private static int difficulte = 1;
    private static bool VR = true;

    private GameObject boutonFacile;
    private GameObject boutonMoyen;
    private GameObject boutonDifficile;
    private GameObject boutonVR;

    void Start()
    {
        variables = new Variables();

        boutonFacile = GameObject.Find("Fenetre/Difficulté/Facile");
        boutonMoyen = GameObject.Find("Fenetre/Difficulté/Moyen");
        boutonDifficile = GameObject.Find("Fenetre/Difficulté/Difficile");
        boutonVR = GameObject.Find("Fenetre/VR");

        changer_difficulte();
    }

    public void changer_difficulte()
    {
        if (difficulte == 1)
        {
            boutonFacile.GetComponent<Image>().color = Color.red;
            boutonMoyen.GetComponent<Image>().color = Color.white;
            boutonDifficile.GetComponent<Image>().color = Color.white;
        }

        if (difficulte == 2)
        {
            boutonFacile.GetComponent<Image>().color = Color.white;
            boutonMoyen.GetComponent<Image>().color = Color.red;
            boutonDifficile.GetComponent<Image>().color = Color.white;
        }

        if (difficulte == 3)
        {
            boutonFacile.GetComponent<Image>().color = Color.white;
            boutonMoyen.GetComponent<Image>().color = Color.white;
            boutonDifficile.GetComponent<Image>().color = Color.red;
        }
    }

    public void quitter()
    {
        Application.Quit();
    }

    public void scene1()
    {
        variables.Set_difficulte(difficulte);
        variables.Set_VR(VR);
        SceneManager.LoadScene(1);
    }

    public void scene2()
    {
        variables.Set_difficulte(difficulte);
        variables.Set_VR(VR);
        SceneManager.LoadScene(2);
    }

    public void bouton_facile()
    {
        difficulte = 1;
        changer_difficulte();
    }

    public void bouton_moyen()
    {
        difficulte = 2;
        changer_difficulte();
    }

    public void bouton_difficile()
    {
        difficulte = 3;
        changer_difficulte();
    }

    public void bouton_VR()
    {
        if (VR == true)
        {
            VR = false;
            boutonVR.gameObject.GetComponentInChildren<Text>().text = "Activer VR";
        }

        else
        {
            VR = true;
            boutonVR.gameObject.GetComponentInChildren<Text>().text = "Désactiver VR";
        }
    }
}
