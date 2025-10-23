using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class YSortGroup : MonoBehaviour
{
    public Transform sortPoint;
    private float baseOffer = 0f;
    private float multipier = 2f;

    private SortingGroup sg;

    private void Awake()
    {
        sg = GetComponent<SortingGroup>();
        if(sortPoint == null)
            sortPoint = transform;
    }
    private void LateUpdate()
    {
        int orderLayer = -(int)(sortPoint.position.y * multipier) + (int)baseOffer;
        sg.sortingOrder = Mathf.Clamp(orderLayer, 1, 9);
    }
}
