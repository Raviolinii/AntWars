using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    private int width = 20;
    private int height = 20;
    public Pole tile;
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

    public int?[] GetSurroundingFeromons(int x, int y)
    {
        int?[] feromonsArray = new int?[8];

        feromonsArray[0] = FeromonOrNullAsign(x - 1, y + 1);
        feromonsArray[1] = FeromonOrNullAsign(x, y + 1);
        feromonsArray[2] = FeromonOrNullAsign(x + 1, y + 1);

        feromonsArray[3] = FeromonOrNullAsign(x - 1, y);
        feromonsArray[4] = FeromonOrNullAsign(x + 1, y);

        feromonsArray[5] = FeromonOrNullAsign(x - 1, y - 1);
        feromonsArray[6] = FeromonOrNullAsign(x, y - 1);
        feromonsArray[7] = FeromonOrNullAsign(x + 1, y - 1);

        return feromonsArray;
    }

    private int? FeromonOrNullAsign(int x, int y)
    {
        try
        {
            return map[x, y].GetFeromon().GetFeromonAmount();        
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

    public void LeaveFeromonOn(int x, int y, int value)
    {
        map[x, y].GetFeromon().AddFeromon(value);
    }
}
