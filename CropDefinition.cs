using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Crop", menuName = "Farming/Crop Definition")]
public class CropDefinition : ScriptableObject
{
    public string cropName;
    public int daysToGrow = 3;
    public int baseSellValue = 10;
    public bool isIllegal = false;
    public int heatGeneration = 0;      // For illegal crops
    
    [Header("Visuals")]
    public TileBase seedTile;
    public TileBase growingTile;
    public TileBase matureTile;
    
    public TileBase GetTileForStage(int stage)
    {
        if (stage == 0) return seedTile;
        if (stage >= daysToGrow) return matureTile;
        return growingTile;
    }
}