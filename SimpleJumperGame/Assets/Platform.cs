using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField]
    private PlayerControls player;
    [SerializeField]
    private int scoreToAdd = 1;

    private bool hitByPlayer;

    int minZvalue = -20;
    int maxZvalue = 20;

    public int movespeed = 20;

    public int resetPos = -20;

    public bool direction;

    public void Start()
    {

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
            //  player.score += scoreToAdd;
        }
    }
}
