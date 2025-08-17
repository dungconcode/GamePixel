using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bow_Attack : MonoBehaviour
{
    private Animator anim;
    private float countdown = 0.5f;
    private float timer;
    public float damage;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius = 1f;
    private Vector2 aimDirection = Vector2.right;

    [Header("Prefabs Item")]
    [SerializeField] private GameObject arrowPrefab;
    private float arrowSpeed = 10f;
    private bool isShooting = false;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        damage = PlayerHealth.instance.playerIndex.damage;
    }
    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        Debug.Log("Attack Bow");
        PlayerAttack();
    }
    public void PlayerAttack()
    {
        if (isShooting) return; // Prevent multiple attacks at the same time
        if (timer <= 0f)
        {
            isShooting = true; // Set shooting state to true when attacking
            //HandleAiming(); // Update aiming direction before attacking
            anim.SetBool("isAttacking", true);
            timer = countdown;
        }
    }
    public void DamageEnemy()
    {
        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, 0.5f, enemyLayerMask);
        if (hitenemy.Length > 0)
        {
            hitenemy[0].GetComponent<Enemy_Health>().TakeDamage(damage);
        }
    }
    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
        isShooting = false; // Reset shooting state after attack animation finishes
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        }
    }
    public void ShootArrow()
    {
        if (arrowPrefab != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.AddForce(attackPoint.transform.right * arrowSpeed, ForceMode2D.Impulse);
        }
    }
}
