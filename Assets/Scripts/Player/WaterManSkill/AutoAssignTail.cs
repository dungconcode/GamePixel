using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAssignTail : MonoBehaviour
{
    public List<Transform> tailTransforms;

    private void Start()
    {
        // Lấy tất cả child làm tail
        for (int i = 0; i < transform.childCount; i++)
        {
            tailTransforms.Add(transform.GetChild(i));
        }

        // Gán followPos
        for (int i = 0; i < tailTransforms.Count; i++)
        {
            TailDragon tail = tailTransforms[i].GetComponent<TailDragon>();
            if (tail != null)
            {
                tail.followPos = (i == 0) ? transform : tailTransforms[i - 1];
            }
        }
        StartCoroutine(ShowTailsSequentially());
    }
    private IEnumerator ShowTailsSequentially()
    {
        for (int i = 0; i < tailTransforms.Count; i++)
        {
            Transform tail = tailTransforms[i];
            TailDragon tailScript = tail.GetComponent<TailDragon>();

            // Chờ đến khi followPos đã active
            while (tailScript.followPos != null && !tailScript.followPos.gameObject.activeInHierarchy)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f); // Thời gian chờ giữa các tail
            // Bật tail hiện tại
            tail.gameObject.SetActive(true);
        }
    }
}
