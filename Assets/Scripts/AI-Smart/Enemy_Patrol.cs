using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Patrol : MonoBehaviour
{
    public Vector2 patrolPoint;
    private AI_Path aiPath;
    private NavMeshAgent agent;
    private Vector2 targetPatrol;
    private float patrolRange = 3f;
    private float coutTime = 2f;


    // Start is called before the first frame update
    private void Start()
    {
        aiPath = GetComponent<AI_Path>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void Initialize(Vector2 spawnPoint)
    {
        patrolPoint = spawnPoint;
        EnemyPatrolPoint(); 
    }
    
    public void PatrolLogic2()
    {
        if (!aiPath.hasPatrolPoint)
        {
            if (coutTime > 0f)
            {
                aiPath.isMoving = false;
                agent.ResetPath();
                coutTime -= Time.deltaTime;
                return;
            }
            else
            {
                
                aiPath.isMoving = true;
                agent.SetDestination(targetPatrol);
                if (Vector2.Distance(transform.position, targetPatrol) < 0.3f)
                {
                    EnemyPatrolPoint(); // Tạo điểm tuần tra mới
                    coutTime = 2f; // Reset thời gian đợi
                }
            }
        }
    }
    private void EnemyPatrolPoint()
    {
        Vector2 centre = patrolPoint;
        for (int i = 0; i < 10; i++) // Thử tối đa 10 lần
        {
            Vector2 randomPoint = centre + Random.insideUnitCircle * patrolRange;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 0.5f, NavMesh.AllAreas))
            {
                targetPatrol = hit.position;
                return;
            }
        }

        // Nếu không tìm được, fallback
        targetPatrol = patrolPoint;
    }
}
