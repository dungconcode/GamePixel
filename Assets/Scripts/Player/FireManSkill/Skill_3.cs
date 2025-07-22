using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : MonoBehaviour
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
    private void OnEnable()
    {
        playerAnimator.SetLayerWeight(0, 0f);
        playerAnimator.SetLayerWeight(1, 1f);
    }
    private void OnDisable()
    {
        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(1, 0f);
    }
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
