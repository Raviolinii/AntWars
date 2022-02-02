using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private List<MrowkaRobotnica> workers1;
    private List<MrowkaWojownik> warriors1;
    private List<MrowkaRobotnica> workers2;
    private List<MrowkaWojownik> warriors2;
    Vector2Int spawnIndex1;
    Vector2Int spawnIndex2;
    Vector2 spawnPosition1;
    Vector2 spawnPosition2;
    public GameObject workerType1;
    public GameObject warriorType1;
    public GameObject workerType2;
    public GameObject warriorType2;
    public GameObject mapPrefab;
    private Mapa map1;
    private Mapa map2;
    public GameObject anthill1;
    public Mrowisko anthill1Script;
    public GameObject anthill2;
    public Mrowisko anthill2Script;
    private int foodForAntWorker1 = 20;
    private int foodForAntWorker2 = 20;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMaps();

        workers1 = new List<MrowkaRobotnica>();
        workers2 = new List<MrowkaRobotnica>();
        //warriors1 = new List<MrowkaWojownik>();
        spawnIndex1 = new Vector2Int(Random.Range(0, 8), Random.Range(0, 4));
        spawnIndex2 = new Vector2Int(Random.Range(0, 8), Random.Range(0, 4));

        var workerScript1 = workerType1.GetComponent<MrowkaRobotnica>();
        workerScript1.spawnPosition = spawnIndex1;

        var workerScript2 = workerType2.GetComponent<MrowkaRobotnica>();
        workerScript2.spawnPosition = spawnIndex2;

        Invoke("SpawnAnthills", 1);

        Invoke("InitializeSpawnPositions", 1);

        Invoke("SpawnWorker1", 2);
        Invoke("SpawnWorker2", 2);
        //Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeMaps()
    {
        map1 = Instantiate(mapPrefab, new Vector3(0, 0, 0), mapPrefab.transform.rotation).GetComponent<Mapa>();
        map2 = Instantiate(mapPrefab, new Vector3(24, 0, 0), mapPrefab.transform.rotation).GetComponent<Mapa>();
    }
    private void InitializeSpawnPositions()
    {
        spawnPosition1 = map1.GetTileOfIndex(spawnIndex1.x, spawnIndex1.y).transform.position;
        spawnPosition2 = map2.GetTileOfIndex(spawnIndex2.x, spawnIndex2.y).transform.position;
    }

    public void SpawnWorker1()
    {
        Vector3 position = new Vector3(spawnPosition1.x, spawnPosition1.y);
        workerType1.GetComponent<Mrowka>().map = map1;
        GameObject instantiated = Instantiate(workerType1, position, workerType1.transform.rotation);
        MrowkaRobotnica toAdd = instantiated.GetComponent<MrowkaRobotnica>();
        workers1.Add(toAdd);
    }
    public void SpawnWorker2()
    {
        Vector3 position = new Vector3(spawnPosition2.x, spawnPosition2.y);
        workerType2.GetComponent<Mrowka>().map = map2;
        GameObject instantiated = Instantiate(workerType2, position, workerType2.transform.rotation);
        MrowkaRobotnica toAdd = instantiated.GetComponent<MrowkaRobotnica>();
        workers2.Add(toAdd);
    }

    private void SpawnAnthills()
    {
        anthill1 = map1.SpawnAnthill(spawnIndex1.x, spawnIndex1.y);
        anthill2 = map2.SpawnAnthill(spawnIndex2.x, spawnIndex2.y);
        anthill1Script = anthill1.GetComponent<Mrowisko>();
        anthill2Script = anthill2.GetComponent<Mrowisko>();
    }

    public void CheckPrices()
    {
        Debug.Log(anthill1Script.GetStoredFoodAmount());
        if (anthill1Script.GetStoredFoodAmount() >= foodForAntWorker1)
            HatchAnAntWorker1();
        if (anthill2Script.GetStoredFoodAmount() >= foodForAntWorker2)
            HatchAnAntWorker2();
    }


    private void HatchAnAntWorker1()
    {
        anthill1Script.SpendFood(foodForAntWorker1);
        foodForAntWorker1 += 30;
        SpawnWorker1();
    }
    private void HatchAnAntWorker2()
    {
        anthill2Script.SpendFood(foodForAntWorker2);
        foodForAntWorker2 += 30;
        SpawnWorker2();
    }
}
