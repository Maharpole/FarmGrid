using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardPackage;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [Header("Deck Setup")]
    public List<Card> starterDeck = new List<Card>(); // Assign card assets in Inspect
    public int startingHandNum = 5;
    [Header("References")]
    public HandManager handManager;
    public Button endTurnButton;

    private List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();

    private int turnNumber = 0;

    void Awake()
    {
        Instance = this;

        if (InGameUIManager.Instance != null)
        {
            InGameUIManager.Instance.UpdateTurnDisplay(turnNumber);
        }
    }

    void Start()
    {
        InitializeDeck();
        DrawStartingHand(startingHandNum); //Draw starting hand at game start

        if(endTurnButton != null)
            endTurnButton.onClick.AddListener(EndTurn);
    }


void InitializeDeck()
{
    drawPile.Clear();
    drawPile.AddRange(starterDeck);
    ShuffleDeck();
}

public void ShuffleDeck()
{
    for (int i = drawPile.Count - 1; i > 0; i--)
    {
        int j = Random.Range(0, i + 1);
        (drawPile[i], drawPile[j]) = (drawPile[j], drawPile[i]);
    }
}

void DrawStartingHand(int count)
{
    for (int i = 0; i < count; i++)
    {
        DrawCard();
    }
}

public Card DrawCard()
{
    // Reshuffle discard into draw pile if empty
    if (drawPile.Count == 0)
    {
        if (discardPile.Count == 0)
        {
            Debug.Log("No cards left to draw!");
            return null;
        }
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }

    // Draw from top
    Card drawnCard = drawPile[0];
    drawPile.RemoveAt(0);

    // Add to hand
    handManager.AddCardToHand(drawnCard);

    Debug.Log($"Drew: {drawnCard.cardName}. Draw pile: {drawPile.Count}, Discard: {discardPile.Count}");
    return drawnCard;
}

public void DiscardCard(Card card)
{
    discardPile.Add(card);
}

public void EndTurn()
{
    turnNumber++;
    Debug.Log($"Turn {turnNumber} ended.");
    TimeManager.Instance.PassTime();
    MerchantManager.Instance.getMerchantButtonState();
    
    if (InGameUIManager.Instance != null)
    {
        InGameUIManager.Instance.UpdateTurnDisplay(turnNumber);
    }
}

public void DiscardHandAndReshuffle()
{
    // Get all cards from hand and add to discard pile
    List<Card> handCards = handManager.DiscardEntireHand();
    discardPile.AddRange(handCards);

    // Shuffle discard into draw pile
    drawPile.AddRange(discardPile);
    discardPile.Clear();
    ShuffleDeck();

    Debug.Log($"Reshuffled deck. Draw pile: {drawPile.Count}");
}

public void DrawNewHand()
{
    DrawStartingHand(startingHandNum);
    Debug.Log($"Drew {startingHandNum} cards for new day");
}

public int GetTurnNumber()
{
    return turnNumber;
}
}