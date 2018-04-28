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

    [SerializeField]
    GameObject[] displayChars;

    int charsView; //will see from myChars[charsview] + the charstoshow number

    int charsToShow = 3;

    [SerializeField]
    private Text charName;

    [SerializeField]
    private Text charPrice;

    [SerializeField]
    private Text charinfo;

    [SerializeField]
    private Button unlockChar;

    int charId;
    [SerializeField]
    private Image coinImg;

    // Use this for initialization
    void Start()
    {
        charsView = 0;

        foreach (var item in displayChars)
        {
            item.transform.gameObject.SetActive(false);
        }

        charName.text = "";
        charPrice.text = "";
        charinfo.text = "";

        coinImg.gameObject.SetActive(false);
        unlockChar.gameObject.SetActive(false);
    }

    public void SetUpMenu()
    {
        for (int i = 0; i < charSpots.Length; i++)
        {
            Vector3 newspot = charSpots[i].transform.position;
            if (mychars[i + charsView] != null)
            {
                mychars[i + charsView].transform.position = newspot;
                mychars[i + charsView].transform.parent = charSpots[i].transform;
            }
        }
    }

    public void NextChars()
    {
        charsView++;
        if (charsView > mychars.Length - charsToShow)
            charsView = 0;

        foreach (var item in mychars)
        {
            item.transform.parent = null;
            item.transform.position = new Vector3(50, 10, -25);
        }

        SetUpMenu();
    }

    public void PreviousChars()
    {
        charsView--;
        if (charsView < 0)
            charsView = mychars.Length - charsToShow;

        foreach (var item in mychars)
        {
            item.transform.position = new Vector3(50, 10, -25);
        }

        SetUpMenu();
    }

    public void ShowInfo(GameObject obj)
    {
        charName.text = obj.transform.GetChild(0).GetComponent<NewCharDesc>().CharName;
        charPrice.text = obj.transform.GetChild(0).GetComponent<NewCharDesc>().Price.ToString();
        charinfo.text = obj.transform.GetChild(0).GetComponent<NewCharDesc>().Desc;
        charId = obj.transform.GetChild(0).GetComponent<NewCharDesc>().Id;

        coinImg.gameObject.SetActive(true);

        if (obj.transform.GetChild(0).GetComponent<NewCharDesc>().Unlocked == 0)
        {
            unlockChar.gameObject.SetActive(true);
        }
        else
        {
            unlockChar.gameObject.SetActive(false);
        }

        foreach (var item in displayChars)
        {
            item.transform.gameObject.SetActive(false);
        }

        displayChars[charId].SetActive(true);
    }

    public void UnlockCharacter()
    {
        foreach (var item in mychars)
        {
            if (item.GetComponent<NewCharDesc>().Id == charId)
            {
                if (item.GetComponent<NewCharDesc>().Unlocked != 1)
                {
                    if (PlayerPrefs.GetInt("myCoins") > item.GetComponent<NewCharDesc>().Price)
                    {
                        item.GetComponent<NewCharDesc>().Unlocked = 1;
                        PlayerPrefs.SetInt("myCoins", PlayerPrefs.GetInt("myCoins") - item.GetComponent<NewCharDesc>().Price);
                    }
                    else
                    {
                        //Let people buy coins, or watch add to get coins
                    }
                }

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //if a new character is clicked, put it up on the big ones place
    }
}
