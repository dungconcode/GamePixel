using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    private bool isPlayerInside = false; // Biến để kiểm tra xem đã spawn chưa
    private Coroutine removeEnemyCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!isPlayerInside) // Kiểm tra nếu chưa spawn
            {
                
                isPlayerInside = true; // Đánh dấu là đã spawn
                if (removeEnemyCoroutine != null)
                {
                    StopCoroutine(removeEnemyCoroutine); // Dừng coroutine nếu có
                    removeEnemyCoroutine = null;
                }
                EnemySpawn.Instance.StartSpawnEnemy(); // Gọi phương thức spawn enemy
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (isPlayerInside) // Kiểm tra nếu đã spawn
            {
                isPlayerInside = false; // Đánh dấu là chưa spawn
                removeEnemyCoroutine = StartCoroutine(RemoveEnemyAfterDelay(4f));
                Debug.Log("Player đã cút");
            }
        }
    }
    private IEnumerator RemoveEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(!isPlayerInside) // Kiểm tra nếu không còn trong vùng trigger
        {
            EnemySpawn.Instance.StopSpawnEnemy(); // Gọi phương thức dừng spawn enemy
        }
    }
}
