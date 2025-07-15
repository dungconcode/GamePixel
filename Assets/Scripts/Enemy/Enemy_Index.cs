using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Index", menuName = "ScriptableObjects/Enemy/Enemy_Index", order = 1)]
public class Enemy_Index : ScriptableObject
{
    public float hp;
    public float speed;
    public float damage;
}
