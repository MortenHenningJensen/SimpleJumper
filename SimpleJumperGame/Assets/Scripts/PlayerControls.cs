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
    private WeaponController shooter;

    [SerializeField]
    private Ease jumpEase;

    [SerializeField]
    private float jumpDesYValue;

    [SerializeField]
    private float timeBeforeReset;

    private Vector3 startPos;

    private bool isJumping;
    Rigidbody myBody;

    public int jumpCounter;

    [SerializeField]
    GameObject[] sideObjects;

    private int objectIterator;

    PoolManager objectPool;

    private bool playerDying;

    public bool gameEnded;

    private Vector3[] sideStartPos;


    // Use this for initialization
    void Start()
    {
        Instance = this;
        this.startPos = this.transform.position;
        myBody = GetComponent<Rigidbody>();
        jumpCounter = 0;
        objectPool = PoolManager.Instance;
        sideStartPos = new Vector3[10];
        SideStartPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !this.isJumping)
        {
            Jump();
            // this.transform.DOJump(this.jumpDestination.transform.position, 2, 0, 0.8f).SetEase(this.jumpEase).OnStart(OnJump).OnComplete(() => this.isJumping = false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !this.isJumping)
        {
            SuperJump();
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    shooter.FireWeapon();
        //}

        if (this.transform.position.y <= -5 && !this.playerDying)
        {
            // SceneManager.LoadScene(0);
            StartCoroutine(KillPlayer());
        }

    }

    public void Jump()
    {
        this.transform.parent = null;
        this.transform.DOJump(new Vector3(RowHandler.Instance.Rows[this.jumpCounter].transform.position.x, this.jumpDesYValue, this.transform.position.z), 2, 0, 0.8f).SetEase(this.jumpEase).OnStart(() => { OnJump(); SpawnNewRows(1); });
    }

    private void SuperJump()
    {
        this.transform.DOJump(new Vector3(RowHandler.Instance.Rows[this.jumpCounter + 1].transform.position.x, this.jumpDesYValue, this.transform.position.z), 2, 0, 0.8f).SetEase(this.jumpEase).OnStart(()=> { OnJump();  SpawnNewRows(2); });
    }

    public void OnJump()
    {
        this.isJumping = true;
        this.jumpCounter++;

        if (jumpCounter % 5 == 0)
        {
            MoveSides();
        }
    }


    public void SpawnNewRows(int nrOfRows)
    {
        RowHandler.Instance.SpawnRows(RowHandler.Instance.Rows[RowHandler.Instance.Rows.Count - 1].transform.position + new Vector3(RowHandler.Instance.DistBetweenRows, 0, 0), nrOfRows);
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

    public void SideStartPos()
    {
        for (int i = 0; i < sideObjects.Length; i++)
        {
            sideStartPos[i] = sideObjects[i].transform.position;
        }
    }

    public void ResetSides()
    {
        for (int i = 0; i < sideObjects.Length; i++)
        {
            sideObjects[i].transform.position = sideStartPos[i];
        }
        objectIterator = 0;
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
        this.playerDying = true;
        //Set time before reset, to the lenght of death animation
        yield return new WaitForSeconds(this.timeBeforeReset);
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
        ResetSides();
        this.transform.position = startPos;
        this.transform.parent = null;
        this.playerDying = false;
        this.jumpCounter = 0;
        this.gameEnded = true;
    }


}
