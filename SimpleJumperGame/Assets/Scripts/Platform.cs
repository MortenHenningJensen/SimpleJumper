using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private int scoreToAdd = 1;

    private bool hitByPlayer;

    int minZvalue = -20;
    int maxZvalue = 20;

    public int movespeed;

    public int resetPos;

    public bool direction;

    public void Start()
    {
        //if (Random.Range(0, 100) > 50)
        //{
        //    movespeed = movespeed * -1;
        //}

    }

    public void Update()
    {
        gameObject.transform.Translate(new Vector3(0, 0, movespeed) * Time.deltaTime);

        if (movespeed < 0)
        {
            direction = false;
        }
        else
        {
            direction = true;
        }


        if (direction)
        {
            if (gameObject.transform.position.z >= maxZvalue)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, resetPos);
            }

        }
        else
        {
            if (gameObject.transform.position.z <= minZvalue)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, resetPos);
            }

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControls>() && !hitByPlayer)
        {
            hitByPlayer = true;
            other.GetComponent<PlayerControls>().score += scoreToAdd;
        }
    }
}
