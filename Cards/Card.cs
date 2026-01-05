using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public CardType  cardType;
        public bool canPlaceOnEmpty = false;

        [System.Serializable]
        public struct CostEntry
        {
            public CostType type;
            public int amount;
        }

        public List<CostEntry> costs = new List<CostEntry>();

        public enum CostType
        {
            Water,
            Earth,
            Light,
            Dark,
            Air,
            Time
        }

        public enum CardType
        {
            Structure,
            Action,
            Weather,
            Animal,
            Land,
            Seed
        }

        public int GetCost(CostType t)
        {
            for (int i = 0; i < costs.Count; i++)
            {
                if (costs[i].type == t) return costs[i].amount;
            }
            return 0;
        }
    }
}