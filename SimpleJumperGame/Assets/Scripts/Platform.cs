using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


enum PlatformType { Normal, Rotate, Timed, UpDown }

public class Platform : MonoBehaviour
{
    [SerializeField]
    private int scoreToAdd = 1;

    private float shakeDuration;

    private float shakeRandomness;

    private int shakeVibrato;

    private Vector3 shakeStrenght;

    private bool hitByPlayer;

    int minZvalue = -20;
    int maxZvalue = 20;

    int minYvalue = -5;
    int maxYvalue = 5;

    public int movespeed;

    public int resetPos;

    public bool direction;
    bool sinking;
    bool updown;

    bool coinPlatform;

    [SerializeField]
    PlatformType mytype;

    PoolManager objectPool;

    public void Start()
    {
        objectPool = PoolManager.Instance;

        if (Random.Range(0, 101) > 97)
        {
            coinPlatform = true;
        }
        PlayerControls.OnPlayerDeath += OnPlayerDeath;
        if (movespeed == 0 && !transform.name.Contains("Turtle"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().leftRight)
            {
                movespeed = -2;
            }
            else
                movespeed = 2;
        }
        StartCoroutine(SpawnCoin());
    }

    public void Update()
    {
        //Used to decide the movementspeed of the platform, as how to move with the different types
        if (mytype == PlatformType.Timed)
        {
            if (sinking)
            {
                gameObject.transform.Translate(new Vector3(0, -2, movespeed) * Time.deltaTime);

            }
            else
            {
                gameObject.transform.Translate(new Vector3(0, 0, movespeed) * Time.deltaTime);
            }

        }
        else if (mytype == PlatformType.UpDown)
        {
            if (!updown)
            {
                gameObject.transform.Translate(new Vector3(0, -1, movespeed) * Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(new Vector3(0, 1, movespeed) * Time.deltaTime);
            }

        }
        else if (mytype == PlatformType.Rotate)
        {
            float step = movespeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, resetPos * 2), step);
        }
        else
        {
            gameObject.transform.Translate(new Vector3(0, 0, movespeed) * Time.deltaTime);

        }

        //Virker ikke, kan ikke få den til at rotate og bevæge sig på samme tid
        if (mytype == PlatformType.Rotate)
        {
            this.transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
        }




        //Decides where the reset position is, and if its hit it, send it back to the other side
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

        //Decides if the updown platform is at its top, or at its bottom
        if (updown)
        {
            if (gameObject.transform.position.y >= 3)
            {
                updown = false;
            }
        }
        else
        {
            if (gameObject.transform.position.y <= -3)
            {
                updown = true;
            }

        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControls>() && !hitByPlayer)
        {
            ShakePlatform();
            hitByPlayer = true;
            HighscoreController.Instance.Score++;

            if (mytype == PlatformType.Timed)
            {
                StartCoroutine(Disappear());
            }

        }
    }

    private void ShakePlatform()
    {
        this.transform.DOShakePosition(this.shakeDuration, this.shakeStrenght, this.shakeVibrato, this.shakeRandomness); 
    }

    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2);
        //Play animation for animal to disappear
        sinking = true;
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

    private void OnPlayerDeath()
    {
        this.hitByPlayer = false;
    }

    public void SetShakeStats(float duration, Vector3 strenght, int vibrato, float randomness)
    {
        this.shakeDuration = duration;
        this.shakeStrenght = strenght;
        this.shakeVibrato = vibrato;
        this.shakeRandomness = randomness;
    }
}
