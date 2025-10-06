using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private float attackCooldown = 1f;
    public float attackDelay = 0.5f;

    private Transform player;

    private Animator anim;
    private AI_Path aiPath;

    public bool isDashAttack;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        aiPath = GetComponent<AI_Path>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

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
        if (anim != null)
        {
            anim.SetBool("isAttack", true);
        }

        if (isDashAttack)
        {
            RandomSkill();
        }
        yield return new WaitForSeconds(attackCooldown);
        aiPath.isEnemyAttacking = false;
        anim.SetBool("isAttack", false);

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
        float retreatDistance = 1.5f;

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

        // KHÔNG ResetPath ngay sau SetDestination.
        aiPath.isEnemyAttacking = false;
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
}
