using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AirKnight : SkillBase
{
    [SerializeField] private Transform actionPoint;
    [SerializeField] private GameObject skillPrefab;
    public float speed;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private Animator skill2Animator;

    private void Start()
    {
       // skill2Animator = GameObject.Find("Player").GetComponent<Animator>();
    }
    public override void Activate()
    {
        skill2Animator.SetBool("isSkill2Active", true);
    }
    public void ActiveSkill(float timeToDelaySkill)
    {
        if(isShooting) return;
        StartCoroutine(WaitForNextSkill(timeToDelaySkill));
    }
    IEnumerator WaitForNextSkill(float timetoDelay)
    {
        Debug.Log("Air Knight Skill Activated");
        isShooting = true;
        GameObject airKnight = Instantiate(skillPrefab, actionPoint.position, actionPoint.rotation);
        Rigidbody2D rb = airKnight.GetComponent<Rigidbody2D>();
        rb.AddForce(actionPoint.right * speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(timetoDelay);
        isShooting = false;
    }
    private void OnDisable()
    {
        isShooting = false; // Reset shooting state when skill is disabled
    }
}
