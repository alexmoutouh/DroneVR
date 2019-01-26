using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NewtonVR;

// Menu des scenes du drone
public class MainMenu : MonoBehaviour {
    private bool On = false;
    private Variables data;

    public bool VRBehaviour = true;
    public Text mode;

    public void quitter() {
        Application.Quit();
    }

    public void LoadScene(int no) {
        SceneManager.LoadScene(no);
    }

    public void SetDifficulty(int no) {
        data.Difficulty = no;
        this.DisplayDifficulty();
    }

    public void DisplayDifficulty() {
        this.mode.text = "Difficulté : ";
        if(this.data.Difficulty == 1) {
            this.mode.text += "Facile";
        } else if(this.data.Difficulty == 2) {
            this.mode.text += "Moyen";
        } else if(this.data.Difficulty == 3) {
            this.mode.text += "Difficile";
        }
    }

    void Start() {
        this.data = GameObject.FindGameObjectWithTag("gamedata").GetComponent<Variables>();
        this.DisplayDifficulty();
    }

    void Update() {
        if(this.VRBehaviour) {
            if(OVRInput.GetDown(OVRInput.Button.Start)) {
                if(this.On) {
                    this.GetComponent<Canvas>().enabled = false;
                } else {
                    this.GetComponent<Canvas>().enabled = true;
                    this.GetComponent<RectTransform>().position = NVRPlayer.Instance.transform.position + NVRPlayer.Instance.Head.transform.forward * 2 + Vector3.up * 2;
                    this.GetComponent<RectTransform>().rotation = NVRPlayer.Instance.Head.transform.rotation;
                }
                this.On = !this.On;
            }
        } else {
            // Si le menu d'une scene drone n'est pas en mode VR. <Echap> au menu principal (cf MenuNoVR.cs). 
            if(Input.GetKeyUp(KeyCode.Escape)) {
                this.LoadScene(0);
            }
        }
    }
}
