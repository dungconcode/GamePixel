using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmart_Controller : MonoBehaviour
{
    private AI_Path aiPath;
    private Transform player;
    private Animator anim;
    [SerializeField] private Transform rotatePoint;
    void Start()
    {
        aiPath = GetComponent<AI_Path>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimationEnemy();
        EnemyAttack();
    }

    void AnimationEnemy()
    {
        anim.SetBool("isMoving", aiPath.isMoving);
    }
    private void EnemyAttack()
    {
        if (aiPath.isEnemyAttacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }
    public void StopEnemyAttack()
    {
        anim.SetBool("isAttacking", false);
        aiPath.isEnemyAttacking = false; // Reset trạng thái tấn công sau khi kết thúc
    }
}
