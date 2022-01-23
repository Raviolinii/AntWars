using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrowkaRobotnica : Mrowka
{
    protected bool[,] goingForFoodMemory;
    protected bool[,] goingWithFoodMemory;
    //protected bool hasFood = false;
    protected int foodAmount = 0;
    protected BoxCollider2D foodAndAnthillDetector;
    protected bool foodOrHillDetected = false;
    protected Surowiec foodScript;
    protected Mrowisko anthillScript;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        int width = map.GetMapWidth();
        int height = map.GetMapHeight();
        goingForFoodMemory = new bool[width, height];
        goingWithFoodMemory = new bool[width, height];

        foodAndAnthillDetector = GetComponentInChildren<BoxCollider2D>();

        //Invoke("TestPositionSet", 4);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        LookForFoor();
        if (anthillScript != null)
        {
            if (transform.position == anthillScript.transform.position)
            {
                StoreFood();
            }
        }
    }

    // To test ant not spawned during the game
    protected void TestPositionSet()
    {
        map = FindObjectOfType<Mapa>();
        transform.position = map.GetTileOfIndex(spawnPosition.x, spawnPosition.y).transform.position;
        currentPosition = spawnPosition;
        lastPosition = spawnPosition;

        int width = map.GetMapWidth();
        int height = map.GetMapHeight();

        goingForFoodMemory = new bool[width, height];
        goingWithFoodMemory = new bool[width, height];
    }

    protected override void FeromonDetection()
    {
        surroundings = map.GetSurroundingWorkerFeromons(currentPosition.x, currentPosition.y, lastPosition);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Food") && foodAmount == 0 && foodOrHillDetected == false)
        {
            foodOrHillDetected = true;
            destination = other.transform.position;
            foodScript = other.GetComponent<Surowiec>();
        }
        if(other.CompareTag("Anthill") && foodAmount != 0)
        {
            foodOrHillDetected = true;
            destination = other.transform.position;
            anthillScript = other.gameObject.GetComponent<Mrowisko>();
        }
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Pole tile = other.GetComponent<Pole>();
            lastPosition = currentPosition;
            currentPosition = tile.GetTileIndex();

            LeaveFeromon(currentPosition.x, currentPosition.y);

            FeromonDetection();
            if (!foodOrHillDetected)
                UpdateDestination();
        }
    }

    protected override void LeaveFeromon(int x, int y)
    {
        if (foodAmount != 0 && !goingWithFoodMemory[x, y])
        {
            map.LeaveWorkerFeromonOn(x, y, 40);
            goingWithFoodMemory[x, y] = true;
        }
        else if (foodAmount == 0 && !goingForFoodMemory[x, y])
        {
            map.LeaveWorkerFeromonOn(x, y, 20);
            goingForFoodMemory[x, y] = true;
        }
    }

    protected void LookForFoor()
    {
        if (foodScript != null)
        {
            if (transform.position == foodScript.transform.position)
                BeginExtraction();
        }
        else if (foodScript == null && foodOrHillDetected && foodAmount == 0)
            FinishExtraction();
    }

    protected void StoreFood()
    {
        anthillScript.StoreFood(foodAmount);
        foodAmount = 0;
        foodOrHillDetected = false;
        anthillScript = null;
        ReverseDirection();
    }

    protected void BeginExtraction() => foodScript.Extract(this);

    protected void ReverseDirection() => destination = map.GetTileOfIndex(lastPosition.x, lastPosition.y).transform.position;

    public void TakeFood(int value)
    {
        foodAmount = value;
        Debug.Log(foodAmount);
    }

    public void FinishExtraction()
    {
        foodScript = null;
        foodOrHillDetected = false;
        ReverseDirection();
    }
}
