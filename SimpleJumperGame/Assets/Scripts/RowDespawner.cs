using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowDespawner : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Row"))
        {
            PoolManager.Instance.DespawnObject(other.gameObject);
        }
    }

}
