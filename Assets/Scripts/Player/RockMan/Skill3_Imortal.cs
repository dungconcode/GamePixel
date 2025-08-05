using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Imortal : SkillBase
{
    public GameObject shieldPrefab;
    public Transform shieldSpawnPoint;

    public override void Activate()
    {
        ActivateShield();
    }
    public void ActivateShield()
    {
        GameObject shieldObj = Instantiate(shieldPrefab, shieldSpawnPoint.position, Quaternion.identity);
        shieldObj.transform.SetParent(shieldSpawnPoint);
    }
}
