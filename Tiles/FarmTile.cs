using UnityEngine;

[System.Serializable]
public class FarmTile
{
    public Vector3Int position;
    public CropDefinition crop;       // What crop is planted here?
    public int growthStage;           // How grown is it? (0 = seed, 3 = mature)
    public bool isIllegal;            // Does this generate heat?
    public int heatLevel;             // How much attention from authorities?
    public bool isWatered;            // Did player water this today?
    
    public bool HasCrop => crop != null;
    public bool IsMature => crop != null && growthStage >= crop.daysToGrow;
}