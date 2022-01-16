using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrowkaRobotnica : Mrowka
{

    protected bool[,] goingForFoodMemory;
    protected bool[,] goingWithFoodMemory;
    protected bool hasFood;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hasFood = false;
        Invoke("TestPositionSet", 4);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Pole tile = other.GetComponent<Pole>();
            lastPosition = currentPosition;
            currentPosition = tile.GetTileIndex();

            Debug.Log(tile.GetWorkerFeromon().GetFeromonAmount());

            LeaveFeromon(currentPosition.x, currentPosition.y);

            FeromonDetection();
            UpdateDestination();
        }
    }

    protected override void LeaveFeromon(int x, int y)
    {
        if (hasFood && !goingWithFoodMemory[x, y])
        {
            map.LeaveWorkerFeromonOn(x, y, 40);
            goingWithFoodMemory[x, y] = true;
        }
        else if (!hasFood && !goingForFoodMemory[x, y])
        {
            map.LeaveWorkerFeromonOn(x, y, 20);
            goingForFoodMemory[x, y] = true;
        }
    }
}
