using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mrowisko : MonoBehaviour
{
    private int startingFood = 0;
    public int storedFood;
    private PlayerManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        storedFood = startingFood;
        gameManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreFood(int value)
    {
        storedFood += value;
        gameManager.CheckPrices();
    }
    public int GetStoredFoodAmount() => storedFood;
    public bool SpendFood(int value)
    {
        if (value <= storedFood)
        {
            storedFood -= value;
            return true;
        }
        return false;
    }
}
