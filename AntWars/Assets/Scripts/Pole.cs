using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private Surowiec resource;
    private Feromon feromon;

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
