using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour
{
    public static HighscoreController Instance;

    private int score;

    private int highscore;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int Highscore
    {
        get
        {
            return highscore;
        }

        set
        {
            highscore = value;
        }
    }

    public Canvas scoreCanvas;

    public Canvas endCanvas;

    private void Awake()
    {
        Instance = this;
        PlayerControls.OnPlayerDeath += OnPlayerDeath;
    }

    private void Start()
    {
        this.highscore = PlayerPrefs.GetInt("highscore");
        UIController.Instance.Highscore.text = highscore.ToString();
    }

    private void SetHighscore()
    {
        EndGame.Instance.GameEnded();

        if (score > PlayerPrefs.GetInt("highscore"))
        {
            EndGame.Instance.BetterScore = true;
            EndGame.Instance.PreviousHighScore = PlayerPrefs.GetInt("highscore");

            PlayerPrefs.SetInt("highscore", score);
            UIController.Instance.Highscore.text = PlayerPrefs.GetInt("highscore").ToString();
        }
    }

    private void OnPlayerDeath()
    {
        SetHighscore();
        scoreCanvas.enabled = false;
        endCanvas.enabled = true;
        this.score = 0;
    }
}
