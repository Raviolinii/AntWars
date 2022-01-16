using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrowkaWojownik : Mrowka
{
    protected bool[,] goingForEnemy;

    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected void TestPositionSet()
    {
        map = FindObjectOfType<Mapa>();
        transform.position = map.GetTileOfIndex(spawnPosition.x, spawnPosition.y).transform.position;
        currentPosition = spawnPosition;

        int width = map.GetMapWidth();
        int height = map.GetMapHeight();

        goingForEnemy = new bool[width, height];
    }

    protected override void FeromonDetection()
    {
        throw new System.NotImplementedException();
    }

    protected override void LeaveFeromon(int x, int y)
    {
        throw new System.NotImplementedException();
    }
}
