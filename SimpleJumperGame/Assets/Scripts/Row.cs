﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField]
    List<Transform> myChildren;
    float newMovespeed;
    float platformsizeX;
    float platformsizeZ;

    float newSizeX;
    float newSizeZ;

    float difficulty;
    Vector3 myScale;

    public List<Transform> MyChildren
    {
        get
        {
            return myChildren;
        }

        set
        {
            myChildren = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        //adds all children to a list, so we can modify them
        foreach (Transform child in transform)
        {
            myChildren.Add(child);
            child.gameObject.SetActive(true);
        }

        if (myChildren[0].GetComponent<RaftController>() != null)
        {
            if (Random.Range(0, 100) > 50)
            {
                foreach (Transform child in myChildren)
                {
                    RaftController raft = child.GetComponent<RaftController>();
                    raft.moveRight = true;
                }
            }
        }


        if (myChildren[0].name.Contains("Turtle"))
        {
            if (Random.Range(0, 100) > 50)
            {
                int rnd = Random.Range(0, myChildren.Count + 1);
                for (int i = 0; i <= myChildren.Count - 1; i++)
                {
                    if (myChildren[i] != myChildren[rnd])
                    {
                        myChildren[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        if (myChildren[0].name.Contains("Turtle"))
        {
            return;
        }

        //a factor which we can multiply by, to make the game progressevely harder
        difficulty = (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().jumpCounter + 1) / 100f;

        //Used to decide the scale of the item
        myScale = myChildren[0].localScale;
        myScale.x -= (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().jumpCounter / 50);
        myScale.z -= (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().jumpCounter / 50);

        //Checks the direction of the last row we made, and makes the new one run the other way
        if (RowHandler.Instance.MyDirection == MoveDirection.Left)
        {
            newMovespeed = Random.Range(5, 10 * (difficulty + 1));
            RowHandler.Instance.MyDirection = MoveDirection.Right;
        }
        else
        {
            newMovespeed = Random.Range(-5, -10 * (difficulty + 1));
            RowHandler.Instance.MyDirection = MoveDirection.Left;
        }

        //Gets the current size of the items
        platformsizeZ = myChildren[0].localScale.z;
        platformsizeX = myChildren[0].localScale.x;

        foreach (var item in myChildren)
        {
            //Sets a new random size for the next object, the further you get, the smaller it can be from its original state
            newSizeX = Random.Range(myScale.x, platformsizeX);
            newSizeZ = Random.Range(myScale.z, platformsizeZ);

            //decides stuff for each of the rows, as how to scale, and how to move and behave
            if (newMovespeed < 0)
            {
                item.localScale = new Vector3(newSizeX, 2, newSizeZ);
            }
            else
            {
                item.localScale = new Vector3(newSizeX, 2, newSizeZ);
            }

        }
    }
}
