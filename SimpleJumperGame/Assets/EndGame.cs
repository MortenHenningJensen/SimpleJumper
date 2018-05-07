using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public static EndGame Instance;

    [SerializeField]
    private Text currentScore;

    [SerializeField]
    private Text highestScore;

    [SerializeField]
    private Text beatScore;

    private bool betterScore;
    private int previousHighScore;

    public bool BetterScore
    {
        get
        {
            return betterScore;
        }

        set
        {
            betterScore = value;
        }
    }

    public int PreviousHighScore
    {
        get
        {
            return previousHighScore;
        }

        set
        {
            previousHighScore = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void GameEnded()
    {
        Debug.Log("TEST");
        if (betterScore)
        {
            int betterScore = previousHighScore - HighscoreController.Instance.Score;
            beatScore.text = "You beat your previous best score by " + betterScore;
        }
        else
        {
            beatScore.text = "";
        }
        currentScore.text = "Your score was " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().jumpCounter;
        highestScore.text = "Your best score is " + PlayerPrefs.GetInt("highscore");
    }

    public void PlayAgain()
    {
        Debug.Log("TEST PLAY");

        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        Debug.Log("TEST EXIT");

        SceneManager.LoadScene(0);
    }
}
