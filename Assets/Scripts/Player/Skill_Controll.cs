using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
public class Skill_Controll : MonoBehaviour
{
    [SerializeField] private Player_Index playerIndex;

    [Header("Attack")]
    private Player_Attack playerAttack;

    [Header("Skill 1")]
    [SerializeField] private GameObject skill1Prefab;
    [SerializeField] private Button button_Skill1;
    [SerializeField] private Image cancleSkill1;
    private float timeCoolDownSkill1;

    [Header("Skill 2")]
    [SerializeField] private GameObject skill2Prefab;
    [SerializeField] private Button button_Skill2;
    [SerializeField] private Image cancleSkill2;
    [SerializeField] private Animator skill2Animator;
    private float timeCoolDownSkill2;

    [Header("Skill 3")]
    [SerializeField] private GameObject skill3Prefab;
    [SerializeField] private Button button_Skill3;
    [SerializeField] private Image cancleSkill3;
    private float timeCoolDownSkill3;

    private void Start()
    {
        //--Attack--
        playerAttack = GetComponent<Player_Attack>();
        playerAttack = Player_Attack.instance;
        //--Skill 1--
        skill1Prefab = GameObject.Find("Skill1");
        button_Skill1 = GameObject.Find("Skill1_Button").GetComponent<Button>();
        cancleSkill1 = GameObject.Find("Skill1_Cancle").GetComponent<Image>();
        cancleSkill1.enabled = false;
        skill1Prefab.SetActive(false);
        button_Skill1.interactable = true;
        timeCoolDownSkill1 = playerIndex.timeSkill1;

        //--Skill 2--
        skill2Prefab = GameObject.Find("Skill2");
        button_Skill2 = GameObject.Find("Skill2_Button").GetComponent<Button>();
        cancleSkill2 = GameObject.Find("Skill2_Cancle").GetComponent<Image>();
        skill2Animator = GameObject.Find("Player").GetComponent<Animator>();
        cancleSkill2.enabled = false;
        skill2Prefab.SetActive(false);
        button_Skill2.interactable = true;
        timeCoolDownSkill2 = playerIndex.timeSkill2;

        //--Skill 3--
        skill3Prefab = GameObject.Find("Skill3");
        button_Skill3 = GameObject.Find("Skill3_Button").GetComponent<Button>();
        cancleSkill3 = GameObject.Find("Skill3_Cancle").GetComponent<Image>();
        cancleSkill3.enabled = false;
        skill3Prefab.SetActive(false);
        button_Skill3.interactable = true;
        timeCoolDownSkill3 = playerIndex.timeSkill3;
    }
    public void PlayerAttack()
    {
        playerAttack.PlayerAttack();
    }
    public void ActiveSkill1()
    {
        StartCoroutine(TimeActiveSkill(skill1Prefab,button_Skill1,cancleSkill1, timeCoolDownSkill1));
        skill1Prefab.GetComponent<RotateSkill>().ActiveSkill(6f,2f); // Time to Spin, Time to Back
    }
    public void ActiveSkill2()
    {
        StartCoroutine(TimeActiveSkill(skill2Prefab, button_Skill2 ,cancleSkill2, timeCoolDownSkill2));
        skill2Prefab.GetComponent<Skill_AirKnight>().ActiveSkill(2f); // time to delay skill
    }
    public void ActiveAniSKill2()
    {
        skill2Animator.SetBool("isSkill2Active", true);
    }
    public void ActiveSkill3()
    {
        StartCoroutine(TimeActiveSkill(skill3Prefab, button_Skill3, cancleSkill3, timeCoolDownSkill3));
        skill3Prefab.GetComponent<Skill_3>().ActivateSkill(3f); // time to delay skill
    }
    IEnumerator TimeActiveSkill(GameObject skill,Button button,Image cancleSkill, float timeActive)
    {
        if(skill != null)
        {
            skill.SetActive(true);
            button.interactable = false;
            cancleSkill.enabled = true;
            float time = timeActive * 2;
            while(time > 0f && cancleSkill.fillAmount > 0f)
            {
                time -= Time.deltaTime;
                cancleSkill.fillAmount -= Time.deltaTime / time;
                yield return null;
            }
            //yield return new WaitForSeconds(timeActive);
            if(cancleSkill.fillAmount <= 0f)
            {
                cancleSkill.fillAmount = 1f;
                button.interactable = true;
                cancleSkill.enabled = false;
                skill.SetActive(false);
            }
            
        }
    }
    
}
