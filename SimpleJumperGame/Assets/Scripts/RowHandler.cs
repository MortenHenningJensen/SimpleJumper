using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowHandler : MonoBehaviour
{
    public static RowHandler Instance;

    [Tooltip("Row settings")]
    [SerializeField]
    private List<GameObject> rowTypes;

    [SerializeField]
    private GameObject rowSpawner;

    [SerializeField]
    private GameObject rowDespawner;

    [SerializeField]
    private GameObject startPlatform;

    [SerializeField]
    private int nrOfStartRows;

    [SerializeField]
    private int distBetweenRows;

    private List<GameObject> rows = new List<GameObject>();

    public List<GameObject> RowTypes
    {
        get
        {
            return rowTypes;
        }

        set
        {
            rowTypes = value;
        }
    }

    private void Start()
    {
        Instance = this;
        //Spawn a set nr of rows when the game starts
        Vector3 newPosition = startPlatform.transform.position + new Vector3(distBetweenRows, 0, 0);
        for (int i = 0; i < nrOfStartRows; i++)
        {
            SpawnRow(newPosition);
            newPosition.x += distBetweenRows;
        }
    }

    private void SpawnRow(Vector3 position)
    {
        GameObject newRow;
        int rnd = Random.Range(0, 4);
        string rowPool = "";
        switch (rnd)
        {
            case 1:
                rowPool = "LilyRow";
                break;
            case 2:
                rowPool = "RaftRowFour";
                break;
            case 3:
                rowPool = "RaftRow";
                break;
            default:
                rowPool = "RaftRowFour";
                break;
        }
        newRow = PoolManager.Instance.SpawnObject(rowPool, position, Quaternion.identity);
        rows.Add(newRow);
    }

    private void DespawnRow(GameObject row)
    {
        rows.Remove(row);
        PoolManager.Instance.DespawnObject(row);
    }
}
