using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurtleController : PlatformController
{

    [SerializeField]
    private float minSinkDelay;

    [SerializeField]
    private float maxSinkDelay;

    [SerializeField]
    private float riseDelay;

    private float sinkDelay;

    private float rotationSpeed = 0.5f;

    void Start()
    {
        this.sinkDelay = Random.Range(this.minSinkDelay, this.maxSinkDelay);
        Sink();
    }

    private void Sink()
    {
        NormalizeRotation();
        gameObject.transform.DOMoveY(-10f, 1).SetDelay(this.sinkDelay).OnStart(RotateDownwards).OnComplete(Rise);
    }

    private void Rise()
    {
        NormalizeRotation();
        gameObject.transform.DOMoveY(0.028f, 1).SetDelay(this.riseDelay).OnStart(RotateUpwards).OnComplete(Sink);
    }

    private void RotateDownwards()
    {
        gameObject.transform.DORotate(new Vector3(90, 0, 0), this.rotationSpeed);
    }

    private void RotateUpwards()
    {
        gameObject.transform.DORotate(new Vector3(-90, 0, 0), this.rotationSpeed);
    }

    private void NormalizeRotation()
    {
        gameObject.transform.DORotate(new Vector3(0, 0, 0), this.rotationSpeed);
    }
}
