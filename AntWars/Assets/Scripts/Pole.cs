using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private Surowiec resource;
    

    void start()
    {
        if (resource != null)
            SpawnResource();
    }

    private void SpawnResource()
    {
        Instantiate(resource, gameObject.transform.position, resource.transform.rotation);
    }

}
