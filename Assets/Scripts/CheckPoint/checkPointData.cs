using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public string id;
    public string name;
    public Transform location;

    public CheckpointData(string id, string name, Transform location)
    {
        this.id = id;
        this.name = name;
        this.location = location;
    }
}
