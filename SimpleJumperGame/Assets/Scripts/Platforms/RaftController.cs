using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : PlatformController
{
    int minZvalue = -20;
    int maxZvalue = 20;
    int resetPos;

    public int movespeed;

    public bool moveRight;

    void Start ()
    {
        if (moveRight)
        {
            resetPos = 20;
        }
        else
        {
            resetPos = -20;
        }
	}
	
	
	void Update ()
    {
        if (moveRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
	}

    private void MoveLeft()
    {
        if (this.gameObject.transform.position.z >= this.maxZvalue)
        {
            ResetRaft();
        }
        this.gameObject.transform.Translate(new Vector3(0, 0, movespeed) * Time.deltaTime);
    }

    private void MoveRight()
    {
        if (this.gameObject.transform.position.z <= this.minZvalue)
        {
            ResetRaft();
        }
        this.gameObject.transform.Translate(new Vector3(0, 0, -movespeed) * Time.deltaTime);
    }

    private void ResetRaft()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.resetPos);
    }
}
