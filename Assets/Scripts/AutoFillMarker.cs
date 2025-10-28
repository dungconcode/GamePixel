using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class AutoFillMarker : MonoBehaviour
{
    public Tilemap targetTilemap;
    public bool fillAllTileTypes = true;
    public TileBase[] specificTileTypes;

    void OnValidate()
    {
        if (targetTilemap == null)
            targetTilemap = GetComponent<Tilemap>();
    }
}
