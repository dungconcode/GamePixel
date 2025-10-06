using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyTick
{
    void Ontick();
}
[RequireComponent(typeof(NavMeshAgent))]
public class AI_Path : MonoBehaviour, IEnemyTick
{
    //public static AI_Path Instance { get; private set; }
    public Transform player;
    [SerializeField] NavMeshAgent agent;


    [Header("Detection")]
    [SerializeField] private float sightRange = 8f; // tầm nhìn
    [SerializeField] private float fieldOfView = 360f;   // góc nhìn
    [SerializeField] private LayerMask obstructionMask;  // vật cản như tường
    [SerializeField] private Transform rotatePoint;

    [Header("Moving")]
    public bool isMoving = false;

    [Header("Patrol Points")]
    public bool hasPatrolPoint = false;

    [Header("Frezon")]
    public bool isFrozen = false;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 0.5f;  
    [SerializeField] private float attackCooldown = 1f; 
    private float lastAttackTime = 0f;
    public bool isEnemyAttacking = false;

    private Enemy_AttackSmart enemyAttack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.updateRotation = false; // Tắt cập nhật xoay của agent
        agent.updateUpAxis = false; // Tắt cập nhật trục Y của agent
        enemyAttack = GetComponent<Enemy_AttackSmart>();
    }
    public void SetattackRanger(float attac_kRange)
    {
        this.attackRange = attac_kRange;
    }
    public void OnEnable()
    {
        EnemyManager.Instance.Register(this);
    }
    public void OnDisable()
    {
        EnemyManager.Instance.Unregister(this);
    }
    public void Ontick()
    {
        if (isFrozen)
        {
            agent.ResetPath();
            return;
        }
        if (isEnemyAttacking)
        {
            agent.isStopped = false; // cho phép skill điều khiển agent
            return;
        }
        Enemy_Patrol patrol = GetComponent<Enemy_Patrol>();
        if (player == null || !patrol.isEnemyInRoom)
        {
            agent.ResetPath(); // đứng yên
            isMoving = false;
            hasPatrolPoint = false;
            return;
        }
        AIPathFinding();
    }
    private void AIPathFinding()
    {
        if (player == null) return; // Nếu không có player, không làm gì cả
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        bool inFOV = Vector3.Angle(transform.forward, dirToPlayer) < fieldOfView / 2; // góc nhìn Angle không > 120 hoặc 180 
        bool inRange = Vector3.Distance(transform.position, player.position) <= sightRange;

        if (inRange && inFOV && !Physics.Linecast(transform.position, player.position, obstructionMask))
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (Mathf.Abs(distanceToPlayer - attackRange) < 0.4f) 
            {
                agent.isStopped = true;
                isMoving = false;
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, 0.5f, obstructionMask);
                    Enemy_Patrol patrol = GetComponent<Enemy_Patrol>();
                    if(patrol.isEnemyInRoom && enemyAttack != null)
                    {
                        enemyAttack.TryAttack();
                        //Debug.Log(isEnemyAttacking);
                        lastAttackTime = Time.time;
                    }
                       
                }
            }
            else if (distanceToPlayer <= attackRange) 
            {
                agent.isStopped = false;
                isMoving = true;
                Vector3 retreatDir = (transform.position - player.position).normalized; 
                Vector3 retreatPos = transform.position + retreatDir * 1.5f; 
                agent.SetDestination(retreatPos);

                Vector3 lookDir = (player.position - transform.position).normalized;
                if (lookDir.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1); // quay trái
                else
                    transform.localScale = new Vector3(1, 1, 1); // quay phải
            }
            else
            {
                Flip();
                //isEnemyAttacking = false;
                agent.isStopped = false;
                isMoving = true;
                agent.SetDestination(player.position);
            }

            //isMoving = true;
            hasPatrolPoint = true;
        }
        else
        {
            agent.ResetPath();
            hasPatrolPoint = false;
        }
    }
    private void Flip()
    {
        if (agent.velocity.x < 0.1f && agent.velocity.x > -0.1f) return;
        Vector3 lookDir2 = (player.position - transform.position).normalized;
        if (lookDir2.x < 0f)
            transform.localScale = new Vector3(-1, 1, 1); // quay trái
        else
            transform.localScale = new Vector3(1, 1, 1); // quay phải
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Vẽ vòng tròn thể hiện tầm nhìn

        Gizmos.color = Color.green;

        Vector3 forward = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-fieldOfView / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fieldOfView / 2, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.DrawRay(transform.position, leftRayDirection * sightRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * sightRange);
    }
}