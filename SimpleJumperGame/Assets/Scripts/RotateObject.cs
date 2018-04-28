using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RotateMe();
    }

    private void RotateMe()
    {
        this.transform.DORotate(new Vector3(0, 360, 0), 7f, RotateMode.FastBeyond360).OnComplete(RotateMe).SetEase(Ease.Linear);
    }

}
