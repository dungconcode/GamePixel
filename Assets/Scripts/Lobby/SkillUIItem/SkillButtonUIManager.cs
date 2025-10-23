using System;
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
            Debug.Log("Adding listener to skill button " + i);
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
    public void SetSkills(SkillData[] dataArr)
    {
        for(int i = 0; i < skills.Length; i++)
        {
            var item = skills[i];
            if (!item) continue;

            bool hasData = (dataArr != null && i < dataArr.Length && dataArr[i] != null);
            item.gameObject.SetActive(hasData);
            if (!hasData) continue;
            item.BInd(dataArr[i]);
            item.skillButon.onClick.RemoveAllListeners();
            int index = i; // Capture the current index

            item.skillButon.onClick.AddListener(() => ExpandOnly(index));
            item.Collapse(false);
        }
        foreach (var it in skills)
            if (it && it.gameObject.activeSelf) it.Collapse(false);
    }
    private void ExpandOnly(int index)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (!skills[i] || !skills[i].gameObject.activeSelf) continue;
            if (i == index) skills[i].Expand();   // mở đúng item đang bấm
            else skills[i].Collapse(); // các item khác đóng
        }
    }
}
