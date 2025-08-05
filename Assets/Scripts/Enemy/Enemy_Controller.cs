using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public Enemy_Index enemyIndex;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;
    private bool isMoving;
    private int facingDirection = 1;

    [SerializeField] private LayerMask obstacleLayer;

    private Animator anim;
    private void Start()
    {
        if (Player_Controller.Instance != null)
        {
            player = Player_Controller.Instance.transform;
        }


        if (enemyIndex != null)
        {
            speed = enemyIndex.speed;
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (player != null && isMoving)
        {
            if(player.position.x < transform.position.x && facingDirection < 0 || player.position.x > transform.position.x && facingDirection > 0)
            {
                Flip();
            }
            if (IsObstacleInFront())
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
                return;
            }

            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed; 
        }
        AnimationEnemy();
    }
    private bool IsObstacleInFront()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);
        return hit.collider != null;
    }
    public void OnPlayerDetected(Transform playerTransform)
    {
        if (player == null)
            player = playerTransform;

        isMoving = true;
    }

    public void OnPlayerLost()
    {
        isMoving = false;
        rb.velocity = Vector2.zero;
    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    private void AnimationEnemy()
    {
        if(isMoving)
        {
            anim.SetInteger("Moving", 1);
        }
        else
        {
            anim.SetInteger("Moving", 0);
        }
    }
}
