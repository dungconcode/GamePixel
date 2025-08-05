using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : SkillBase
{
    [SerializeField] private Player_Controller playerController;
    [SerializeField] private Player_Attack playerAttack;
    [SerializeField] private Animator playerAnimator;

    private float speed = 3f;
    private float damage = 5f;
    private bool isActive = false;
    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
    }
    public override void Activate()
    {
        ActivateSkill(3f); 
    }
    public void ActivateSkill(float timeToDelaySkill)
    {
        if (isActive) return;
        StartCoroutine(DelayTimeSkill(timeToDelaySkill));
    }
    IEnumerator DelayTimeSkill(float timeToDelaySkill)
    {
        isActive = true;

        playerAnimator.SetLayerWeight(0, 0f);
        playerAnimator.SetLayerWeight(1, 1f);

        playerController.speed += speed;
        playerAttack.damage += damage;
        yield return new WaitForSeconds(timeToDelaySkill);

        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(1, 0f);

        playerController.speed -= speed;
        playerAttack.damage -= damage;
        isActive = false;
    }
}
