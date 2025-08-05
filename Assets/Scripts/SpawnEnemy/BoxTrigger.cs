using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    private bool isPlayerInside = false; // Biến để kiểm tra xem đã spawn chưa
    private Coroutine removeEnemyCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isPlayerInside)
            {
                isPlayerInside = true;

                // Hủy coroutine xóa nếu player quay lại
                EnemySpawn.Instance.CancelRemoveEnemyCoroutine();

                // Chỉ spawn nếu không còn enemy nào tồn tại
                if (EnemySpawn.Instance.spawnedEnemies.Count == 0)
                {
                    EnemySpawn.Instance.StartSpawnEnemy();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPlayerInside)
        {
            isPlayerInside = false;
            EnemySpawn.Instance.StartRemoveCoroutineFromOutside(20f);
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
