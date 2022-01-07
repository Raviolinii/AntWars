using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mrowka : MonoBehaviour
{
    private Vector2 spawnPosition = new Vector2(1,1);
    private Vector2 currentPosition;
    private GameObject[,] map;
    private Rigidbody2D rb;
    private CircleCollider2D tileDetector;
    private BoxCollider2D targetDetector;

    private int detectionRadius = 3;
    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<Mapa>().GetMap();
        rb = GetComponent<Rigidbody2D>();
        currentPosition = spawnPosition;
        tileDetector = GetComponentInChildren<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Tile"))
            {
                // get tile number and update current position
            }
    }
}
