using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{

    [SerializeField]
    private GameObject jumpDestination;
    [SerializeField]
    private GameObject superJumpDestination;

    [SerializeField]
    private Ease jumpEase;

    private Vector3 startPos;

    private bool isJumping;
    Rigidbody myBody;

    public int score;
    public int jumpCounter;

    [SerializeField]
    GameObject[] bodyobjects;
    public int bodypart;

    public Vector3 firstSpawn;

    PoolManager objectPool;

    // Use this for initialization
    void Start()
    {
        this.startPos = this.transform.position;
        myBody = GetComponent<Rigidbody>();
        score = 0;
        jumpCounter = 0;
        firstSpawn = new Vector3(28, 0, 0);
        objectPool = PoolManager.Instance;

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
            ResetPlayer();
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

        objectPool.SpawnObject("Cube", new Vector3(nextspot, 0, 0), Quaternion.identity);

    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "MovingPlatform")
        {

            bodypart = Random.Range(0, 5);

            bodyobjects[bodypart].GetComponent<Renderer>().material = collision.transform.GetComponent<Renderer>().material;
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

    private void ResetPlayer()
    {
        this.transform.position = startPos;
    }

}
