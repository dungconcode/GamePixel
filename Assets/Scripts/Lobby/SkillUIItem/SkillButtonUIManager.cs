using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonUIManager : MonoBehaviour
{
    public SkillUIItem[] skills;

    private void Start()
    {
        for(int i = 0; i < skills.Length; i++)
        {
            int index = i; // Capture the current index
            skills[i].skillButon.onClick.AddListener(() => OnSkillButtonClicked(index));
        }
    }
    private void OnSkillButtonClicked(int index)
    {
        for(int i = 0; i < skills.Length; i++)
        {
            if(i == index)
            {
                skills[i].ToggleDetailPanel();
            }
            else
            {
                if(skills[i].detailPanel.activeSelf)
                {
                    skills[i].ToggleDetailPanel();
                }
            }
        }
    }
}
