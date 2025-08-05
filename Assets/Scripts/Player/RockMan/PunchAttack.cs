using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] private Animator punch1_anim;
    [SerializeField] private Animator punch2_anim;
    private float timer;
    private float punchCooldown = 0.8f; // Cooldown time for the punch combo
    private float delayBetweenPunches = 0.2f;

    public void Attack()
    {
        PlayerAttack();
    }
    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }
    private void PlayerAttack()
    {
        if(timer <= 0f)
        {
            StartCoroutine(PunchCombo());
            timer = punchCooldown;
        }
    }
    IEnumerator PunchCombo()
    {
        punch1_anim.SetBool("Punch", true);
        yield return new WaitForSeconds(delayBetweenPunches);
        punch2_anim.SetBool("Punch", true);
    }
}
