using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Row"))
        {
            RowHandler.Instance.DespawnRow(other.gameObject);
        }
    }
}
