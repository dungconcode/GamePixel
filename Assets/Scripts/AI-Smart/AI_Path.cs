using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy_Moving))]
public class AI_Path : MonoBehaviour
{
    //public static AI_Path Instance { get; private set; }
    public Transform player;
    [SerializeField] NavMeshAgent agent;

    [Header("Detection")]
    [SerializeField] private float sightRange = 10f; // tầm nhìn
    [SerializeField] private float fieldOfView = 360f;   // góc nhìn
    [SerializeField] private LayerMask obstructionMask;  // vật cản như tường

    [Header("Moving")]
    public bool isMoving = false; // Biến này có thể dùng để kiểm soát trạng thái di chuyển của AI

    [Header("Patrol Points")]
    public bool hasPatrolPoint = false;
    private Enemy_Patrol enemyPatrol;

    
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
        if (player == null)
        {
            agent.ResetPath(); // đứng yên
            isMoving = false;
            hasPatrolPoint = false;
            return;
        }
        AIPathFinding();
        if (agent.velocity.x < 0.1f && agent.velocity.x > -0.1f) return;
        if (agent.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Quay trái
        else
            transform.localScale = new Vector3(1, 1, 1); // Quay phải
    }
    
    private void AIPathFinding()
    {
        if (player == null) return; // Nếu không có player, không làm gì cả
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        bool inFOV = Vector3.Angle(transform.forward, dirToPlayer) < fieldOfView / 2; // góc nhìn Angle không > 120 hoặc 180 
        bool inRange = Vector3.Distance(transform.position, player.position) <= sightRange;

        if (inRange && inFOV && !Physics.Linecast(transform.position, player.position, obstructionMask))
        {
            // Player đang trong tầm nhìn rõ ràng
            agent.SetDestination(player.position);
            isMoving = true; // Đặt trạng thái di chuyển là true
            hasPatrolPoint = true;
        }
        else
        {
            agent.ResetPath(); // Reset đường đi của agent
            hasPatrolPoint = false;
            enemyPatrol.PatrolLogic2(); // Gọi logic tuần tra nếu không thấy player
        }
    }


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