using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CloseOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject panelToClose;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            if (panelToClose != null)
            {
                RectTransform rt = panelToClose.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(0, 676, 0);
                panelToClose.SetActive(false);
            }        
        }
    }
}
