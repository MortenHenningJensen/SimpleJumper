using System.Collections;
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
    float difficultyFactor;

    // Use this for initialization
    void Start()
    {

        foreach (Transform child in transform)
        {
            myChildren.Add(child);
        }

        difficulty = (GameObject.Find("JumpChar").GetComponent<PlayerControls>().jumpCounter + 1) / 100f;

        difficultyFactor = 5f;
        //Change these if conditions to make the game progressevely harder
        #region Diffuculty
        if (difficulty >= 0.1f)
        {
            difficultyFactor = 5f;
        }
        else if (difficulty >= 0.3f)
        {
            difficultyFactor = 4.8f;
        }
        else if (difficulty >= 0.4f)
        {
            difficultyFactor = 4.5f;
        }
        else if (difficulty >= 0.5f)
        {
            difficultyFactor = 4.2f;
        }
        else if (difficulty >= 0.6f)
        {
            difficultyFactor = 4.0f;
        }
        else if (difficulty >= 0.7f)
        {
            difficultyFactor = 3.8f;
        }
        else if (difficulty >= 0.8f)
        {
            difficultyFactor = 3.5f;
        }
        else if (difficulty >= 0.9f)
        {
            difficultyFactor = 3.2f;
        }
        else if (difficulty >= 1f)
        {
            difficultyFactor = 3.0f;
        }
        else if (difficulty >= 1.1f)
        {
            difficultyFactor = 2.8f;
        }
        else if (difficulty >= 1.2f)
        {
            difficultyFactor = 2.5f;
        }
#endregion

        newMovespeed = Random.Range(-10 * (difficulty + 1), 10 * difficulty);
        if (newMovespeed == 0)
        {
            newMovespeed = 2;
        }

        platformsizeZ = myChildren[0].localScale.z;
        platformsizeX = myChildren[0].localScale.x;

        foreach (var item in myChildren)
        {
            newSizeX = Random.Range(difficultyFactor, platformsizeX);
            newSizeZ = Random.Range(difficultyFactor, platformsizeZ);

            if (newMovespeed < 0)
            {
                item.GetComponent<Platform>().direction = false;
                item.GetComponent<Platform>().resetPos = 20;
                item.localScale = new Vector3(newSizeZ, 2, newSizeZ);
            }
            else
            {
                item.GetComponent<Platform>().direction = true;
                item.GetComponent<Platform>().resetPos = -20;
                item.localScale = new Vector3(newSizeZ, 2, newSizeZ);
            }

            item.GetComponent<Platform>().movespeed = (int)newMovespeed;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DestrySelf());
        }
    }

    public IEnumerator DestrySelf()
    {
        yield return new WaitForSeconds(5);
        PoolManager.Instance.DespawnObject(this.gameObject);
    }
}
