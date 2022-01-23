using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private List<MrowkaRobotnica> workers;
    private List<MrowkaWojownik> warriors;
    Vector2Int spawnIndex;
    Vector2 spawnPosition;
    public GameObject workerType;
    public GameObject warriorType;
    private Mapa map;
    public Mrowisko anthill;

    // Start is called before the first frame update
    void Start()
    {
        workers = new List<MrowkaRobotnica>();
        warriors = new List<MrowkaWojownik>();
        spawnIndex = new Vector2Int(1, 1);
        var workerScript = workerType.GetComponent<MrowkaRobotnica>();
        workerScript.spawnPosition = spawnIndex;

        Invoke("InitializeMap", 1);

        Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
        //Invoke("SpawnWorker", 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeMap()
    {
        map = FindObjectOfType<Mapa>();
        spawnPosition = map.GetTileOfIndex(spawnIndex.x, spawnIndex.y).transform.position;
    }

    public void SpawnWorker()
    {
        Vector3 position = new Vector3(spawnPosition.x, spawnPosition.y);
        GameObject instantiated = Instantiate(workerType, position, workerType.transform.rotation);
        MrowkaRobotnica toAdd = instantiated.GetComponent<MrowkaRobotnica>();
        workers.Add(toAdd);
    }
}
