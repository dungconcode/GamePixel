using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Moving : MonoBehaviour,IEnemyTick
{
    private AI_Path aiPath;
    private Transform player;
    private Animator anim;
    [SerializeField] private Animator weaponAnim;
    void Start()
    {
        aiPath = GetComponent<AI_Path>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }
    public void OnEnable()
    {
        EnemyManager.Instance.Register(this);
    }
    public void OnDisable()
    {
        EnemyManager.Instance.Unregister(this);
    }
    public void Ontick()
    {
        anim.SetBool("isMoving", aiPath.isMoving);
        weaponAnim.SetBool("isMoving", aiPath.isMoving);
    }
}
