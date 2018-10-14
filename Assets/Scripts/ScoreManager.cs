using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text highscoreText;

    public float scoreCount;

    public float pointperSecond;

    public bool scoreIncrease = true;

	// Use this for initialization
	void Start ()
    {
      
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if player has not hit anything, continue to increase score
        if(scoreIncrease)
        {
            scoreCount += pointperSecond * Time.deltaTime;
        }

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
