using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string checkpointID;
    public string checkpointName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (!CheckpointManager.Instance.IsUnlocked(checkpointID))
        {
            CheckpointManager.Instance.UnlockCheckpoint(checkpointID, checkpointName, transform);
            RespawnController.Instance.respawnPoint = transform;
        }
        UIManager.Instance.OpenTeleportMenu();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.CloseTeleportMenu();
        }
    }

}
