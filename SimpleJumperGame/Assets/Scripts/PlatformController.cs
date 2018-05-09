using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlatformType { Turtle, WaterLilly, Raft }

public class PlatformController : MonoBehaviour
{
    private bool hitByPlayer;

    bool coinPlatform;

    PoolManager objectPool;


    private void Start()
    {
        objectPool = PoolManager.Instance;

        PlayerControls.OnPlayerDeath += OnPlayerDeath;

        if (Random.Range(0, 101) > 97)
        {
            coinPlatform = true;
        }

        StartCoroutine(SpawnCoin());
    }

    IEnumerator SpawnCoin()
    {
        yield return new WaitForSeconds(1);

        if (coinPlatform == true)
        {
            GameObject go = objectPool.SpawnObject("Coin", new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);
            go.transform.parent = this.transform;
        }
    }

    public virtual void HitByPlayer()
    {
        HighscoreController.Instance.Score++;
    }

    private IEnumerator ShakePlatform()
    {
        this.transform.Translate(new Vector3(0, -.1f, 0));
        yield return new WaitForSeconds(.1f);
        this.transform.Translate(new Vector3(0, .1f, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControls>() && !hitByPlayer)
        {
            StartCoroutine(ShakePlatform());
            hitByPlayer = true;
            HighscoreController.Instance.Score++;

        }
    }

    private void OnPlayerDeath()
    {
        this.hitByPlayer = false;
    }
}
