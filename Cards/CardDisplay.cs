using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardPackage;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    private Color[] typeColors = {
        Color.red, //Structure
        Color.magenta, //Action
        new Color32(0xB7, 0x8F, 0x4F, 0xFF), //Weather
        Color.green, //Animal
        Color.blue, //Land
        Color.cyan //Seed
    };

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        if (cardData == null)
        {
            Debug.LogWarning("CardDisplay: cardData is not assigned.", this);
            return;
        }

        int idx = (int)cardData.cardType;

        if (cardImage != null)
            cardImage.color = (idx >= 0 && idx < typeColors.Length) ? typeColors[idx] : Color.white;
        else
            Debug.LogWarning("CardDisplay: cardImage is not assigned.", this);

        if (nameText != null)
            nameText.text = cardData.cardName;
        else
            Debug.LogWarning("CardDisplay: nameText is not assigned.", this);

        if (descriptionText != null)
            descriptionText.text = cardData.cardDescription;
        else
            Debug.LogWarning("CardDisplay: descriptionText is not assigned.", this);
    }


}
