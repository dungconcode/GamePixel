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
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        aiPath = GetComponent<AI_Path>();
        if (aiPath == null)
            Debug.LogError($"{name} is missing AI_Path component!");

        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError($"{name} is missing Animator component!");

        if (weaponAnim == null)
        {
            weaponAnim = GetComponentInChildren<Animator>();
            if (weaponAnim == null)
                Debug.LogWarning($"{name} has no child Animator assigned as weaponAnim!");
        }
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
        if (aiPath == null) return;
        if (anim != null) 
        {
            anim.SetBool("isMoving", aiPath.isMoving);
        }

        
        if (weaponAnim != null)
        {
            weaponAnim.SetBool("isMoving", aiPath.isMoving);
        }
        
    }
}
