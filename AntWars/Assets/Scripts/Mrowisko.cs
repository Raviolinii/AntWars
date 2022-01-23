using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mrowisko : MonoBehaviour
{

    private int startingFood = 0;
    private int foodStored;

    // Start is called before the first frame update
    void Start()
    {
        foodStored = startingFood;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreFood(int value) => foodStored += value;
    public int GetStoredFoodAmount() => foodStored;
    public bool SpendFood(int value)
    {
        if (value <= foodStored)
        {
            foodStored -= value;
            return true;
        }
        return false;
    }
}
