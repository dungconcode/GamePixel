using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUpButtonUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UpdateManager updateManager;
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Text costText;

    private void OnEnable()
    {
        if(GoldService.Instance != null)
        {
            GoldService.Instance.OnGoldChanged += OnGoldCHanged;
        }
        CharacterEvent.OnStatsUpdated += OnStatsUpdated;
    }
    private void OnDisable()
    {
        if(GoldService.Instance != null)
        {
            GoldService.Instance.OnGoldChanged -= OnGoldCHanged;
        }
        CharacterEvent.OnStatsUpdated -= OnStatsUpdated;
    }
    private void Start()
    {
        if(levelUpButton != null)
        {
            levelUpButton.onClick.AddListener(OnClickLevelUp);
        }
        RefreshAll();
    }
    void OnGoldCHanged(int totalGold)
    {
        RefreshInteractable(totalGold);
    }
    void OnStatsUpdated(CharacterData data)
    {
        RefreshAll();
    }
    void OnClickLevelUp()
    {
        if (updateManager == null) return;
        updateManager.LevelUP();
    }
    void RefreshAll()
    {
        if(updateManager == null || costText == null || levelUpButton == null) return;
        if (updateManager.IsMaxLevel())
        {
            levelUpButton.gameObject.SetActive(false);
            return;
        }
        int cost = updateManager.GetNextLevelCost();
        costText.text = cost.ToString();
    }
    void RefreshInteractable(int curretnGold)
    {
        if(updateManager == null || costText == null || levelUpButton == null) return;
        if (updateManager.IsMaxLevel())
        {
            levelUpButton.gameObject.SetActive(false);
            return;
        }
        int cost = updateManager.GetNextLevelCost();
        levelUpButton.interactable = curretnGold >= cost;
    }
}
