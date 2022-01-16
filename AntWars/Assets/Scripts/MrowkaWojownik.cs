using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrowkaWojownik : Mrowka
{
    protected bool[,] goingForEnemy;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Invoke("TestPositionSet", 4);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected void TestPositionSet()
    {
        map = FindObjectOfType<Mapa>();
        transform.position = map.GetTileOfIndex(spawnPosition.x, spawnPosition.y).transform.position;
        currentPosition = spawnPosition;
        lastPosition = spawnPosition;

        int width = map.GetMapWidth();
        int height = map.GetMapHeight();

        goingForEnemy = new bool[width, height];
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Pole tile = other.GetComponent<Pole>();
            lastPosition = currentPosition;
            currentPosition = tile.GetTileIndex();

            Debug.Log(tile.GetWarriorFeromon().GetFeromonAmount());

            LeaveFeromon(currentPosition.x, currentPosition.y);

            FeromonDetection();
            UpdateDestination();
        }
    }

    protected override void FeromonDetection()
    {
        surroundings = map.GetSurroundingWarriorFeromons(currentPosition.x, currentPosition.y, lastPosition);
    }

    protected override void LeaveFeromon(int x, int y)
    {
        map.LeaveWarriorFeromonOn(x, y, 40);
        goingForEnemy[x, y] = true;
    }
}
