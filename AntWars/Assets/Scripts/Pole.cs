using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private Vector2Int tileIndex;
    public GameObject food;
    public GameObject anthill;
    private Mrowisko anthillScript;
    private Surowiec resourceScript;
    private Feromon workerFeromon;
    private Feromon warriorFeromon;

    void Start()
    {
        workerFeromon = gameObject.AddComponent<Feromon>();
        warriorFeromon = gameObject.AddComponent<Feromon>();
        SpawnResource();
    }

    public void SpawnResource()
    {
        if (food != null)
        {
            Instantiate(food, gameObject.transform.position, food.transform.rotation);
            resourceScript = food.GetComponent<Surowiec>();
        }
    }
    public void SpawnAnthill()
    {
        if (anthill != null)
        {
            Instantiate(anthill, gameObject.transform.position, anthill.transform.rotation);
            anthillScript = anthill.GetComponent<Mrowisko>();
        }
    }

    public void AssignFood(GameObject foodPrefab) => food = foodPrefab;
    public void AssignAnthill(GameObject anthillPrefab) => anthill = anthillPrefab;

    public Vector2Int GetTileIndex() => tileIndex;
    public void SetTileIndex(int i, int j)
    {
        tileIndex = new Vector2Int(i, j);
    }
    public Feromon GetWorkerFeromon() => workerFeromon;
    public Feromon GetWarriorFeromon() => warriorFeromon;

}
