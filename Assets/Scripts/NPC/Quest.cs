using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea] public string description;
    public int requiredKills = 5;

    [HideInInspector] public int currentKills = 0;
    [HideInInspector] public bool isCompleted = false;

    public void ResetQuest()
    {
        currentKills = 0;
        isCompleted = false;
    }

    public void AddKill()
    {
        if (isCompleted) return;
        currentKills++;
        if (currentKills >= requiredKills)
        {
            isCompleted = true;
            currentKills = requiredKills;
            Debug.Log($"Quest completed: {questName}");
        }
    }
}
