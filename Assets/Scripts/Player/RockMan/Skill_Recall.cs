using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Recall : SkillBase
{
    [SerializeField] private GameObject skillPrefabs;
    [SerializeField] private Player_Controller player;
    [SerializeField] private float recallDistance = 3f;
    private void Start()
    {
        player = Player_Controller.Instance;
    }
    public override void Activate()
    {
        ActiveSkill();
    }
    public void ActiveSkill()
    {
        Vector3 direction = player.directionPlayer.normalized;
        if (direction == Vector3.zero)
        {
            direction = player.transform.right;
        }
        Vector3 summonDistance = player.transform.position + direction * recallDistance;
        GameObject skillRecall = Instantiate(skillPrefabs, summonDistance, Quaternion.identity);
        if (direction.x < 0)
            skillRecall.transform.localScale = new Vector3(-1, 1, 1);
        else
            skillRecall.transform.localScale = new Vector3(1, 1, 1);
    }
}
