using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AI_Path : MonoBehaviour
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
    public bool isMoving = false; // Biến này có thể dùng để kiểm soát trạng thái di chuyển của AI

    [Header("Patrol Points")]
    public bool hasPatrolPoint = false;
    private Enemy_Patrol enemyPatrol;

    [Header("Frezon")]
    public bool isFrozen = false;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 0.5f;  
    [SerializeField] private float attackCooldown = 1f; 
    private float lastAttackTime = 0f;
    public bool isEnemyAttacking = false;
    [SerializeField] private float attackDelay = 0.3f; 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        enemyPatrol = GetComponent<Enemy_Patrol>();
        agent.updateRotation = false; // Tắt cập nhật xoay của agent
        agent.updateUpAxis = false; // Tắt cập nhật trục Y của agent
    }
    private void Update()
    {
        if (isFrozen)
        {
            agent.ResetPath();
            return;
        }
        if (player == null)
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

            if (Mathf.Abs(distanceToPlayer - attackRange) < 0.4f) // gần bằng attackRange
            {
                agent.isStopped = true;
                isMoving = false;
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else if (distanceToPlayer < attackRange) // quá gần -> lùi ra
            {
                agent.isStopped = false;
                isMoving = true;
                Vector3 retreatDir = (transform.position - player.position).normalized; // hướng ngược player
                Vector3 retreatPos = transform.position + retreatDir * 1.5f; // lùi ra thêm 1.5m
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
                isEnemyAttacking = false;
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
            Flip();
            hasPatrolPoint = false;
            enemyPatrol.PatrolLogic2();
        }
    }
    private void Flip()
    {
        if (agent.velocity.x < 0.1f && agent.velocity.x > -0.1f) return;
        if (agent.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Quay trái
        else
            transform.localScale = new Vector3(1, 1, 1); // Quay phải
    }
    private void Attack()
    {
        if(!isEnemyAttacking)
        {
            StartCoroutine(AttackAfterDelay());
        }
    }
    private IEnumerator AttackAfterDelay()
    {
        isEnemyAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        yield return new WaitForSeconds(attackCooldown); 
        isEnemyAttacking = false; 
    }
    //private bool IsObstacleInFront()
    //{
    //    Vector2 direction = (player.position - transform.position).normalized;
    //    float distance = Vector2.Distance(transform.position, player.position);
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstructionMask);
    //    return hit.collider != null;
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Vẽ vòng tròn thể hiện tầm nhìn

        Gizmos.color = Color.green;

        // Vẽ hình nón (hình quạt) để thể hiện góc nhìn (fieldOfView)
        Vector3 forward = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-fieldOfView / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fieldOfView / 2, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.DrawRay(transform.position, leftRayDirection * sightRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * sightRange);
    }
}