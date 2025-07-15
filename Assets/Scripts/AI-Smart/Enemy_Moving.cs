using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moving : MonoBehaviour
{
    private AI_Path aiPath;
    private Transform player;
    private Animator anim;

    private int facingDirection = 1; // 1: phải, -1: trái

    void Start()
    {
        aiPath = AI_Path.Instance;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimationEnemy();
    }
    void AnimationEnemy()
    {
        if (aiPath.isMoving)
        {
            anim.SetBool("isMoving", true);
            Flip();
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    void Flip()
    {
        if(player.position.x < transform.position.x && facingDirection > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingDirection = -1; // Đặt hướng đối diện
        }
        else if(player.position.x > transform.position.x && facingDirection < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingDirection = 1; // Đặt hướng đối diện
        }
    }
}
