using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text highscoreText;

    public float scoreCount;

    private AudioSource sound;
    private bool highScorePassed = false;

    private void Start()
    {
        sound = FindObjectOfType<PlayerController>().GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        scoreCount = Mathf.Clamp(FindObjectOfType<PlayerController>().transform.position.x, 0f, Mathf.Infinity);

        // check if current score is higher than high score
        if(scoreCount > (PlayerPrefs.GetFloat("High Score")))
        {
            PlayerPrefs.SetFloat("High Score", scoreCount);
        }

        // display score and high score
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highscoreText.text = "High Score: " + Mathf.Round(PlayerPrefs.GetFloat("High Score"));
	}
}
