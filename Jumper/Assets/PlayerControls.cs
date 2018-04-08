using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    Rigidbody myBody;
    public Vector3 myForward;
    public int jumpforce = 20;

    public int score;

	// Use this for initialization
	void Start () {
        myBody = GetComponent<Rigidbody>();
        myForward = new Vector3(-1, 0, 0);
        score = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myBody.AddForce((myForward * jumpforce) + new Vector3(0, 8, 0), ForceMode.Impulse);
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
            transform.parent = null;
            //transform.Rotate(new Vector3(0, -1.2f, 0));
        }

    }

}
