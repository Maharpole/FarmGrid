using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    
    public int money = 100;
    private Dictionary<CropDefinition, int> crops = new Dictionary<CropDefinition, int>();
    
    public event Action<int> OnMoneyChanged;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void AddCrop(CropDefinition crop, int amount = 1)
    {
        if (!crops.ContainsKey(crop))
            crops[crop] = 0;
        crops[crop] += amount;
        Debug.Log($"Added {amount} {crop.cropName}. Total: {crops[crop]}");
    }
    
    public void SellCrop(CropDefinition crop)
    {
        if (crops.ContainsKey(crop) && crops[crop] > 0)
        {
            crops[crop]--;
            money += crop.baseSellValue;
            OnMoneyChanged?.Invoke(money);
            Debug.Log($"Sold {crop.cropName} for {crop.baseSellValue}. Money: {money}");
        }
    }
    
    public void SellAllCrops()
    {
        foreach (var kvp in crops)
        {
            int value = kvp.Value * kvp.Key.baseSellValue;
            money += value;
        }
        crops.Clear();
        OnMoneyChanged?.Invoke(money);
    }
    
    public int GetCropCount(CropDefinition crop)
    {
        return crops.ContainsKey(crop) ? crops[crop] : 0;
    }
}