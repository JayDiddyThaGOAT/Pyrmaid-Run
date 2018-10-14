using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	// Use this for initialization
    public void PlayGame ()
    {
        SceneManager.LoadScene("DialoguePyramidRun");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
