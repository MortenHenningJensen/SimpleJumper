using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabProjectile;

    public void FireWeapon()
    {
        GameObject newProjectile = PoolManager.Instance.SpawnObject("Projectile", this.transform.position, Quaternion.identity);
    }
}
