using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mrowka : MonoBehaviour
{
    private Vector2 spawnPosition = new Vector2(1, 1);
    private Vector2 currentPosition;
    private Pole[,] map;
    private Rigidbody2D rb;
    private CircleCollider2D tileDetector;
    private BoxCollider2D targetDetector;

    private int detectionRadius = 3;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tileDetector = GetComponentInChildren<CircleCollider2D>();
        // start of code to comment for tests
        map = FindObjectOfType<Mapa>().GetMap();
        transform.position = map[(int)spawnPosition.x, (int)spawnPosition.y].transform.position;
        currentPosition = spawnPosition;
        // end of that code, uncomment next line
        //Invoke("TestPositionSet", 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // To test ant not spawned during the game
    private void TestPositionSet()
    {
        map = FindObjectOfType<Mapa>().GetMap();
        transform.position = map[(int)spawnPosition.x, (int)spawnPosition.y].transform.position;
        currentPosition = spawnPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Pole tile = other.GetComponent<Pole>();
            currentPosition = tile.GetTileIndex();
        }
    }

}
