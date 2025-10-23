using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Burst.Intrinsics.X86.Avx;

public interface IAttackEffect
{
    void ApplyAttack(Transform enemy, Transform player);
}
public abstract class AttackEffectSO : ScriptableObject, IAttackEffect
{
    public abstract void ApplyAttack(Transform enemy, Transform player);
}
public class Enemy_AttackSmart : MonoBehaviour
{
    [SerializeField] private List<AttackEffectSO> attackEffectSOs;  // phải là AttackEffectSO
    private List<IAttackEffect> attackEffects = new List<IAttackEffect>();

    private float attackCooldown = 0.5f;
    public float attackDelay = 0.5f;

    private Transform player;

    private Animator anim;
    private AI_Path aiPath;
    private CapsuleCollider2D attackCollider;
    public bool isDashAttack;
    private float tmp;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        aiPath = GetComponent<AI_Path>();
        attackCollider = GetComponent<CapsuleCollider2D>();
        attackCollider.isTrigger = false;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        tmp = aiPath.attackRange;
        attackEffects.Clear();
        foreach (var so in attackEffectSOs)
        {
            if (so is IAttackEffect effect)
                attackEffects.Add(effect);
        }
    }
    public void TryAttack()
    {
        if (!aiPath.isEnemyAttacking && attackEffectSOs.Count >= 0)
            StartCoroutine(AttackRoutine());
    }
    private IEnumerator AttackRoutine()
    {
        aiPath.isEnemyAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        
        if (anim != null && aiPath.isEnemyAttacking)
        {
            anim.SetBool("isAttack", true);
        }
        if (isDashAttack)
        {
            RandomSkill();
        }
        yield return new WaitForSeconds(attackCooldown);
        aiPath.isEnemyAttacking = false;
        Debug.Log(aiPath.isEnemyAttacking);
        if (!aiPath.isEnemyAttacking)
        {
            anim.SetBool("isAttack", false);
            attackCollider.isTrigger = false;
            aiPath.attackRange = tmp;
            Debug.Log(aiPath.attackRange);
        }
        

        StartCoroutine(RetreatAfterAttack());
    }
    private IEnumerator RetreatAfterAttack()
    {
        aiPath.isEnemyAttacking = true;
        
        var agent = GetComponent<NavMeshAgent>();
        if (agent == null || aiPath.player == null)
        {
            aiPath.isEnemyAttacking = false;
            yield break;
        }

        Vector3 retreatDir = (transform.position - aiPath.player.position).normalized;
        float retreatDistance = 0.5f;

        Vector3 targetPos = transform.position + retreatDir * retreatDistance;

        agent.isStopped = false;
        agent.speed = 10f;

        agent.SetDestination(targetPos);

        // chờ tới nơi (hoặc timeout nhỏ nếu thích)
        float t = 0f, timeout = 0.5f;
        while (t < timeout && (agent.pathPending || agent.remainingDistance > 0.05f))
        {
            t += Time.deltaTime;
            yield return null;
        }
        if(isDashAttack)
        {
            RandomSkill();
        }
        aiPath.isEnemyAttacking = false;
        if(!aiPath.isEnemyAttacking)
        {
            anim.SetBool("isAttack", false);
            attackCollider.isTrigger = false;
            aiPath.attackRange = tmp;
            Debug.Log(aiPath.attackRange);
        }
        agent.speed = 5f;
    }

    public void RandomSkill()
    {

        if (attackEffects.Count == 0)
        {
            return;
        }
        int index = Random.Range(0, attackEffects.Count);
        
        attackEffects[index].ApplyAttack(transform, player);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(1);
            other.gameObject.GetComponent<Player_KnockBack>()?.KnockBack(transform, 30f);
        }
    }
    public void TurnOnTigger()
    {
        attackCollider.isTrigger = true;
    }
}
