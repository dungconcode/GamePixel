using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIItem : MonoBehaviour
{
    public Button skillButon;
    public GameObject detailPanel;

    public Image headerIcon;         // icon nhỏ trên header
    public Image detailIcon;         // icon to trong panel
    public Text titleText;           // nếu dùng Text (UGUI)
    public Text shortText;           // ví dụ cooldown/energy
    public Text detailText;

    private SkillData data;
    private RectTransform layoutroot;
    private bool isExpanded = false;
    private void Start()
    {
        detailPanel.SetActive(false);
    }
    public void ToggleDetailPanel()
    {
        Toggle();
    }
    public void Toggle()
    {
        if (isExpanded) Collapse(); else Expand();
    }
    public void Expand()
    {
        isExpanded = true;
        if(detailPanel) detailPanel.SetActive(true);
        if (skillButon) skillButon.gameObject.SetActive(false);
        RebuildLayout();
    }
    public void BInd(SkillData d)
    {
        data = d;
        if (data == null) return;
        if(headerIcon) headerIcon.sprite = data.icon;
        if (titleText) titleText.text = data.displayName;
        if (shortText) shortText.text = data.displayName;
        if (detailIcon) detailIcon.sprite = data.icon;
        if (detailText) detailText.text = data.decription;
        Collapse();
    }
    public void Collapse(bool rebuild = true)
    {
        isExpanded = false;
        if (detailPanel) detailPanel.SetActive(false);
        if(skillButon) skillButon.gameObject.SetActive(true);
        if(rebuild) RebuildLayout();
    }
    private void RebuildLayout()
    {
        if (layoutroot) return;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        //LayoutRebuilder.ForceRebuildLayoutImmediate(layoutroot);
        Canvas.ForceUpdateCanvases();
    }
}
