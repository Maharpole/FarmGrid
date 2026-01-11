using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardPackage;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance { get; private set; }

    public TMP_Text turnNumberText;
    public TMP_Text timeOfDayText;
    public TMP_Text actionPointText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Initialize AP display from actual ActionPointManager value
        if (ActionPointManager.Instance != null)
        {
            UpdateRemainingAP(ActionPointManager.Instance.actionPoints);
        }
    }

    public void UpdateTurnDisplay(int turn)
    {
        turnNumberText.text = "Turn: " + turn;
        timeOfDayText.text = TimeManager.Instance.GetCurrentTime();
    }

    public void UpdateRemainingAP(int APRemaining)
    {
        actionPointText.text = "AP: " + APRemaining;
    }
}