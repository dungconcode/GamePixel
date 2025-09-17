using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpDetail : MonoBehaviour
{
    [SerializeField] private GameObject detailPanel;
    private Vector3 targetStart, targetEnd;
    private void Start()
    {
        targetStart = new Vector3(0, 676, 0);
        RectTransform rt = detailPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = targetStart;
        detailPanel.SetActive(false);
        targetEnd = new Vector3(0, 0, 0);
    }

    public void ShowDetailPannel()
    {
        StartCoroutine(ShowDetail());
    }
    public void HideDetailPannel()
    {
        if (detailPanel != null)
        {
            RectTransform rt = detailPanel.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0, 676, 0);
            detailPanel.SetActive(false);
        }
    }
    IEnumerator ShowDetail()
    {
        detailPanel.SetActive(true);
        RectTransform rt = detailPanel.GetComponent<RectTransform>();
        while (Vector2.Distance(rt.anchoredPosition, targetEnd) > 0.1f)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition,
                targetEnd,
                5000 * Time.deltaTime
            );
            yield return null;
        }
        rt.anchoredPosition = targetEnd;
    }
}
