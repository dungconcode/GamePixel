using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : WeaponBase
{
    [SerializeField] private Animator punch1_anim;
    [SerializeField] private Animator punch2_anim;
    private float timer;
    private float punchCooldown = 0.8f; 
    private float delayBetweenPunches = 0.2f;

    protected override void Attack()
    {
        StartCoroutine(PunchCombo());
    }
    IEnumerator PunchCombo()
    {
        punch1_anim.SetBool("Punch", true);
        yield return new WaitForSeconds(delayBetweenPunches);
        punch2_anim.SetBool("Punch", true);
    }
}