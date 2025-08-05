using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Moving : MonoBehaviour
{
    private AI_Path aiPath;
    private Transform player;
    private Animator anim;


    

    void Start()
    {
        aiPath = GetComponent<AI_Path>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimationEnemy();
    }
    void AnimationEnemy()
    {
        anim.SetBool("isMoving", aiPath.isMoving);
    }
}
