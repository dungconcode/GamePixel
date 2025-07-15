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

        }
        else
        {
            // Nếu muốn enemy đứng yên hoặc roam thêm, xử lý ở đây
            agent.ResetPath();
            isMoving = false;
        }
    }
}
