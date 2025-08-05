using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Dragon : SkillBase
{
    [SerializeField] private GameObject dragonPrefab;
    public Transform player;
    public Transform spawnPoint;
    
    public override void Activate()
    {
        ActiveSkill();
    }
    public void ActiveSkill()
    {
        Debug.Log("Fuck");
        GameObject dragon = Instantiate(dragonPrefab, spawnPoint.position, spawnPoint.rotation);
        dragon.GetComponentInChildren<DragonFly>().Init(player);
        Destroy(dragon, 3f); // Giả sử rồng sẽ tồn tại trong 5 giây
    }
}