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
    private GameObject startPlatform;

    [SerializeField]
    private int rowsToSpawn;

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

    public List<GameObject> Rows
    {
        get
        {
            return rows;
        }

        set
        {
            rows = value;
        }
    }

    public int DistBetweenRows
    {
        get
        {
            return distBetweenRows;
        }
    }

    private void Start()
    {
        Instance = this;
        PlayerControls.OnPlayerDeath += OnPlayerDeath;
        
        OnStart();
    }


    /// <summary>
    /// Spawns a set nr of rows
    /// </summary>
    /// <param name="position"></param>
    /// <param name="nrOfSpawns"></param>
    public void SpawnRows(Vector3 position, int nrOfSpawns)
    {
        GameObject newRowGO;

        for (int i = 0; i < nrOfSpawns; i++)
        {
            int rnd = Random.Range(0, 4);
            string rowPool = "";

            //String to find the right pool
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
            newRowGO = PoolManager.Instance.SpawnObject(rowPool, position, Quaternion.identity);
            position.x += distBetweenRows;
            Row newRow = newRowGO.GetComponent<Row>();
            SetRowDirection(newRow);
            rows.Add(newRowGO);
        }
    }

    /// <summary>
    /// Used to remove rows from the game
    /// </summary>
    /// <param name="row"></param>
    public void DespawnRow(GameObject row)
    {
        rows.Remove(row);
        PoolManager.Instance.DespawnObject(row);
    }

    private void OnPlayerDeath()
    {
        ClearAllRows();
        OnStart();
    }

    private void OnStart()
    {
        Vector3 newPosition = startPlatform.transform.position + new Vector3(distBetweenRows, 0, 0);
        SpawnRows(newPosition, this.rowsToSpawn);
    }

    //Is suppose to decide which direction the rows platforms move
    private void SetRowDirection(Row row)
    {
        row.MyDirection = (MoveDirection)Random.Range(0, 2);

        foreach (Transform platform in row.MyChildren)
        {
            platform.GetComponent<Platform>().MyDirection = row.MyDirection;
        }
    }

    /// <summary>
    /// Disables all rows that are in the level
    /// </summary>
    private void ClearAllRows()
    {
        for (int i = rows.Count - 1; i >= 0; i--)
        {
            DespawnRow(rows[i]);
        }
    }
}
