using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public Surowiec resource;

    // jak bedzie kod dla budynkow
    //private Budynek budynek;

    void start()
    {
        SpawnResource();
    }

    private void SpawnResource()
    {
        if (resource != null)
            Instantiate(resource, gameObject.transform.position, resource.transform.rotation);
    }

}
