using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelecter : MonoBehaviour
{

    [SerializeField]
    GameObject[] mychars;

    [SerializeField]
    GameObject[] charSpots;

    int charsView; //will see from myChars[charsview] + the charstoshow number

    int charsToShow = 3;

    // Use this for initialization
    void Start()
    {
        charsView = 0;
    }

    public void SetUpMenu()
    {
        for (int i = 0; i < charSpots.Length; i++)
        {
            Vector3 newspot = charSpots[i].transform.position;
            mychars[i + charsView].transform.position = newspot;
        }
    }

    public void NextChars()
    {
        charsView++;
        if (charsView + 3 > mychars.Length)
            charsView = 0;

        foreach (var item in mychars)
        {
            item.transform.position = new Vector3(50, 10, -25);
        }

        SetUpMenu();
    }

    public void PreviousChars()
    {
        charsView--;
        if (charsView < 0)
            charsView = mychars.Length;

        foreach (var item in mychars)
        {
            item.transform.position = new Vector3(50, 10, -25);
        }

        SetUpMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (charsView >= 1)
        {
            GameObject.Find("previous").GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("previous").GetComponent<Button>().interactable = false;
        }
    }
}
