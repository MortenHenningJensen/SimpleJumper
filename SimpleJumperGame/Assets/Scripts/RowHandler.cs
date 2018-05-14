using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection { Left, Right }

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

    [SerializeField]
    private MoveDirection myDirection;

    public MoveDirection MyDirection
    {
        get
        {
            return myDirection;
        }

        set
        {
            myDirection = value;
        }
    }

    private void Start()
    {
        Instance = this;
        PlayerControls.OnPlayerDeath += OnPlayerDeath;
        myDirection = MoveDirection.Left;

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

        //string prevRow = "";

        for (int i = 0; i < nrOfSpawns; i++)
        {
            int rnd = Random.Range(0, 5);
            string rowPool = "";


            //switch (prevRow)
            //{
            //    case "LilyRow":
            //          rowPol = NewRowName(1);
            //        break;
            //    case "RaftRowFour":
            //          rowPol = NewRowName(2);
            //        break;
            //    case "RaftRow":
            //          rowPol = NewRowName(3);
            //        break;
            //    case "TurleRow":
            //          rowPol = NewRowName(4);
            //        break;
            //    case "":
            //          rowPol = NewRowName(5);
            //        break;
            //}

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
                case 4:
                    rowPool = "TurtleRow";
                    break;
                default:
                    rowPool = "RaftRowFour";
                    break;
            }
            newRowGO = PoolManager.Instance.SpawnObject(rowPool, position, Quaternion.identity);
            position.x += distBetweenRows;
            Row newRow = newRowGO.GetComponent<Row>();
            rows.Add(newRowGO);
            //prevRow = rowPool;
        }
    }

    private string NewRowName(int method)
    {
        string test = "";
        int rnd = Random.Range(0, 5);

        if (method == 1)
        {
            switch (rnd)
            {
                case 1:
                    test = "RaftRowFour";
                    break;
                case 2:
                    test = "RaftRowFour";
                    break;
                case 3:
                    test = "RaftRow";
                    break;
                case 4:
                    test = "TurtleRow";
                    break;
                case 5:
                    test = "RaftRowFour";
                    break;
            }

        }
        else if (method == 2)
        {
            switch (rnd)
            {
                case 1:
                    test = "LilyRow";
                    break;
                case 2:
                    test = "RaftRowFour";
                    break;
                case 3:
                    test = "RaftRow";
                    break;
                case 4:
                    test = "TurtleRow";
                    break;
                case 5:
                    test = "RaftRowFour";
                    break;
            }

        }
        else if (method == 3)
        {
            switch (rnd)
            {
                case 1:
                    test = "LilyRow";
                    break;
                case 2:
                    test = "RaftRowFour";
                    break;
                case 3:
                    test = "RaftRow";
                    break;
                case 4:
                    test = "TurtleRow";
                    break;
                case 5:
                    test = "RaftRowFour";
                    break;
            }

        }
        else if (method == 4)
        {
            switch (rnd)
            {
                case 1:
                    test = "RaftRow";
                    break;
                case 2:
                    test = "RaftRowFour";
                    break;
                case 3:
                    test = "RaftRow";
                    break;
                case 4:
                    test = "RaftRowFour";
                    break;
                case 5:
                    test = "RaftRowFour";
                    break;
            }

        }
        else if (method == 5)
        {
            //String to find the right pool
            switch (rnd)
            {
                case 1:
                    test = "LilyRow";
                    break;
                case 2:
                    test = "RaftRowFour";
                    break;
                case 3:
                    test = "RaftRow";
                    break;
                case 4:
                    test = "TurtleRow";
                    break;
                case 5:
                    test = "RaftRowFour";
                    break;
            }
        }

        return test;
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

    public void OnStart()
    {
        Vector3 newPosition = startPlatform.transform.position + new Vector3(distBetweenRows, 0, 0);
        SpawnRows(newPosition, this.rowsToSpawn);
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
