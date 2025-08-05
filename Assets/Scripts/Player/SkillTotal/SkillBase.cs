using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    //public float cooldown = 3f;
    public Sprite icon;
    public abstract void Activate();
}
