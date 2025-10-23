using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIndex", menuName = "RPG/Skill")]
public class SkillData : ScriptableObject
{
    public Sprite icon;
    public string displayName;
    [TextArea] public string decription;

}
