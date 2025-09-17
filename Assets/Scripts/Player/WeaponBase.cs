using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee = 0,     // kiếm, dao, cận chiến
    Chain = 1,    // đấm liên tục
    Magic = 2    // gậy phép, sách phép
}
public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Type")]
    public string weaponName;
    public WeaponType weaponType;
    public GameObject weaponPrefab;

    [Header("Weapon Settings")]

    public float fireRate = 1f;          

    protected float nextFireTime = 0f;

    public void TryAttack()
    {
        if (Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    protected abstract void Attack();
}
