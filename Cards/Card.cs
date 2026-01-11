using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CardPackage
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        [TextArea(3, 10)]
        public string cardDescription;
        public Sprite icon;
        public TileBase tileToPlace;
        public CardType cardType;
        public bool canPlaceOnEmpty = false;
        public List<TileType> validPlacementTiles = new List<TileType>();
        public CropDefinition cropToPlant;

        [Header("Cost")]
        public int apCost = 1;

        public enum CardType
        {
            Structure,
            Action,
            Weather,
            Animal,
            Land,
            Seed
        }
    }
}