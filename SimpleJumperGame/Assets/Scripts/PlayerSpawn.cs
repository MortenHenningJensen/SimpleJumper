﻿using System.Collections;
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
            case 1:
                break;
            case 2:
                break;
            case 3:
                myobjects[1].gameObject.SetActive(true);
                break;
            case 4:
                break;
            case 5:
                myobjects[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
