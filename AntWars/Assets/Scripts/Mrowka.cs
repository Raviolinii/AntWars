using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mrowka : MonoBehaviour
{
    private Vector2Int spawnPosition = new Vector2Int(1, 1);
    private Vector2Int currentPosition;
    private bool[,] goingForFoodMemory;
    private bool[,] goingWithFoodMemory;
    private Mapa map;
    private Rigidbody2D rb;
    private CircleCollider2D tileDetector;
    private BoxCollider2D targetDetector;

    private Vector2 destination;
    private float movementSpeed;

    private bool hasFood;
    private int?[] surroundings;

    private int detectionRadius = 3;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tileDetector = GetComponentInChildren<CircleCollider2D>();

        // start of code to comment for tests
        /* 
        map = FindObjectOfType<Mapa>();
        transform.position = map.GetTileOfIndex(spawnPosition.x, spawnPosition.y).transform.position;
        currentPosition = spawnPosition;

        int width = map.GetMapWidth();
        int height = map.GetMapHeight();

        goingForFoodMemory = new bool[width, height];
        goingWithFoodMemory = new bool[width, height]; 
        */

        // end of that code, uncomment next line
        Invoke("TestPositionSet", 4);
        //Invoke("FeromonDetection", 5);
        //Invoke("Move", 6);

        destination = new Vector2();
        hasFood = false;
        surroundings = new int?[8];
    }

    // Update is called once per frame
    void Update()
    {
        if (destination != null)
        {
            movementSpeed = (1 / Vector2.Distance(transform.position, destination)) * Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, destination, movementSpeed);
        }
    }

    // To test ant not spawned during the game
    private void TestPositionSet()
    {
        map = FindObjectOfType<Mapa>();
        transform.position = map.GetTileOfIndex(spawnPosition.x, spawnPosition.y).transform.position;
        currentPosition = spawnPosition;

        int width = map.GetMapWidth();
        int height = map.GetMapHeight();

        goingForFoodMemory = new bool[width, height];
        goingWithFoodMemory = new bool[width, height];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Pole tile = other.GetComponent<Pole>();
            currentPosition = tile.GetTileIndex();

            Debug.Log(tile.GetFeromon().GetFeromonAmount());

            LeaveFeromon(currentPosition.x, currentPosition.y);

            FeromonDetection();
            UpdateDestination();
        }
    }
    private void FeromonDetection()
    {
        surroundings = map.GetSurroundingFeromons(currentPosition.x, currentPosition.y);
    }


    private int RouletteTileSelection(int?[] values)
    {
        int index;
        int sum = 0;
        int rand;
        for (int i = 0; i < 8; i++)
        {
            if (values[i] != null)
            {
                sum += (int)values[i] + 5;
                values[i] = sum;
            }
        }

        rand = Random.Range(1, sum + 1);
        index = FindIndex(values, rand);

        return index;
    }

    private int FindIndex(int?[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                //Debug.Log($"index = {i}, array value: {array[i]}, value: {value}");

                if (value <= array[i])
                    return i;
            }
        }
        throw new System.Exception("Couldnt find index of element from feromons array");
    }

    private void UpdateDestination()
    {
        int x = -1, y = -1;
        int index = RouletteTileSelection(surroundings);
        switch (index)
        {
            case 0:
                x = currentPosition.x - 1;
                y = currentPosition.y + 1;
                break;
            case 1:
                x = currentPosition.x;
                y = currentPosition.y + 1;
                break;
            case 2:
                x = currentPosition.x + 1;
                y = currentPosition.y + 1;
                break;
            case 3:
                x = currentPosition.x - 1;
                y = currentPosition.y;
                break;
            case 4:
                x = currentPosition.x + 1;
                y = currentPosition.y;
                break;
            case 5:
                x = currentPosition.x - 1;
                y = currentPosition.y - 1;
                break;
            case 6:
                x = currentPosition.x;
                y = currentPosition.y - 1;
                break;
            case 7:
                x = currentPosition.x + 1;
                y = currentPosition.y - 1;
                break;
            default:
                throw new System.Exception($"Couldnt find tile with index = {index}");
        }
        Vector2Int moveTo = new Vector2Int(x, y);
        //Debug.Log(moveTo);
        destination = map.GetTileOfIndex(moveTo.x, moveTo.y).transform.position;
    }

    private void LeaveFeromon(int x, int y)
    {
        if (hasFood && !goingWithFoodMemory[x, y])
        {
            map.LeaveFeromonOn(x, y, 40);
            goingWithFoodMemory[x, y] = true;
        }
        else if (!hasFood && !goingForFoodMemory[x, y])
        {
            map.LeaveFeromonOn(x, y, 20);
            goingForFoodMemory[x, y] = true;
        }
    }
}
