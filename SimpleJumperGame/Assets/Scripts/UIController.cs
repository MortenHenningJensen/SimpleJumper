using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField]
    Text score;

    [SerializeField]
    Text highscore;

    public Text Highscore
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

    private void Start()
    {
        Instance = this;
    }

    void Update()
    {
        score.text = HighscoreController.Instance.Score.ToString();
    }

    private void OnGUI()
    {
        //int minutes = Mathf.FloorToInt(timer / 60F);
        //int seconds = Mathf.FloorToInt(timer - minutes * 60);
        //string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        //_txtTimer.text = niceTime;
    }
    
}
