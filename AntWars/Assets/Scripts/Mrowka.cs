using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Mrowka : MonoBehaviour
{
    private Vector2Int spawnPosition = new Vector2Int(2, 2);
    private Vector2Int currentPosition;
    private Pole[,] map;
    private Rigidbody2D rb;
    private CircleCollider2D tileDetector;
    private BoxCollider2D targetDetector;

    private bool hasFood;

    // radius wont be changed, script will remain commented
    //private int surroundingsRadius;
    private int?[] surroundings;

    private int detectionRadius = 3;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tileDetector = GetComponentInChildren<CircleCollider2D>();

        // start of code to comment for tests
        /* map = FindObjectOfType<Mapa>().GetMap();
        transform.position = map[spawnPosition.x, spawnPosition.y].transform.position;
        currentPosition = spawnPosition; */

        // end of that code, uncomment next line
        Invoke("TestPositionSet", 4);
        //Invoke("FeromonDetection", 5);
        //Invoke("Move", 6);
        hasFood = false;
        surroundings = new int?[8];
    }

    // Update is called once per frame
    void Update()
    {

    }

    // To test ant not spawned during the game
    private void TestPositionSet()
    {
        map = FindObjectOfType<Mapa>().GetMap();
        transform.position = map[spawnPosition.x, spawnPosition.y].transform.position;
        currentPosition = spawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Debug.Log(gameObject.tag);
        if (other.gameObject.CompareTag("Tile"))
        {
            Pole tile = other.GetComponent<Pole>();
            currentPosition = tile.GetTileIndex();

            FeromonDetection();
            StartCoroutine("WaitAMinute");
                
        }
    }

    IEnumerator WaitAMinute()
    {
        yield return new WaitForSeconds(0.2f);
        Move();
    }
    private void FeromonDetection()
    {
        // radius wont be changed, script will remain commented
        //surroundings = new int [surroundingsRadius * 8];

        if (!hasFood)
        {
            // getting feromon amount of surrounding tiles            
            FeromonOrNullAsign(0, currentPosition.x - 1, currentPosition.y + 1);
            FeromonOrNullAsign(1, currentPosition.x, currentPosition.y + 1);
            FeromonOrNullAsign(2, currentPosition.x + 1, currentPosition.y + 1);

            FeromonOrNullAsign(3, currentPosition.x - 1, currentPosition.y);
            FeromonOrNullAsign(4, currentPosition.x + 1, currentPosition.y);

            FeromonOrNullAsign(5, currentPosition.x - 1, currentPosition.y - 1);
            FeromonOrNullAsign(6, currentPosition.x, currentPosition.y - 1);
            FeromonOrNullAsign(7, currentPosition.x + 1, currentPosition.y - 1);

        }
        else
        {
            return;
        }
    }

    // if ant is on the edge or int the corner it will asingn null
    private void FeromonOrNullAsign(int index, int x, int y)
    {
        try
        {
            surroundings[index] = map[x, y].GetFeromon().GetFeromonAmount();
        }
        catch (System.IndexOutOfRangeException)
        {
            surroundings[index] = null;
        }
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
                sum += (int)values[i] + 1;
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
                Debug.Log($"index = {i}, array value: {array[i]}, value: {value}");

                if (value <= array[i])
                    return i;
            }
        }
        throw new System.Exception("Couldnt find index of element from feromons array");
    }

    private void Move()
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
        Debug.Log(moveTo);
        rb.MovePosition(map[moveTo.x, moveTo.y].transform.position);
        //rb.AddForce(moveTo,ForceMode2D.Impulse);
    }
}
