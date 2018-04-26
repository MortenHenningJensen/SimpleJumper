using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        RotateMe();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RotateMe()
    {
        this.transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360).OnComplete(RotateMe).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        HighscoreController.Instance.Score++;
    }
}
