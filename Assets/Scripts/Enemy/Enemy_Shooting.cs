using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefabs;
    private Transform playerTransform;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float bulletForce = 20f;

    [SerializeField] private AI_Path aiPath;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        StartAttack();
    }
    public void Shooting()
    {
        
        GameObject bullet = Instantiate(bulletPrefabs, attackPoint.position, attackPoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 shootDir = (playerTransform.position - attackPoint.position).normalized;
        rb.AddForce(attackPoint.right * bulletForce, ForceMode2D.Impulse);
    }
    private void StartAttack()
    {
        if(aiPath.isEnemyAttacking)
        {
            anim.SetBool("isAttacking", true);
        }

    }
    public void StopShooting()
    {
        anim.SetBool("isAttacking", false);
        aiPath.isEnemyAttacking = false;
    }
}
