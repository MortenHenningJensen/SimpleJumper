using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum followType { XZ, X}
public class FollowTargetController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private followType ft;

    private Vector3 offset;

	void Start ()
    {
        this.offset = this.transform.position - this.target.transform.position;
	}
	
	void Update ()
    {
        switch (ft)
        {
            case followType.XZ:
                this.transform.position = new Vector3(this.target.transform.position.x + this.offset.x, this.transform.position.y, this.target.transform.position.z + this.offset.z);
                break;
            case followType.X:
                this.transform.position = new Vector3(this.target.transform.position.x + this.offset.x, this.transform.position.y, this.transform.position.z);
                break;
        }

	}
}
