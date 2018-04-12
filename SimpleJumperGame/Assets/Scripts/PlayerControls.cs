using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public Vector3 myForward;
    public int jumpforce = 10;

    public int score;
    public int jumpCounter;

    [SerializeField]
    GameObject[] bodyobjects;
    public int bodypart;

    public GameObject rowObject;
    Quaternion test;
    public Vector3 firstSpawn;


    // Use this for initialization
    void Start()
    {
        this.startPos = this.transform.position;
        myBody = GetComponent<Rigidbody>();
        myForward = new Vector3(-1, 0, 0);
        score = 0;
        jumpCounter = 0;
        firstSpawn = new Vector3(28, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !this.isJumping)
        {
            //myBody.AddForce((myForward * jumpforce) + new Vector3(0, 8, 0), ForceMode.Impulse);
            this.transform.DOJump(this.jumpDestination.transform.position, 5, 0, 1f).SetEase(this.jumpEase).OnStart(OnJump).OnComplete(() => this.isJumping = false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !this.isJumping)
        {
            this.transform.DOJump(this.superJumpDestination.transform.position, 5, 0, 1f).SetEase(this.jumpEase).OnStart(() => this.isJumping = true).OnComplete(() => this.isJumping = false);
        }

        if (this.transform.position.y <= -5)
        {
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

        GameObject newRow = Instantiate(rowObject, new Vector3(nextspot, 0, 0), test);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "MovingPlatform")
        {
            //while (bodyobjects[bodypart].GetComponent<Renderer>().material != collision.transform.GetComponent<Renderer>().material)
            //{
            //    bodypart = Random.Range(0, bodyobjects.Length + 1);
            //}
            bodypart = Random.Range(0, 5);

            bodyobjects[bodypart].GetComponent<Renderer>().material = collision.transform.GetComponent<Renderer>().material;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;

            //transform.Rotate(new Vector3(0, -1.2f, 0));
        }
        else
        {
            Debug.Log(collision);

            transform.parent = null;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            collision.transform.parent.GetComponent<Row>().startDestroySelf();
         //   StartCoroutine(collision.transform.parent.GetComponent<Row>().DestrySelf());
            transform.parent = null;
        }

    }

    private void ResetPlayer()
    {
        this.transform.position = startPos;
    }

}
