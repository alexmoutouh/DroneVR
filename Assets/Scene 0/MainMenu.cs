using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void quitter()
    {
        Application.Quit();
    }

    public void scene1()
    {
        SceneManager.LoadScene(1);
    }

    public void scene2()
    {
        SceneManager.LoadScene(2);
    }
}
