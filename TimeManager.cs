using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    
    public int currentDay = 1;
    private AvailableTimes currentTime = AvailableTimes.Morning;

    public event Action<string> OnTimeChanged;
    public event Action<int> OnDayChanged;

    private enum AvailableTimes
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Trigger initial morning setup
        OnTimeChanged?.Invoke(currentTime.ToString());
    }

    public void PassTime()
    {
        currentTime = currentTime switch
        {
            AvailableTimes.Morning => AvailableTimes.Afternoon,
            AvailableTimes.Afternoon => AvailableTimes.Evening,
            AvailableTimes.Evening => AvailableTimes.Night,
            AvailableTimes.Night => AvailableTimes.Morning,
            _ => AvailableTimes.Morning
        };

        Debug.Log("Time advanced to: " + currentTime);
        OnTimeChanged?.Invoke(currentTime.ToString());

        // Night: Discard hand, reshuffle, grow crops
        if (currentTime == AvailableTimes.Night)
        {
            ExecuteNightPhase();
        }

        // Morning: New day, draw fresh hand
        if (currentTime == AvailableTimes.Morning)
        {
            ExecuteMorningPhase();
        }
    }

    private void ExecuteNightPhase()
    {
        // Discard hand and reshuffle
        DeckManager.Instance.DiscardHandAndReshuffle();

        // Crops grow
        GridManager.Instance.GrowCrops();
        
        Debug.Log("Night: Hand discarded, deck reshuffled, crops grew!");
    }

    private void ExecuteMorningPhase()
    {
        currentDay++;
        OnDayChanged?.Invoke(currentDay);
        
        // Draw new hand for the day
        DeckManager.Instance.DrawNewHand();

        Debug.Log($"=== Day {currentDay}: Drew new hand ===");
    }

    public string GetCurrentTime()
    {
        return currentTime.ToString();
    }
}