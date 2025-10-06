using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
public class GoldUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI goldText;

    [Header("Increase coin")]
    public GameObject floatingTextPrefabs;
    public RectTransform popupRoot;

    private void Start()
    {
        if (popupRoot != null)
            popupRoot = popupRoot.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        if(GoldService.Instance == null)
        {
            Debug.LogWarning("GoldUI: GoldService instance is null. Cannot subscribe to events.");
            return;
        }
        Debug.Log("GoldUI: Subscribing to GoldService events.");
        GoldService.Instance.OnGoldChanged += HandleChanged;
        GoldService.Instance.OnGoldAdded += HandleAdded;
        HandleChanged(GoldService.Instance.TotalGold);
    }
    private void OnDisable()
    {
        if(GoldService.Instance != null)
        {
            GoldService.Instance.OnGoldChanged -= HandleChanged;
            GoldService.Instance.OnGoldAdded -= HandleAdded;

        }
        else
        {
            return;
        }
    }
    private void HandleChanged(int total)
    {
        if(goldText != null)
        {
            Debug.Log(total);
            goldText.text = total.ToString();
        }
        else
        {
            Debug.LogWarning("GoldUI: goldText is not assigned.");
        }
    }
    void HandleAdded(int delta)
    {
        if (delta <= 0 || floatingTextPrefabs == null || popupRoot == null) return;

        GameObject go = Instantiate(floatingTextPrefabs, popupRoot);
        RectTransform rt = go.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition = new Vector3(0, 0, 0);
        }
        var ft = go.GetComponent<FloatingText>();
        if (ft) ft.Show($"+{delta}");
    }
}
