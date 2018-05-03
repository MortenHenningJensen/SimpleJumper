using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private PlatformType myType;

    [SerializeField]
    private float sinkDelay;

    [SerializeField]
    private float riseDelay;

    public MoveDirection myDirection;

    public float moveSpeed;

    private bool hit;

    private int minZvalue = -20;
    private int maxZvalue = 20;

    private int resetPos;



    // Use this for initialization
    void Start()
    {
        Setup();
    }


    void Update()
    {
        if (myType == PlatformType.Static)
        {
            return;
        }
        if (myType == PlatformType.Strafe)
        {
            if (myDirection == MoveDirection.Right)
            {
                MoveRight();
            }
            if (myDirection == MoveDirection.Left)
            {
                MoveLeft();
            }
        }
    }

    private void MoveLeft()
    {
        gameObject.transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime);

        if (gameObject.transform.position.z >= maxZvalue)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, resetPos);
        }
    }

    private void MoveRight()
    {
        gameObject.transform.Translate(new Vector3(0, 0, -moveSpeed) * Time.deltaTime);

        if (gameObject.transform.position.z <= minZvalue)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, resetPos);
        }
    }

    private void Sink()
    {
        gameObject.transform.DOLocalMoveY(-0.854f, 1).SetDelay(this.sinkDelay).OnComplete(Rise);
    }

    private void Rise()
    {
        gameObject.transform.DOLocalMoveY(0.019f, 1).SetDelay(this.riseDelay).OnComplete(Sink);

    }

    private void Setup()
    {
        switch (myType)
        {
            case PlatformType.Strafe:
                if (myDirection == MoveDirection.Left)
                {
                    this.resetPos = -20;
                }
                if (myDirection == MoveDirection.Right)
                {
                    this.resetPos = 20;
                }
                break;
            case PlatformType.Sink:
                Sink();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
