using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : WeaponBase
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private GameObject arrowPrefab;
    private float arrowSpeed = 15f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Attack()
    {
        anim.SetBool("isAttacking", true);
    }

    // Event này sẽ được gọi từ animation
    public void ShootArrow()
    {
        if (arrowPrefab != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.AddForce(attackPoint.transform.right * arrowSpeed, ForceMode2D.Impulse);
        }
    }
    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
