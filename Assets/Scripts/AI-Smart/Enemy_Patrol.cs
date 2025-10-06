using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public interface IEnemyPatrolTick
{
    void OnPatrolTick();
}
public class Enemy_Patrol : MonoBehaviour, IEnemyPatrolTick
{
    public Vector2 patrolPoint;
    private AI_Path aiPath;
    private NavMeshAgent agent;
    public Vector3 targetPatrol;
    private float patrolRange = 5f;
    private float coutTime = 2f;

    private Rigidbody2D rb;
    private float moveSpeed = 30f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    public int tmp = 1;
    public bool isEnemyInRoom = false;

    private void Start()
    {
        aiPath = GetComponent<AI_Path>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.RegisterPatrol(this);
    }
    private void OnDisable()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.UnregisterPatrol(this);
    }
    public void OnPatrolTick()
    {
        PatrolLogic2();
    }
    public void Initialize(Vector2 spawnPoint)
    {
        patrolPoint = spawnPoint;
        EnemyPatrolPoint(); 
    }
    
    private void PatrolLogic2()
    {
        if (aiPath == null) return;
        if (!aiPath.hasPatrolPoint)
        {
            if (!aiPath.hasPatrolPoint && isEnemyInRoom)
            {
                if (tmp == 1)
                {
                    targetPatrol = transform.position;
                    patrolPoint = transform.position;
                    tmp = 0;
                }
            }
            if (coutTime > 0f)
            {
                aiPath.isMoving = false;
                agent.ResetPath();
                coutTime -= Time.deltaTime;
                return;
            }
            else
            {
                Vector3 direction = (targetPatrol - transform.position).normalized;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, groundLayer);
                if (hit.collider != null)
                {
                    EnemyPatrolPoint(); // Tạo điểm tuần tra mới
                    return;
                }
                aiPath.isMoving = true;
                if (direction.x > 0.01f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (direction.x < -0.01f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                    Vector3 moveStep = direction * moveSpeed * Time.deltaTime;
                rb.MovePosition(transform.position + moveStep);
                if (Vector2.Distance(transform.position, targetPatrol) < 0.3f)
                {
                    EnemyPatrolPoint(); 
                    coutTime = 2f; 
                }
            }
        }
        if(aiPath.hasPatrolPoint)
        {
            tmp = 1;
        }
    }
    private void EnemyPatrolPoint()
    {
        Vector3 centre = patrolPoint; 
        Vector2 random2D = Random.insideUnitCircle * patrolRange; 
        Vector3 randomPoint = centre + new Vector3(random2D.x, random2D.y, 0); 
        targetPatrol = randomPoint; 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(patrolPoint, patrolRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(targetPatrol, 0.3f);
    }
}
