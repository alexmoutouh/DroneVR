using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NewtonVR;

public class MainMenu : MonoBehaviour {
    private bool On = false;
    private Variables data;

    private GameObject boutonFacile;
    private GameObject boutonMoyen;
    private GameObject boutonDifficile;
    private GameObject boutonVR;

    public bool VRBehaviour = true;
    public Text mode;

    public void changer_difficulte() {
        if(data.Difficulty == 1) {
            boutonFacile.GetComponent<Image>().color = Color.red;
            boutonMoyen.GetComponent<Image>().color = Color.white;
            boutonDifficile.GetComponent<Image>().color = Color.white;
        }

        if(data.Difficulty == 2) {
            boutonFacile.GetComponent<Image>().color = Color.white;
            boutonMoyen.GetComponent<Image>().color = Color.red;
            boutonDifficile.GetComponent<Image>().color = Color.white;
        }

        if(data.Difficulty == 3) {
            boutonFacile.GetComponent<Image>().color = Color.white;
            boutonMoyen.GetComponent<Image>().color = Color.white;
            boutonDifficile.GetComponent<Image>().color = Color.red;
        }
    }

    public void quitter() {
        Application.Quit();
    }

    public void LoadScene(int no) {
        SceneManager.LoadScene(no);
    }

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
        boutonFacile = GameObject.Find("MenuNoVR/Difficulté/Facile");
        boutonMoyen = GameObject.Find("MenuNoVR/Difficulté/Moyen");
        boutonDifficile = GameObject.Find("MenuNoVR/Difficulté/Difficile");

        changer_difficulte();

        this.mode.text = "Difficulty : ";
        if(this.data.Difficulty == 1) {
            this.mode.text += "Easy";
        } else if(this.data.Difficulty == 2) {
            this.mode.text += "Normal";
        } else if(this.data.Difficulty == 3) {
            this.mode.text += "Hard";
        }

        this.GetComponent<Canvas>().enabled = false;
    }

    void Update() {
        if(this.VRBehaviour) {
            if(OVRInput.GetDown(OVRInput.Button.Start)) {
                if(this.On) {
                    this.GetComponent<Canvas>().enabled = false;
                } else {
                    this.GetComponent<Canvas>().enabled = true;
                    this.GetComponent<RectTransform>().position = NVRPlayer.Instance.transform.position + NVRPlayer.Instance.Head.transform.forward * 2 + Vector3.up;
                    this.GetComponent<RectTransform>().rotation = NVRPlayer.Instance.Head.transform.rotation;
                }
                this.On = !this.On;
            }
        } else {
            if(Input.GetKeyUp(KeyCode.Escape)) {
                if(this.On) {
                    this.GetComponent<Canvas>().enabled = false;
                } else {
                    this.GetComponent<Canvas>().enabled = true;
                }
                this.On = !this.On;
            }
        }
    }
}
