using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace CardPackage
{
    [CreeateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public Sprite icon;
        public TileBase tileToPlace;
        public int cost;
        public CostType costType;
        public CardType  cardType;
        public bool canPlaceOnEmpty = false;

        public enum CostType
        {
            Water,
            Earth,
            Light,
            Dark,
            Air
        }

        public enum CardType
        {
            Structure,
            Weather,
            Animal,
            Land,
            Seed
        }
    }
}