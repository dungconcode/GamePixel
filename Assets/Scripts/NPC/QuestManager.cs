using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public Quest currentQuest;
    public TMP_Text questUIText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateQuestUI();
    }

    public void SetCurrentQuest(Quest q)
    {
        currentQuest = q;
        if (currentQuest != null) currentQuest.ResetQuest();
        UpdateQuestUI();
    }

    public void EnemyKilled()
    {
        if (currentQuest == null || currentQuest.isCompleted) return;
        currentQuest.AddKill();
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        if (questUIText == null)
        {
            Debug.LogWarning("Quest UI Text not assigned in QuestManager.");
            return;
        }

        if (currentQuest == null)
        {
            questUIText.text = "No active quest";
            return;
        }

        if (currentQuest.isCompleted)
            questUIText.text = $"{currentQuest.questName} ✅\n{currentQuest.description}";
        else
            questUIText.text = $"{currentQuest.questName}\nKills: {currentQuest.currentKills}/{currentQuest.requiredKills}\n{currentQuest.description}";
    }
}
