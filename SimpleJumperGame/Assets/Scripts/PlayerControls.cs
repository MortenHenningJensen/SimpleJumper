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
    private Ease jumpEase;

    private Vector3 startPos;

    private bool isJumping;
    Rigidbody myBody;

    public int jumpCounter;

    [SerializeField]
    GameObject[] bodyobjects;
    public int bodypart;

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
            this.transform.DOJump(this.jumpDestination.transform.position, 2, 0, 0.8f).SetEase(this.jumpEase).OnStart(OnJump).OnComplete(() => this.isJumping = false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !this.isJumping)
        {
            this.transform.DOJump(this.superJumpDestination.transform.position, 5, 0, 1f).SetEase(this.jumpEase).OnStart(() => this.isJumping = true).OnComplete(() => this.isJumping = false);
        }

        if (this.transform.position.y <= -5)
        {
            // SceneManager.LoadScene(0);
            KillPlayer();
        }

    }

    public void OnJump()
    {
        this.isJumping = true;
        this.jumpCounter++;
        SpawnNew();
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "MovingPlatform")
        {
            //Might want to change so you can only jump again if you hit another platform, and not at the end of the animation
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
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
        this.transform.position = startPos;
        this.transform.parent = null;
    }

    
}
