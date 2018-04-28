using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("myCoins"))
        {
            PlayerPrefs.SetInt("myCoins", 0);
        }
        RotateMe();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RotateMe()
    {
        this.transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360).OnComplete(RotateMe).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //Might want to change so you can only jump again if you hit another platform, and not at the end of the animation
            PlayerPrefs.SetInt("myCoins", PlayerPrefs.GetInt("myCoins") + 1);
            this.gameObject.SetActive(false);
            Debug.Log("Added coin, total is now" + PlayerPrefs.GetInt("myCoins"));
        }
    }
}


