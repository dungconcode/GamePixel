using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
public static class SaveKeys
{
    public const string TOTAL_GOLD = "TOTAL_GOLD";
}
public class GoldService : MonoBehaviour
{
    public static GoldService Instance { get; private set; }
    public int TotalGold { get; private set; }
    public event Action<int> OnGoldChanged;
    public event Action<int> OnGoldAdded;

    [Header("Auto-save")]
    [SerializeField] private int saveEveryNChanges = 5;
    int _dirty;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
        OnGoldChanged?.Invoke(TotalGold);
    }
    public void AddGold(int amount)
    {
        if (amount <= 0) return;
        TotalGold += amount;
        OnGoldAdded?.Invoke(amount);
        OnGoldChanged?.Invoke(TotalGold);
        MarkDirty();
    }
    public bool TrySpend(int price)
    {
        if (TotalGold < price) return false;
        TotalGold -= price;
        OnGoldChanged?.Invoke(TotalGold);
        MarkDirty();
        return true;
    }
    private void Load()
    {
        TotalGold = PlayerPrefs.GetInt(SaveKeys.TOTAL_GOLD, 0);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            TotalGold = 0;
            PlayerPrefs.SetInt(SaveKeys.TOTAL_GOLD, 0);
            PlayerPrefs.Save();
            OnGoldChanged?.Invoke(TotalGold);
        }
    }
    void MarkDirty(bool force = false)
    {
        _dirty++;
        PlayerPrefs.SetInt(SaveKeys.TOTAL_GOLD, TotalGold);
        if(force || _dirty >= saveEveryNChanges)
        {
            PlayerPrefs.Save();
            _dirty = 0;
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) PlayerPrefs.Save();
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
