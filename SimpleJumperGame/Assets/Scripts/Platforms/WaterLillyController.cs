using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterLillyController : PlatformController
{
    [SerializeField]
    private float sinkLevel;

    [SerializeField]
    private float sinkSpeed;

    [SerializeField]
    private float riseSpeed;

    private float normalYLevel;

    private bool hit;

    private void Start()
    {
        this.normalYLevel = this.transform.position.y;
    }
    //Work in progress...
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        SinkLilly();
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        RiseLilly();
    //    }
    //}

    //private void SinkLilly()
    //{
    //    if (hit)
    //    {
    //        this.gameObject.transform.DOLocalMoveY(-8, 5);
    //    }
    //}

    //private void RiseLilly()
    //{
    //    this.gameObject.transform.DOLocalMoveY(normalYLevel, 2);
    //}
}
