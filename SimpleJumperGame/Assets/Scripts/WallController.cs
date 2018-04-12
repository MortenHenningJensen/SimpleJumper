using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField]
    private float newZPosition;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            MoveObject(other.gameObject);
        }
    }

    private void MoveObject(GameObject go)
    {
        go.transform.position = go.transform.position - new Vector3(go.transform.position.x, go.transform.position.y, this.newZPosition);
    }
}
