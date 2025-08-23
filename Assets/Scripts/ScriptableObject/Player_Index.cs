using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIndex", menuName = "ScriptableObjects/PlayerIndex", order = 1)]
public class Player_Index : ScriptableObject
{
    public string characterID;
    public float hp;
    public float armor;
    public float mana;
    public float damage;
    public float speed;
    public float timeSkill1;
    public float timeSkill2;
    public float timeSkill3;
}
