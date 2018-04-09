using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour {


    [SerializeField]
    GameObject testObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T was pressed");
            //GameObject test = ObjectPool.Instance.Instantiate(testObject, new Vector3(0, 8, 0), Quaternion.identity);
        }
	}
}
