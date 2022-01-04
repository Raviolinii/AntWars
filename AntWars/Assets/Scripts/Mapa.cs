using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    private int width = 10;
    private int height = 10;
    public GameObject pole; 
    private GameObject[,] map;

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
        map = new GameObject[width, height];
        
        float positionX = 0;
        float positionY = 0;
        Vector3 newPosition;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // obliczanie pozycji dla kazdego pola, j jest na minusie, zeby pola szly w dol, zeby pole [0,0] bylo w lewym gornym rogu
                positionX = pole.transform.localScale.x * i;
                positionY = pole.transform.localScale.y * -j;
                newPosition = new Vector3(positionX, positionY, 0);

                map[i,j] = Instantiate(pole, newPosition, pole.transform.rotation);
            }
        }
    }

}
