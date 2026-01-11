using System.Collections.Generic;
using UnityEngine;
using CardPackage;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{
    public static MerchantManager Instance { get; private set; }
    public Button merchantButton;
    public Canvas purchaseCanvas;

    void Awake()
    {
        Instance = this;
    }

    public void getMerchantButtonState()
    {
        if (TimeManager.Instance.GetCurrentTime() == "Morning")
        {
            merchantButton.interactable = true;
        }
        else
        {
            merchantButton.interactable = false;
        }
    }
}
