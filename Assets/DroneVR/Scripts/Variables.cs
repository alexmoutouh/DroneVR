using UnityEngine;

public class Variables : MonoBehaviour {
    /* La difficulté choisie
     * 1 = facile
     * 2 = moyen
     * 3 = difficile
     * */
    public int Difficulty { get; set; }

    void Awake() {
        this.Difficulty = 1;
        DontDestroyOnLoad(this.gameObject);
    }
}
