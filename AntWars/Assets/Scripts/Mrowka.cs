using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Mrowka : MonoBehaviour
{
    public Vector2Int spawnPosition;
    protected Vector2Int currentPosition;
    protected Vector2Int lastPosition;
    protected Mapa map;
    protected Rigidbody2D rb;
    protected CircleCollider2D tileDetector;
    protected BoxCollider2D targetDetector;
    public Vector2 destination;
    protected float movementSpeed;
    protected int?[] surroundings;

    // Start is called before the first frame update
    protected virtual void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tileDetector = GetComponentInChildren<CircleCollider2D>();

        // start of code to comment for tests

        map = FindObjectOfType<Mapa>();

        currentPosition = spawnPosition;
        lastPosition = spawnPosition;

        // end of that code, uncomment next line

        //Invoke("TestPositionSet", 4);
        //FeromonDetection();
        //Invoke("FeromonDetection", 5);
        //Invoke("Move", 6);

        //destination = new Vector2();
        surroundings = new int?[8];
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    protected abstract void FeromonDetection();

    protected int RouletteTileSelection(int?[] values)
    {
        int index;
        int sum = 0;
        int rand;
        for (int i = 0; i < 8; i++)
        {
            if (values[i] != null)
            {
                sum += (int)values[i] + 2;
                values[i] = sum;
            }
        }

        rand = Random.Range(1, sum + 1);
        index = FindIndex(values, rand);

        return index;
    }

    protected int FindIndex(int?[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                if (value <= array[i])
                    return i;
            }
        }
        throw new System.Exception("Couldnt find index of element from feromons array");
    }

    protected void UpdateDestination()
    {
        int x, y;
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

    protected abstract void LeaveFeromon(int x, int y);

    protected void Move()
    {
        if (destination != null)
        {
            Rotate();
            movementSpeed = (1 / Vector2.Distance(transform.position, destination)) * Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, destination, movementSpeed);
        }
    }

    protected void Rotate()
    {
        Vector3 norTar = (new Vector3(destination.x, destination.y, 0) - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 135);
        transform.rotation = rotation;
    }
}
