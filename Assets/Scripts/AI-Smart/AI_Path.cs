using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy_Moving))]
public class AI_Path : MonoBehaviour
{
    public static AI_Path Instance { get; private set; }
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agent;

    [Header("Detection")]
    [SerializeField] private float sightRange = 10f; // tầm nhìn
    [SerializeField] private float fieldOfView = 360f;   // góc nhìn
    [SerializeField] private LayerMask obstructionMask;  // vật cản như tường

    [Header("Moving")]
    public bool isMoving = false; // Biến này có thể dùng để kiểm soát trạng thái di chuyển của AI

    [Header("Patrol Points")]
    [SerializeField] private Transform patrolPoint;
    private Vector2 targetPatrol;
    private float patrolRange = 3f;
    private bool hasPatrolPoint = false;
    private float coutTime = 0.2f; // Thời gian đợi trước khi di chuyển đến điểm tuần tra mới

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent.updateRotation = false; // Tắt cập nhật xoay của agent
        agent.updateUpAxis = false; // Tắt cập nhật trục Y của agent
    }
    private void Update()
    {
        if (player == null) return;
        AIPathFinding();
        if (agent.velocity.x < 0.1f && agent.velocity.x > -0.1f) return;
        if (agent.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Quay trái
        else
            transform.localScale = new Vector3(1, 1, 1); // Quay phải
    }
    private void AIPathFinding()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        bool inFOV = Vector3.Angle(transform.forward, dirToPlayer) < fieldOfView / 2; // góc nhìn Angle không > 120 hoặc 180 
        bool inRange = Vector3.Distance(transform.position, player.position) <= sightRange;

        if (inRange && inFOV && !Physics.Linecast(transform.position, player.position, obstructionMask))
        {
            // Player đang trong tầm nhìn rõ ràng
            agent.SetDestination(player.position);
            isMoving = true;
            hasPatrolPoint = true;
        }
        else
        {
            isMoving = false;
            agent.ResetPath(); // Reset đường đi của agent
            hasPatrolPoint = false;
            PatrolLogic();

        }
    }
    private void EnemyPatrolPoint()
    {
        Vector2 centre = (Vector2)patrolPoint.position;
        targetPatrol = centre + Random.insideUnitCircle * patrolRange;
    }
    private void PatrolLogic()
    {
        if (!isMoving)
        {
            if (coutTime > 0f)
            {
                coutTime -= Time.deltaTime;
                agent.ResetPath();
                return;
            }
            isMoving = true;
            if (!hasPatrolPoint)
            {
                Debug.Log("Tạo điểm tuần tra mới");
                agent.SetDestination(targetPatrol);
                if (Vector2.Distance(transform.position, targetPatrol) < 0.2f)
                {
                    EnemyPatrolPoint(); // Tạo điểm tuần tra mới
                    coutTime = 5f; // Reset thời gian đợi
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(patrolPoint.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPatrol, patrolRange);
    }
}
