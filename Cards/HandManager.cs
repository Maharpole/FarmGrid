using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardPackage;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign card prefab in inspector
    public Transform handTransform; //root of the hand position
    // public float fanSpread = 5f; //Rotation of cards in hand
    // public float cardSpacing = 10f; //Spread of cards in hand
    public List<GameObject> cardsInHand = new List<GameObject>(); //Hold a list of card objects in hand
    [Header("Fan Arc Settings")]
    public float fanRadius = 400f;
    public float maxFanAngle = 30f;

    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);

        // Wire up the CardDisplay with the card data
        CardDisplay display = newCard.GetComponent<CardDisplay>();
        if (display != null)
        {
            display.cardData = cardData;
            display.UpdateCardDisplay();
        }

        cardsInHand.Add(newCard);
        UpdateHandVisuals();
    }

    void Update()
    {
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 0) return;

        for (int i = 0; i < cardCount; i++)
        {
            // Normalized position: -0.5 (left) to +0.5 (right)
            float t = (cardCount == 1) ? 0f : (float)i / (cardCount - 1) - 0.5f;

            // Angle from center (in degrees, then radians)
            float angleDeg = t * maxFanAngle;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            // Position along the arc (pivot is below the hand at -fanRadius)
            float x = Mathf.Sin(angleRad) * fanRadius;
            float y = Mathf.Cos(angleRad) * fanRadius - fanRadius; // Offset so middle card is at y=0

            cardsInHand[i].transform.localPosition = new Vector3(x, y, 0f);

            // Rotate card to face outward from pivot
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, -angleDeg);
        }
    }
}
