using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIItem : MonoBehaviour
{
    public Button skillButon;
    public GameObject detailPanel;

    private bool isExpanded = false;
    private void Start()
    {
        detailPanel.SetActive(false);
    }
    public void ToggleDetailPanel()
    {
        isExpanded = !isExpanded;
        detailPanel.SetActive(isExpanded);
        skillButon.gameObject.SetActive(!isExpanded);
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            transform.parent.GetComponent<RectTransform>()
        );
    }
}
