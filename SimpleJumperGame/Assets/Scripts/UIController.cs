using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Text score;
    public PlayerControls pc;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.Find("JumpChar").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = pc.score.ToString();
    }

    private void OnGUI()
    {
        //int minutes = Mathf.FloorToInt(timer / 60F);
        //int seconds = Mathf.FloorToInt(timer - minutes * 60);
        //string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        //_txtTimer.text = niceTime;

    }

}
