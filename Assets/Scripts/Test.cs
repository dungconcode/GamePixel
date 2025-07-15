using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject pf = GameObject.FindGameObjectWithTag("Player");
            if (pf != null) player = pf.transform;
        }
        agent.updateRotation = false; // Tắt cập nhật xoay của agent
        agent.updateUpAxis = false; // Tắt cập nhật trục Y của agent
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }
}