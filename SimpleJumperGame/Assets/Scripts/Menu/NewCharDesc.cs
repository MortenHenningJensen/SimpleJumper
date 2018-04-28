using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharDesc : MonoBehaviour
{
    [SerializeField]
    string charName;
    [SerializeField]
    int price;
    [SerializeField]
    string desc;
    [SerializeField]
    byte unlocked;
    [SerializeField]
    int id;

    public string CharName
    {
        get
        {
            return charName;
        }

        set
        {
            charName = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public string Desc
    {
        get
        {
            return desc;
        }

        set
        {
            desc = value;
        }
    }

    public byte Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (unlocked != 1)
        {
            //Grey out the model here, might want to get all child objects through a loop and then grey them out there.
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
