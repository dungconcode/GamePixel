using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_RecallShark : SkillBase
{
    [SerializeField] private GameObject sharkPrefabs;
    [SerializeField] private Player_Controller player;
    public float recallDistance = 1f;
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
        if(direction == Vector3.zero)
        {
            direction = player.transform.right;
        }
        Vector3 summonDistance = player.transform.position + direction * recallDistance;
        GameObject skillRecall = Instantiate(sharkPrefabs, summonDistance, Quaternion.identity);
        if(direction.x < 0)
            skillRecall.transform.localScale = new Vector3(-1, 1, 1);
        else
            skillRecall.transform.localScale = new Vector3(1, 1, 1);
    }
}
