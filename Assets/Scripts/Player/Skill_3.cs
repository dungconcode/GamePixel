using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : MonoBehaviour
{
    [SerializeField] private Player_Controller playerController;
    [SerializeField] private Player_Attack playerAttack;

    private float speed = 3f;
    private float damage = 5f;
    private bool isActive = false;
    public void ActivateSkill(float timeToDelaySkill)
    {
        if (isActive) return;
        StartCoroutine(DelayTimeSkill(timeToDelaySkill));
    }
    IEnumerator DelayTimeSkill(float timeToDelaySkill)
    {
        isActive = true;
        playerController.speed += speed;
        playerAttack.damage += damage;
        yield return new WaitForSeconds(timeToDelaySkill);
        playerController.speed -= speed;
        playerAttack.damage -= damage;
        isActive = false;
    }
}
