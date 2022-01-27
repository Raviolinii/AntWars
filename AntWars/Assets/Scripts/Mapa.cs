using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    private int width = 5;
    private int height = 5;
    public Pole tile;
    public GameObject food;
    public GameObject anthill;
    private Pole[,] map;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateMap()
    {
        map = new Pole[width, height];

        float positionX = 0;
        float positionY = 0;
        Vector3 newPosition;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // obliczanie pozycji dla kazdego pola, j jest na minusie, zeby pola szly w dol, zeby pole [0,0] bylo w lewym gornym rogu
                positionX = tile.transform.localScale.x * i;
                positionY = tile.transform.localScale.y * -j;
                newPosition = new Vector3(positionX, positionY, 0);

                map[i, j] = Instantiate(tile, newPosition, tile.transform.rotation);
                map[i, j].SetTileIndex(i, j);
            }
        }
    }
    public Pole[,] GetMap() => map;

    public int?[] GetSurroundingWorkerFeromons(int x, int y, Vector2Int lastPosition)
    {
        int?[] feromonsArray = new int?[8];

        feromonsArray[0] = WorkerFeromonOrNullAsign(x - 1, y + 1, lastPosition);
        feromonsArray[1] = WorkerFeromonOrNullAsign(x, y + 1, lastPosition);
        feromonsArray[2] = WorkerFeromonOrNullAsign(x + 1, y + 1, lastPosition);

        feromonsArray[3] = WorkerFeromonOrNullAsign(x - 1, y, lastPosition);
        feromonsArray[4] = WorkerFeromonOrNullAsign(x + 1, y, lastPosition);

        feromonsArray[5] = WorkerFeromonOrNullAsign(x - 1, y - 1, lastPosition);
        feromonsArray[6] = WorkerFeromonOrNullAsign(x, y - 1, lastPosition);
        feromonsArray[7] = WorkerFeromonOrNullAsign(x + 1, y - 1, lastPosition);

        return feromonsArray;
    }

    private int? WorkerFeromonOrNullAsign(int x, int y, Vector2Int lastPosition)
    {
        //Debug.Log(x + " " + y);
        if (x == lastPosition.x && y == lastPosition.y)
            return null;            
        try
        {            
            return map[x, y].GetWorkerFeromon().GetFeromonAmount();
        }
        catch (System.IndexOutOfRangeException)
        {
            return null;
        }
    }

    public int?[] GetSurroundingWarriorFeromons(int x, int y, Vector2Int lastPosition)
    {
        int?[] feromonsArray = new int?[8];

        feromonsArray[0] = WarriorFeromonOrNullAsign(x - 1, y + 1, lastPosition);
        feromonsArray[1] = WarriorFeromonOrNullAsign(x, y + 1, lastPosition);
        feromonsArray[2] = WarriorFeromonOrNullAsign(x + 1, y + 1, lastPosition);

        feromonsArray[3] = WarriorFeromonOrNullAsign(x - 1, y, lastPosition);
        feromonsArray[4] = WarriorFeromonOrNullAsign(x + 1, y, lastPosition);

        feromonsArray[5] = WarriorFeromonOrNullAsign(x - 1, y - 1, lastPosition);
        feromonsArray[6] = WarriorFeromonOrNullAsign(x, y - 1, lastPosition);
        feromonsArray[7] = WarriorFeromonOrNullAsign(x + 1, y - 1, lastPosition);

        return feromonsArray;
    }

    private int? WarriorFeromonOrNullAsign(int x, int y, Vector2Int lastPosition)
    {
        if (x == lastPosition.x && y == lastPosition.y)
            return null; 
        try
        {
            return map[x, y].GetWarriorFeromon().GetFeromonAmount();
        }
        catch (System.IndexOutOfRangeException)
        {
            return null;
        }
    }


    public int GetMapWidth() => width;
    public int GetMapHeight() => height;

    public Pole GetTileOfIndex(int x, int y)
    {
        return map[x, y];
    }

    public void LeaveWorkerFeromonOn(int x, int y, int value)
    {
        map[x, y].GetWorkerFeromon().AddFeromon(value);
    }
    public void LeaveWarriorFeromonOn(int x, int y, int value)
    {
        map[x, y].GetWarriorFeromon().AddFeromon(value);
    }
}
