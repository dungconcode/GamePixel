using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Manager : MonoBehaviour
{
    [SerializeField] private Player_Index playerIndex;
    [SerializeField] private List<SkillSlot> skillSlots;
    private void Start()
    {
        for(int i = 0; i < skillSlots.Count; i++)
        {
            SkillSlot slot = skillSlots[i];
            slot.cooldownImage.fillAmount = 0f;
            slot.cooldownImage.enabled = false;
            slot.button.interactable = true;
            SkillBase skill = slot.skill;
            switch(i)
            {
                   case 0:
                    slot.cooldownTimer = playerIndex.timeSkill1;
                    break;
                case 1:
                    slot.cooldownTimer = playerIndex.timeSkill2;
                    break;
                case 2:
                    slot.cooldownTimer = playerIndex.timeSkill3;
                    break;
            }
            SkillSlot capturedSlot = slot; // tránh bug delegate
            slot.button.onClick.AddListener(() => ActivateSkill(capturedSlot));
        }
    }
    private void Update()
    {
        foreach (var slot in skillSlots)
        {
            if (slot.cooldownImage.fillAmount > 0f)
            {
                slot.cooldownImage.fillAmount -= Time.deltaTime / slot.cooldownTimer;
                if (slot.cooldownImage.fillAmount <= 0f)
                {
                    slot.cooldownImage.fillAmount = 0f;
                    slot.cooldownImage.enabled = false;
                    slot.button.interactable = true;
                }
            }
        }
    }

    private void ActivateSkill(SkillSlot slot)
    {
        slot.skill.Activate();
        slot.button.interactable = false;
        slot.cooldownImage.enabled = true;
        slot.cooldownImage.fillAmount = 1f;
    }
}
