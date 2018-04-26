using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lifeTime;

    private void OnEnable()
    {
        StartCoroutine(PoolManager.Instance.DespawnObjectTimer(this.gameObject, this.lifeTime));
    }

    private void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
