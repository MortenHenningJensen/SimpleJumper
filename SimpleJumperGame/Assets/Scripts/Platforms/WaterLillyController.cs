using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            this.hit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            this.hit = false;
        }
    }

    private void Update()
    {
        if (hit)
        {
            SinkLilly();
        }
        else
        {
            RiseLilly();
        }
    }

    private void SinkLilly()
    {
        if (gameObject.transform.position.y > this.sinkLevel)
        {
            gameObject.transform.Translate(new Vector3(0, -this.sinkSpeed, 0) * Time.deltaTime);
        }
    }

    private void RiseLilly()
    {
        if (this.gameObject.transform.position.y < this.normalYLevel)
        {
            this.gameObject.transform.Translate(new Vector3(0, this.riseSpeed, 0) * Time.deltaTime);
        }
    }
}
