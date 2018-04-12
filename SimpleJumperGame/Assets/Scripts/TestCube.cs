using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour {


    private void OnEnable()
    {
        StartCoroutine(Kill());
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.up);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(2);
        PoolManager.Instance.DespawnObject(this.gameObject);
    }
}
