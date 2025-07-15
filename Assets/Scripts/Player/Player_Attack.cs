using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public static Player_Attack instance;
    private Animator anim;
    private float countdown = 0.8f;
    private float timer;
    public float damage;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius = 1f;
    [Header("Skill2")]
    [SerializeField] private GameObject skillController;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        anim = GetComponentInChildren<Animator>();
        damage = PlayerHealth.instance.playerIndex.damage;
        skillController = GameObject.Find("Skill_Total");
    }
    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Skill2Attack()
    {
        if (skillController != null)
        {
            skillController.GetComponent<Skill_Controll>().ActiveSkill2();
        }
    }
    public void FinishSkill2()
    {
        anim.SetBool("isSkill2Active", false);
    }
    public void PlayerAttack()
    {
        if(timer <= 0f)
        {
            anim.SetBool("isAttacking", true);
            timer = countdown;
        }   
    }
    public void DamageEnemy()
    {
        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, 0.5f, enemyLayerMask);
        if(hitenemy.Length > 0)
        {
            hitenemy[0].GetComponent<Enemy_Health>().TakeDamage(damage);
        }
    }
    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        }
    }
}
