using UnityEngine;

public class Variables : MonoBehaviour {
    /* La difficulté choisie
     * 1 = facile
     * 2 = moyen
     * 3 = difficile
     * */
    public int Difficulty { get; set; }

    private static Vector3 Wind = new Vector3(0, 0, 0);

    public Vector3 getWind()
    {
        return Wind;
    }

    public void setWind(Vector3 w)
    {
        Wind = w;
    }

    void Awake() {
        this.Difficulty = 1;
        DontDestroyOnLoad(this.gameObject); // Ne pas detruire et conserver le GameObject lors des changements de scene
    }
}
