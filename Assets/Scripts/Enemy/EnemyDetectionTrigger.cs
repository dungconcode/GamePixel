using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionTrigger : MonoBehaviour
{
    private Enemy_Controller enemyController;

    private void Start()
    {
        enemyController = GetComponentInParent<Enemy_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyController.OnPlayerDetected(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyController.OnPlayerLost();
        }
    }
}