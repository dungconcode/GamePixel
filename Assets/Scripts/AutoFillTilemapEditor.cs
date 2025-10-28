using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[InitializeOnLoad]
public static class AutoFillTilemapEditor
{
    
    static Dictionary<Tilemap, TileBase[]> lastSnapshot = new();
    static Dictionary<Tilemap, BoundsInt> lastBounds = new();
    static double lastTime;

    static AutoFillTilemapEditor()
    {
        EditorApplication.update += Update;
    }

    static void Update()
    {
        
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;

        
        if (EditorApplication.timeSinceStartup - lastTime < 0.5) return;
        lastTime = EditorApplication.timeSinceStartup;

        
        AutoFillMarker[] markers = Object.FindObjectsOfType<AutoFillMarker>(true);
        foreach (var marker in markers)
        {
            if (marker == null || marker.targetTilemap == null) continue;

            Tilemap map = marker.targetTilemap;
            BoundsInt bounds = map.cellBounds;

            
            if (bounds.size.x <= 0 || bounds.size.y <= 0) continue;
            if (bounds.size.x > 2048 || bounds.size.y > 2048) continue;

            TileBase[] currentTiles = map.GetTilesBlock(bounds);

            bool changed = false;
            if (!lastSnapshot.TryGetValue(map, out TileBase[] prevTiles))
                changed = true;
            else if (!BoundsEqual(lastBounds[map], bounds))
                changed = true;
            else if (!AreTilesEqual(prevTiles, currentTiles))
                changed = true;

            if (changed)
            {
                DoAutoFill(map, marker);
                lastSnapshot[map] = currentTiles;
                lastBounds[map] = bounds;
            }
        }
    }

    static bool BoundsEqual(BoundsInt a, BoundsInt b)
    {
        return a.min == b.min && a.size == b.size;
    }

    static bool AreTilesEqual(TileBase[] a, TileBase[] b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;
        return true;
    }

    static void DoAutoFill(Tilemap map, AutoFillMarker marker)
    {
        if (map == null) return;

        BoundsInt bounds = map.cellBounds;
        int w = bounds.size.x;
        int h = bounds.size.y;
        if (w <= 0 || h <= 0) return;

        TileBase[] tiles = map.GetTilesBlock(bounds);

        
        HashSet<TileBase> present = new();
        for (int i = 0; i < tiles.Length; i++) if (tiles[i] != null) present.Add(tiles[i]);

        List<TileBase> toProcess = new();
        if (marker.fillAllTileTypes)
        {
            toProcess.AddRange(present);
        }
        else
        {
            if (marker.specificTileTypes != null)
            {
                foreach (var t in marker.specificTileTypes)
                    if (t != null && present.Contains(t)) toProcess.Add(t);
            }
        }

        if (toProcess.Count == 0) return;

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        foreach (var tileType in toProcess)
        {
           
            bool[,] isWall = new bool[w, h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    isWall[x, y] = (tiles[x + y * w] == tileType);

            
            bool[,] reachable = new bool[w, h];
            Queue<Vector2Int> q = new();
           
            for (int x = 0; x < w; x++)
            {
                if (!isWall[x, 0]) { reachable[x, 0] = true; q.Enqueue(new Vector2Int(x, 0)); }
                if (!isWall[x, h - 1]) { reachable[x, h - 1] = true; q.Enqueue(new Vector2Int(x, h - 1)); }
            }
            for (int y = 0; y < h; y++)
            {
                if (!isWall[0, y]) { reachable[0, y] = true; q.Enqueue(new Vector2Int(0, y)); }
                if (!isWall[w - 1, y]) { reachable[w - 1, y] = true; q.Enqueue(new Vector2Int(w - 1, y)); }
            }

            while (q.Count > 0)
            {
                var p = q.Dequeue();
                for (int k = 0; k < 4; k++)
                {
                    int nx = p.x + dx[k];
                    int ny = p.y + dy[k];
                    if (nx < 0 || ny < 0 || nx >= w || ny >= h) continue;
                    if (isWall[nx, ny]) continue;
                    if (reachable[nx, ny]) continue;
                    reachable[nx, ny] = true;
                    q.Enqueue(new Vector2Int(nx, ny));
                }
            }

            
            List<Vector3Int> fillPositions = new();
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (!isWall[x, y] && !reachable[x, y])
                    {
                        fillPositions.Add(bounds.min + new Vector3Int(x, y, 0));
                    }
                }

            if (fillPositions.Count > 0)
            {
                Undo.RecordObject(map, "AutoFill Tilemap");
                foreach (var pos in fillPositions) map.SetTile(pos, tileType);
            }
        }

        map.RefreshAllTiles();
        Debug.Log($"AutoFill: completed on '{map.name}' — processed {toProcess.Count} tile types.");
    }

    
    [MenuItem("Tools/AutoFill/Run AutoFill On Selected Tilemap")]
    public static void RunOnSelected()
    {
        var go = Selection.activeGameObject;
        if (go == null) { Debug.LogWarning("Select a GameObject with a Tilemap."); return; }
        var map = go.GetComponent<Tilemap>();
        if (map == null) { Debug.LogWarning("Selected GameObject has no Tilemap."); return; }

        AutoFillMarker marker = go.GetComponent<AutoFillMarker>();
        if (marker == null)
        {
            
            marker = go.AddComponent<AutoFillMarker>();
            marker.targetTilemap = map;
            marker.fillAllTileTypes = true;
            DoAutoFill(map, marker);
            Object.DestroyImmediate(marker);
        }
        else DoAutoFill(map, marker);
    }
}
