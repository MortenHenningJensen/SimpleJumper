using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private Vector3 offset;

	void Start ()
    {
        this.offset = this.transform.position - this.target.transform.position;
	}
	
	void Update ()
    {
        this.transform.position = new Vector3(this.target.transform.position.x + this.offset.x, this.transform.position.y,  this.target.transform.position.z + this.offset.z);
	}
}
