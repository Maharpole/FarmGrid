using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image icon;
    public Button button;
    private PlacementCard card;
    
    public void Init(PlacementCard newCard)
    {
        card = newCard;
        icon.sprite = card.icon;

        button.onClick.AddListener(() =>
        {
            PlacementManager.Instance.SelectCard(card);
        });
    }
}
