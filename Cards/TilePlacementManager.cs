using UnityEngine;
using UnityEngine.Tilemaps;
using CardPackage;
using System.Collections.Generic;

public class TilePlacementManager : MonoBehaviour
{
    public static TilePlacementManager Instance { get; private set; }

    [Header("Tilemaps")]
    public Tilemap groundTilemap;
    public Tilemap buildingTilemap;
    public Camera mainCamera;

    [Header("Tile Data")]
    public List<TileData> tileDataList = new List<TileData>();
    private Dictionary<TileBase, TileType> tileTypeLookup;

    void Awake()
    {
        Instance = this;
        if (mainCamera == null)
            mainCamera = Camera.main;
        
        // Build lookup dictionary
        tileTypeLookup = new Dictionary<TileBase, TileType>();
        foreach (var data in tileDataList)
        {
            // Loop through ALL tiles in this TileData
            foreach (var tile in data.tiles)
            {
                if (tile != null)
                    tileTypeLookup[tile] = data.tileType;
            }
        }
    }

    public TileType? GetTileTypeAt(Vector3Int cellPos)
    {
        TileBase tile = groundTilemap.GetTile(cellPos);
        if (tile != null && tileTypeLookup.TryGetValue(tile, out TileType type))
            return type;
        return null;
    }

    public bool TryPlaceCard(Card card, Vector2 screenPosition)
    {
        if (!ActionPointManager.Instance.CanPlayCard(card.apCost))
        {
            Debug.Log($"Not enough AP to play {card.cardName}!");
            return false;
        }


        if (card == null || card.tileToPlace == null)
        {
            Debug.LogWarning("Card or tile is null.");
            return false;
        }

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0;

        Tilemap targetMap = GetTargetTilemap(card.cardType);
        if (targetMap == null)
            return false;

        Vector3Int cellPos = targetMap.WorldToCell(worldPos);

        if (!CanPlaceAt(card, cellPos))
            return false;

        targetMap.SetTile(cellPos, card.tileToPlace);
        Debug.Log($"Placed {card.cardName} at {cellPos}");

        // Plant crop data AFTER successful placement
        if (card.cardType == Card.CardType.Seed && card.cropToPlant != null)
        {
            GridManager.Instance.SetCrop(cellPos, card.cropToPlant);
        }

        // After successful placement:
        ActionPointManager.Instance.TrySpendAP(card.apCost);
        
        return true;
    }

    private Tilemap GetTargetTilemap(Card.CardType cardType)
    {
        switch (cardType)
        {
            case Card.CardType.Structure:
                return buildingTilemap;
            default:
                return groundTilemap;
        }
    }

    private bool CanPlaceAt(Card card, Vector3Int cellPos)
    {
        // Check tile type restrictions
        if (card.validPlacementTiles.Count > 0)
        {
            TileType? currentType = GetTileTypeAt(cellPos);
            
            if (!currentType.HasValue || !card.validPlacementTiles.Contains(currentType.Value))
            {
                Debug.Log($"Cannot place {card.cardName} here - requires: {string.Join(", ", card.validPlacementTiles)}");
                return false;
            }
        }
        
        // Prevent planting seeds where a crop already exists
        if (card.cardType == Card.CardType.Seed)
        {
            if (GridManager.Instance != null)
            {
                FarmTile existingTile = GridManager.Instance.GetTileAt(cellPos);
                if (existingTile.HasCrop)
                {
                    Debug.Log($"Cannot plant {card.cardName} here - a crop is already growing!");
                    return false;
                }
            }
        }
        
        if (card.cardType == Card.CardType.Structure)
        {
            if (buildingTilemap.HasTile(cellPos))
                return false;
            if (!card.canPlaceOnEmpty && !groundTilemap.HasTile(cellPos))
                return false;
        }

        return true;
    }
}