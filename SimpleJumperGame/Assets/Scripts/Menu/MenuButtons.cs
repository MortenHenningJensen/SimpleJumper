using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    [SerializeField]
    private Text coinText;

    private int myCoinNum;

    [SerializeField]
    private Canvas shopCanvas;
    [SerializeField]
    private Canvas menuCanvas;


    [SerializeField]
    private Camera menuCam;
    [SerializeField]
    private Camera shopCam;

    private void Start()
    {
        shopCanvas.GetComponent<CharacterSelecter>().SetUpMenu();
        shopCanvas.enabled = false;
        shopCam.enabled = false;
        myCoinNum = PlayerPrefs.GetInt("myCoins");
        coinText.text = myCoinNum.ToString();

    }

    private void Update()
    {
        coinText.text = myCoinNum.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
        shopCanvas.enabled ^= true;
        menuCanvas.enabled ^= true;

        if (shopCanvas.enabled == true)
        {
            menuCam.enabled = false;
            shopCam.enabled = true;

        }
        else
        {
            menuCam.enabled = true;
            shopCam.enabled = false;
        }


    }
}
