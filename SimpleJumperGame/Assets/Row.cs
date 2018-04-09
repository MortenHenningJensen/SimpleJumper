using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour {

    List<GameObject> myChildren;
    public Transform rowObject;
    Vector3 myPos;

	// Use this for initialization
	void Start () {
        myPos = transform.position;
        //foreach (var item in GetComponentsInChildren<GameObject>())
        //{
        //    myChildren.Add(item);
        //}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator DestrySelf()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
