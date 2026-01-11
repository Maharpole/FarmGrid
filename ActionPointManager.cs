using UnityEngine;
using System;

public class ActionPointManager : MonoBehaviour
{
    public static ActionPointManager Instance { get; private set; }

    public int actionPoints = 0;

    public event Action<int> OnPointsChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged += OnTimeChanged;
        }
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= OnTimeChanged;
        }
    }

    public void OnTimeChanged(string timeOfDay)
    {
        switch (timeOfDay)
        {
            case "Morning":
                actionPoints = 1;
                Debug.Log("Morning: 1 AP");
                break;
            case "Afternoon":
                actionPoints = 1;
                Debug.Log("Afternoon: 1 AP");
                break;
            case "Evening":
                actionPoints = 1;
                Debug.Log("Evening: 1 AP");
                break;
            case "Night":
                actionPoints = 0;
                Debug.Log("Night: No actions");
                break;
        }
        OnPointsChanged?.Invoke(actionPoints);
        
        // Update the UI
        if (InGameUIManager.Instance != null)
        {
            InGameUIManager.Instance.UpdateRemainingAP(actionPoints);
        }
    }

    public bool CanPlayCard(int cost)  // FIX: Add parameter
    {
        return actionPoints >= cost;
    }

    public bool TrySpendAP(int amount)  // Keep only this version
    {
        if (actionPoints >= amount)
        {
            actionPoints -= amount;
            OnPointsChanged?.Invoke(actionPoints);
            InGameUIManager.Instance.UpdateRemainingAP(actionPoints);
            Debug.Log($"Spent {amount} AP. Remaining: {actionPoints}");
            return true;
        }
        Debug.Log($"Not enough AP! Need {amount}, have {actionPoints}");
        return false;
    }
}