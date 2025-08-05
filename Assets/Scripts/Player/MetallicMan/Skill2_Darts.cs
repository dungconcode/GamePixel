using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Darts : SkillBase
{
    [SerializeField] private Transform actionPoint;
    [SerializeField] private GameObject skillPrefab;
    public float speed;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private Animator skill2Animator;

    private void Start()
    {
        skill2Animator = GameObject.Find("Player").GetComponent<Animator>();
    }
    public override void Activate()
    {
        ActiveSkill(2f); // Call ActiveSkill with a delay of 0.5 seconds
    }
    public void ActiveSkill(float timeToDelaySkill)
    {
        if (isShooting) return; // Prevent multiple shots at the same time
        StartCoroutine(WaitForNextSkill(timeToDelaySkill));
    }
    IEnumerator WaitForNextSkill(float timetoDelay)
    {
        isShooting = true;


        for(int i = 1; i <= 5; i++)
        {
            GameObject darts = Instantiate(skillPrefab, actionPoint.position, actionPoint.rotation);
            Rigidbody2D rb = darts.GetComponent<Rigidbody2D>();
            rb.AddForce(actionPoint.right * speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(timetoDelay);
        isShooting = false;
    }
    private void OnDisable()
    {
        isShooting = false; // Reset shooting state when skill is disabled
    }
}
