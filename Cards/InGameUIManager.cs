using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardPackage;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance { get; private set; }

    public TMP_Text turnNumberText;

    void Awake()
    {
        Instance = this;
        
    }

    public void UpdateTurnDisplay(int turn)
    {
        turnNumberText.text = "Turn: " + turn;
    }
}