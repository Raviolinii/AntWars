using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private Vector2 tileIndex;
    private Surowiec resource;
    private Feromon feromon;

    void start()
    {

    }

    public void SpawnResource()
    {
        if (resource != null)
            Instantiate(resource, gameObject.transform.position, resource.transform.rotation);
    }

    public Vector2 GetTileIndex() => tileIndex;
    public void SetTileIndex(int i, int j)
    {
        tileIndex = new Vector2(i, j);
    }
    public Feromon GetFeromon() => feromon;

}
