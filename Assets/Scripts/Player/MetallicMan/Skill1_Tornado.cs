using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Tornado : SkillBase
{
    [SerializeField] private GameObject skillPrefabs;
    [SerializeField] private Transform player;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    public override void Activate()
    {
        ActiveSkill();
    }
    public void ActiveSkill()
    {
        GameObject skillRecall = Instantiate(skillPrefabs, player.position , Quaternion.identity);
    }
}
