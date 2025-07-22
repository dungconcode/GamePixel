using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Collision : MonoBehaviour
{
    private AI_Path aiPath;
    private NavMeshAgent agent;
    private Enemy_Moving enemyMoving;   
    private Rigidbody2D rb;

    private void Start()
    {
        aiPath =GetComponent<AI_Path>();
        agent = GetComponent<NavMeshAgent>();
        enemyMoving = GetComponent<Enemy_Moving>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            agent.enabled = false; // Tắt NavMeshAgent khi va chạm với người chơi
            aiPath.enabled = false; // Tắt AI_Path khi va chạm với người chơi   
            enemyMoving.enabled = false; // Tắt Enemy_Moving khi va chạm với người chơi
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cút");
            rb.velocity = Vector2.zero; // Dừng chuyển động của Enemy khi va chạm
            agent.enabled = true; // Bật lại NavMeshAgent khi không còn va chạm
            aiPath.enabled = true; // Bật lại AI_Path khi không còn va chạm
            enemyMoving.enabled = true; // Bật lại Enemy_Moving khi không còn va chạm
        }
    }
}
