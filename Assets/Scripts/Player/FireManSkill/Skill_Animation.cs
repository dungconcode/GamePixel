using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Animation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject skill2Active;
    private void Start()
    {
        anim = GetComponent<Animator>();
        skill2Active = GameObject.Find("Skill2");
        
    }
    public void Skill2Active()
    {
        skill2Active.GetComponent<Skill_AirKnight>().ActiveSkill(2f);
    }
    public void FinishSkill2()
    {
        anim.SetBool("isSkill2Active", false);
    }

}
