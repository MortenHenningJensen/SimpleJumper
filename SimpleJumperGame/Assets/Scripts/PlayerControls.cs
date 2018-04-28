using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance;

    public static System.Action OnPlayerDeath;


    [SerializeField]
    private GameObject jumpDestination;
    [SerializeField]
    private GameObject superJumpDestination;

    [SerializeField]
    private WeaponController shooter;

    [SerializeField]
    private Ease jumpEase;

    [SerializeField]
    private float timeBeforeReset;

    private Vector3 startPos;

    private bool isJumping;
    Rigidbody myBody;

    public int jumpCounter;

    [SerializeField]
    GameObject[] sideObjects;

    private int objectIterator;

    public Vector3 firstSpawn;

    PoolManager objectPool;

    public bool leftRight;



    // Use this for initialization
    void Start()
    {
        Instance = this;
        this.startPos = this.transform.position;
        myBody = GetComponent<Rigidbody>();
        jumpCounter = 0;
        firstSpawn = new Vector3(28, 0, 0);
        objectPool = PoolManager.Instance;
        leftRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !this.isJumping)
        {
            Jump();
           // this.transform.DOJump(this.jumpDestination.transform.position, 2, 0, 0.8f).SetEase(this.jumpEase).OnStart(OnJump).OnComplete(() => this.isJumping = false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !this.isJumping)
        {
            this.transform.DOJump(this.superJumpDestination.transform.position, 5, 0, 1f).SetEase(this.jumpEase).OnStart(() => this.isJumping = true).OnComplete(() => this.isJumping = false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            shooter.FireWeapon();
        }

        if (this.transform.position.y <= -5)
        {
            // SceneManager.LoadScene(0);
            StartCoroutine(KillPlayer());
        }

    }

    public void Jump()
    {
        this.transform.DOJump(this.jumpDestination.transform.position, 2, 0, 0.8f).SetEase(this.jumpEase).OnStart(OnJump);
    }

    public void OnJump()
    {
        this.isJumping = true;
        this.jumpCounter++;
        SpawnNew();

        if (jumpCounter % 5 == 0)
        {
            MoveSides();
        }
    }


    public void SpawnNew()
    {
        float spawnLocation = firstSpawn.x;
        float nextspot = spawnLocation + (7 * jumpCounter);

        //makes a random number, and the if statements below decides which row to spawn
        int rnd = Random.Range(0, 101);

        if (rnd < 10)
        {
            objectPool.SpawnObject("Turtle", new Vector3(nextspot, 0, 0), Quaternion.identity);

        }
        else if (rnd > 10 && rnd < 55)
        {
            objectPool.SpawnObject("Raft", new Vector3(nextspot, 0, 0), Quaternion.identity);

        }
        else if (rnd > 55)
        {
            objectPool.SpawnObject("Cube", new Vector3(nextspot, 0, 0), Quaternion.identity);

        }
    }
    Vector3 oldPos;
    Vector3 oldPos2;
    Vector3 myNewPos;
    Vector3 myNewPos2;

    public void MoveSides()
    {
        switch (objectIterator)
        {
            case 0:
                oldPos = sideObjects[0].transform.position;
                oldPos2 = sideObjects[1].transform.position;

                myNewPos = new Vector3(oldPos.x + 100, oldPos.y, oldPos.z);
                myNewPos2 = new Vector3(oldPos2.x + 100, oldPos2.y, oldPos2.z);

                sideObjects[0].transform.position = myNewPos;
                sideObjects[1].transform.position = myNewPos2;
                break;
            case 1:
                oldPos = sideObjects[2].transform.position;
                oldPos2 = sideObjects[3].transform.position;

                myNewPos = new Vector3(oldPos.x + 100, oldPos.y, oldPos.z);
                myNewPos2 = new Vector3(oldPos2.x + 100, oldPos2.y, oldPos2.z);

                sideObjects[2].transform.position = myNewPos;
                sideObjects[3].transform.position = myNewPos2;
                break;
            case 2:
                oldPos = sideObjects[4].transform.position;
                oldPos2 = sideObjects[5].transform.position;

                myNewPos = new Vector3(oldPos.x + 100, oldPos.y, oldPos.z);
                myNewPos2 = new Vector3(oldPos2.x + 100, oldPos2.y, oldPos2.z);

                sideObjects[4].transform.position = myNewPos;
                sideObjects[5].transform.position = myNewPos2;
                break;

        }
        if (objectIterator < 3)
        {
            objectIterator++;
        }
        else
        {
            objectIterator = 0;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "MovingPlatform")
        {
            //Might want to change so you can only jump again if you hit another platform, and not at the end of the animation
            isJumping = false;

        }

       
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;

        }
        else
        {
            Debug.Log(collision);

            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
           StartCoroutine(KillPlayer());
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(KillPlayer());
        }
    }
    

    public IEnumerator KillPlayer()
    {

        //Set time before reset, to the lenght of death animation
        yield return new WaitForSeconds(this.timeBeforeReset);
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
        this.transform.position = startPos;
        this.transform.parent = null;
    }


}
