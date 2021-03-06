using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feromon : MonoBehaviour
{
    private int amount;
    private float vanishingTime;
    private int vanishingAmount;
    private int maxAmount = 60;

    // Start is called before the first frame update
    void Start()
    {        
        SetFeromonAmount(0);
        SetVanishingTime(5f);
        SetVanishingAmount(1);

        BeginFeromonVanishing();
    }

    public int GetFeromonAmount() => amount;
    private void SetFeromonAmount(int value) => amount = value;
    public void AddFeromon(int value) 
    {
        if (value + amount < maxAmount)
            amount += value;
        else
            amount = maxAmount;
    }
    public void DecreaseFeromonAmount(int value)
    {
        if (amount > value)
            amount -= value;
        else
            amount = 0;        
    }
    public void RemoveFeromon() => amount = 0;
    public float GetVanishingTime() => vanishingTime;
    public void SetVanishingTime(float value) => vanishingTime = value;

    public int GetVanishingAmount() => vanishingAmount;
    public void SetVanishingAmount(int value) => vanishingAmount = value;


    private void BeginFeromonVanishing() => InvokeRepeating("FeromonVanishing", vanishingTime, vanishingTime);
    private void FeromonVanishing()
    {
        if (amount > 0)
            DecreaseFeromonAmount(vanishingAmount);
    }
}
