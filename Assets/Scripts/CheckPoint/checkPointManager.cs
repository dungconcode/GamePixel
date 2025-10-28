using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public List<CheckpointData> unlockedCheckpoints = new List<CheckpointData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IsUnlocked(string id)
    {
        return unlockedCheckpoints.Exists(cp => cp.id == id);
    }

    public void UnlockCheckpoint(string id, string name, Transform location)
    {
        if (!IsUnlocked(id))
        {
            unlockedCheckpoints.Add(new CheckpointData(id, name, location));
            Debug.Log("Checkpoint đã được mở: " + name);
        }
    }
}
