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

    public void quitter() {
        Application.Quit();
    }

    public void LoadScene(int no) {
        SceneManager.LoadScene(no);
    }

    public void ChangeDifficulty(int no) {
        data.Difficulty = no;
    }

    void Start() {
        this.data = GameObject.FindGameObjectWithTag("gamedata").GetComponent<Variables>();
        boutonFacile = GameObject.Find("MenuNoVR/Difficulté/Facile");
        boutonMoyen = GameObject.Find("MenuNoVR/Difficulté/Moyen");
        boutonDifficile = GameObject.Find("MenuNoVR/Difficulté/Difficile");

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
                this.LoadScene(0);
            }
        }
    }
}
