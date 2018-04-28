using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject[] myobjects;

    private void Awake()
    {
        switch (PlayerPrefs.GetInt("SelectedCharacter"))
        {
            case 0:
                myobjects[0].gameObject.SetActive(true);
                break;
            case 3:
                myobjects[1].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
