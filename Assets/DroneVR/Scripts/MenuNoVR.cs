using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNoVR : MonoBehaviour {
    private bool On = false;
    private Variables data; // Variables globales conservees entre chaque changement de scene

    private GameObject boutonFacile;
    private GameObject boutonMoyen;
    private GameObject boutonDifficile;
    public Text mode;

    /// <summary>
    /// Actualise l'affichage en fonction de la difficulte presente dans this.data
    /// </summary>
    public void changer_difficulte() {
        if(data.Difficulty == 1) {
            boutonFacile.GetComponent<Image>().color = Color.red;
            boutonMoyen.GetComponent<Image>().color = Color.white;
            boutonDifficile.GetComponent<Image>().color = Color.white;
            this.mode.text = "Facile";
        }

        if(data.Difficulty == 2) {
            boutonFacile.GetComponent<Image>().color = Color.white;
            boutonMoyen.GetComponent<Image>().color = Color.red;
            boutonDifficile.GetComponent<Image>().color = Color.white;
            this.mode.text = "Moyen";
        }

        if(data.Difficulty == 3) {
            boutonFacile.GetComponent<Image>().color = Color.white;
            boutonMoyen.GetComponent<Image>().color = Color.white;
            boutonDifficile.GetComponent<Image>().color = Color.red;
            this.mode.text = "Difficile";
        }
    }

    public void quitter() {
        Application.Quit();
    }

    public void LoadScene(int no) {
        //Si on est sur la scène "Foret"
        if (no == 2)
            //Vent de la gauche vers la droite
            data.setWind(1);
        SceneManager.LoadScene(no);
    }

    /// <summary>
    /// Change la difficulte en mode facile
    /// </summary>
    public void bouton_facile() {
        data.Difficulty = 1;
        changer_difficulte();
    }

    public void bouton_moyen() {
        data.Difficulty = 2;
        changer_difficulte();
    }

    public void bouton_difficile() {
        data.Difficulty = 3;
        changer_difficulte();
    }

    void Start() {
        this.data = GameObject.FindGameObjectWithTag("gamedata").GetComponent<Variables>();
        boutonFacile = GameObject.Find("Fenetre/Difficulte/Easy");
        boutonMoyen = GameObject.Find("Fenetre/Difficulte/Normal");
        boutonDifficile = GameObject.Find("Fenetre/Difficulte/Hard");

        changer_difficulte();
    }
}
