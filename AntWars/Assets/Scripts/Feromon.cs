using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feromon : MonoBehaviour
{
    private int amount;
    private float vanishingTime;
    private int decreseAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetFeromonuAmount() => amount;
    private void SetFeromonAmount(int value) => amount = value;
    public void AddFeromon(int value) => amount += value;
    public void DecreaseFeromonAmount(int value)
    {
        if (amount >= value)
            amount -= value;
        else
            amount = 0;
    }
    public void RemoveFeromon() => amount = 0;

    private void BeginFeromonVanishing() => InvokeRepeating("FeromonVanishing", vanishingTime, vanishingTime);
    private void FeromonVanishing() => DecreaseFeromonAmount(decreseAmount);
}
