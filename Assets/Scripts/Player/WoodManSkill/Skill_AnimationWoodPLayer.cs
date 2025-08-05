using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AnimationWoodPLayer : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject skill3Active;
    private void Start()
    {
        anim = GetComponent<Animator>();
        skill3Active = GameObject.Find("Skill3");

    }
    public void Skill3Active()
    {
        skill3Active.GetComponent<Skill3_SeedBullet>().ActiveSkill(2f);
    }
    public void FinishSkill3()
    {
        anim.SetBool("isActiveSkill3", false);
    }
}
