using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

// Enum at top level so other scripts can access it
public enum TileType
{
    Grass,
    Dirt,
    Water,
    Sand,
    Tilled,
    Stone
}

[CreateAssetMenu(fileName = "New TileData", menuName = "Tile Data")]
public class TileData : ScriptableObject
{
    public List<TileBase> tiles;
    public TileType tileType;
}