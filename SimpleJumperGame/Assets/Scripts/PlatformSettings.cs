using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSettings : MonoBehaviour
{
    [SerializeField]
    private float shakeDuration;

    [SerializeField]
    private float shakeRandomness;

    [SerializeField]
    private int shakeVibrato;

    [SerializeField]
    private Vector3 shakeStrenght;

    void Start()
    {
        foreach (GameObject platform in RowHandler.Instance.RowTypes)
        {
            SetPlatformSettings(platform);
        }
    }

    private void SetPlatformSettings(GameObject platform)
    {
        Row row = platform.GetComponent<Row>();
        foreach (Transform obj in row.MyChildren)
        {
            obj.GetComponent<Platform>().SetShakeStats(this.shakeDuration, this.shakeStrenght, this.shakeVibrato, this.shakeRandomness);
        }
    }
}
