using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    PoolManager objectPool;

    // Use this for initialization
    void Start()
    {
        objectPool = PoolManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            objectPool.SpawnObject("Cube", transform.position, Quaternion.identity);
        }
    }
}
