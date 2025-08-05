using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillSlot
{
    public Button button;
    public Image cooldownImage;
    public SkillBase skill;
    public float cooldownTimer;
}
