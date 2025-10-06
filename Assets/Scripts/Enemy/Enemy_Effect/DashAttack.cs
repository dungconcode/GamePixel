using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/Trap/Dash")]
public class DashAttack : AttackEffectSO
{
    [SerializeField] private float dashDuration = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask playerLayer;
    public override void ApplyAttack(Transform enemy, Transform player)
    {
        enemy.GetComponent<MonoBehaviour>().StartCoroutine(DashAttackAction(enemy, player));
    }
    private IEnumerator DashAttackAction(Transform enemy, Transform player)
    {
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        AI_Path aiPath = enemy.GetComponent<AI_Path>();
        agent.updateRotation = false; // tránh bị xoay lung tung

        Vector3 dir = (player.position - enemy.position).normalized;
        float timer = 0f;
        CapsuleCollider2D coll = enemy.GetComponent<CapsuleCollider2D>();
        while (timer < dashDuration)
        {
            agent.speed = 100f;
            aiPath.SetattackRanger(0.5f);
            coll.isTrigger = true;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        coll.isTrigger = false;
        agent.speed = 10f;
        aiPath.SetattackRanger(4f);
    }
    
}
