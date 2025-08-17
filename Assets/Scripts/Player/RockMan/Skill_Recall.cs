using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Recall : SkillBase
{
    [SerializeField] private GameObject skillPrefabs;
    [SerializeField] private Player_Controller player;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float recallDistance = 3f;
    private void Start()
    {
        player = Player_Controller.Instance;
        if(attackPoint == null)
        {
            attackPoint = GameObject.Find("AttackPoint").transform;
        }
    }
    public override void Activate()
    {
        ActiveSkill();
    }
    public void ActiveSkill()
    {
        Vector3 direction = attackPoint.right.normalized;
        if (direction == Vector3.zero)
        {
            direction = Vector3.right;
        }
        Vector3 summonDistance = attackPoint.position + direction * recallDistance;
        GameObject skillRecall = Instantiate(skillPrefabs, summonDistance , Quaternion.identity);
        if (direction.x < 0)
            skillRecall.transform.localScale = new Vector3(-1, 1, 1);
        else
            skillRecall.transform.localScale = new Vector3(1, 1, 1);
    }
}
