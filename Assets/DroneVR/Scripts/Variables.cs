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

    public void setWind(int w)
    {
        if (w == 1)
           Wind = new Vector3(0.5f, 0, 0);

    }

    void Awake() {
        this.Difficulty = 1;
        DontDestroyOnLoad(this.gameObject); // Ne pas detruire et conserver le GameObject lors des changements de scene
    }
}
