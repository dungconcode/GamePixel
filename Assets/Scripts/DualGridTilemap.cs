using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TileType;

public class DualGridTilemap : MonoBehaviour {
    protected static readonly Vector3Int[] NEIGHBOURS = new Vector3Int[] {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0)
    };

    protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTile;

    public Tilemap tilemap;
    public Tile grassPlaceholderTile;
    public Tile dirtPlaceholderTile;

    public Tile[] tiles; 
    void Start() {
        neighbourTupleToTile = new() {
            {new (Grass, Grass, Grass, Grass), tiles[6]},
            {new (Dirt, Dirt, Dirt, Grass), tiles[13]},
            {new (Dirt, Dirt, Grass, Dirt), tiles[0]},
            {new (Dirt, Grass, Dirt, Dirt), tiles[8]},
            {new (Grass, Dirt, Dirt, Dirt), tiles[15]},
            {new (Dirt, Grass, Dirt, Grass), tiles[1]},
            {new (Grass, Dirt, Grass, Dirt), tiles[11]},
            {new (Dirt, Dirt, Grass, Grass), tiles[3]},
            {new (Grass, Grass, Dirt, Dirt), tiles[9]},
            {new (Dirt, Grass, Grass, Grass), tiles[5]},
            {new (Grass, Dirt, Grass, Grass), tiles[2]},
            {new (Grass, Grass, Dirt, Grass), tiles[10]},
            {new (Grass, Grass, Grass, Dirt), tiles[7]},
            {new (Dirt, Grass, Grass, Dirt), tiles[14]},
            {new (Grass, Dirt, Dirt, Grass), tiles[4]},
            {new (Dirt, Dirt, Dirt, Dirt), tiles[12]},
        };

        RefreshAllTiles();
    }

    public void SetCell(Vector3Int coords, Tile tile) {
        tilemap.SetTile(coords, tile);
        UpdateSurroundingTiles(coords);
    }

    private TileType GetTileType(Vector3Int coords) {
        Tile t = tilemap.GetTile(coords) as Tile;
        if (t == grassPlaceholderTile) return Grass;
        if (t == dirtPlaceholderTile) return Dirt;
        return Dirt; 
    }

    private Tile CalculateTile(Vector3Int coords) {
        TileType tl = GetTileType(coords);
        TileType tr = GetTileType(coords + new Vector3Int(1, 0, 0));
        TileType bl = GetTileType(coords + new Vector3Int(0, 1, 0));
        TileType br = GetTileType(coords + new Vector3Int(1, 1, 0));

        var key = new Tuple<TileType, TileType, TileType, TileType>(tl, tr, bl, br);
        if (neighbourTupleToTile.TryGetValue(key, out var tile))
            return tile;

        return tiles[12];
    }

    private void UpdateSurroundingTiles(Vector3Int pos) {
        
        for (int dx = -1; dx <= 1; dx++) {
            for (int dy = -1; dy <= 1; dy++) {
                Vector3Int c = pos + new Vector3Int(dx, dy, 0);
                tilemap.SetTile(c, CalculateTile(c));
            }
        }
    }

    public void RefreshAllTiles() {
        for (int x = -50; x < 50; x++) {
            for (int y = -50; y < 50; y++) {
                Vector3Int pos = new Vector3Int(x, y, 0);
                tilemap.SetTile(pos, CalculateTile(pos));
            }
        }
    }
}

public enum TileType {
    None,
    Grass,
    Dirt
}
