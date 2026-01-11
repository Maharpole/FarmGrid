using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    // Singleton so any script can access: GridManager.Instance.GetTileAt(pos)
    public static GridManager Instance { get; private set; }
    
    // Reference to the tilemap where crop sprites are displayed
    public Tilemap cropTilemap;
    
    // THE KEY DATA STRUCTURE: Maps grid positions to their game state
    // This is where all the "real" farm data lives
    private Dictionary<Vector3Int, FarmTile> farmTiles = new Dictionary<Vector3Int, FarmTile>();
    
    void Awake()
    {
        Instance = this;
    }
    
    // Get the FarmTile at any position
    // If none exists yet, create an empty one (lazy initialization)
    public FarmTile GetTileAt(Vector3Int pos)
    {
        if (!farmTiles.ContainsKey(pos))
        {
            farmTiles[pos] = new FarmTile { position = pos };
        }
        return farmTiles[pos];
    }
    
    // Plant a crop at a position
    public void SetCrop(Vector3Int pos, CropDefinition crop)
    {
        FarmTile tile = GetTileAt(pos);
        tile.crop = crop;
        tile.growthStage = 0;           // Start at seed stage
        tile.isIllegal = crop.isIllegal; // Inherit from crop type
        
        UpdateCropVisual(pos);          // Sync the tilemap
        Debug.Log($"Planted {crop.cropName} at {pos}");
    }
    
    // Called once per day during Resolution phase
    // Advances growth for ALL crops on the grid
    public void GrowCrops()
    {
        foreach (var kvp in farmTiles)
        {
            FarmTile tile = kvp.Value;
            
            // Only grow if there's a crop and it's not already mature
            if (tile.HasCrop && !tile.IsMature)
            {
                tile.growthStage++;
                UpdateCropVisual(kvp.Key);
                
                if (tile.IsMature)
                    Debug.Log($"{tile.crop.cropName} at {kvp.Key} is ready to harvest!");
            }
        }
    }
    
    // Harvest a mature crop, returns what was harvested (for inventory)
    public CropDefinition HarvestAt(Vector3Int pos)
    {
        FarmTile tile = GetTileAt(pos);
        
        if (!tile.IsMature)
        {
            Debug.Log("Crop not ready to harvest!");
            return null;
        }
        
        CropDefinition harvested = tile.crop;
        
        // Clear the tile
        tile.crop = null;
        tile.growthStage = 0;
        tile.isIllegal = false;
        
        UpdateCropVisual(pos);  // Remove the crop sprite
        Debug.Log($"Harvested {harvested.cropName} from {pos}");
        
        return harvested;  // Caller adds this to inventory
    }
    
    // Keep the tilemap in sync with the data
    private void UpdateCropVisual(Vector3Int pos)
    {
        FarmTile tile = GetTileAt(pos);
        
        if (tile.crop == null)
        {
            // No crop = no sprite
            cropTilemap.SetTile(pos, null);
        }
        else
        {
            // Get the appropriate sprite for current growth stage
            TileBase visual = tile.crop.GetTileForStage(tile.growthStage);
            cropTilemap.SetTile(pos, visual);
        }
    }
}