using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surowiec : MonoBehaviour
{
    private float extractionTime;
    private IEnumerator extractCoroutine;
    private bool isUnderExtraction;

    // 0 = ziemia, 1 = kamien, 2 = jedzenie
    private int resourceType;
    private int resourceAmmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExtractionTime(float time) => extractionTime = time;
    public float GetExtractionTime()
    {
        return extractionTime;
    }

    public void Extract()
    {
        extractCoroutine = ExtractCoroutine();
        isUnderExtraction = true;
        StartCoroutine(extractCoroutine);
    }

    public void AbortExtraction()
    {
        if (extractCoroutine != null)
        {
            StopCoroutine(extractCoroutine);
            isUnderExtraction = false;
        }
        
    }

    IEnumerator ExtractCoroutine()
    {
        yield return new WaitForSeconds(extractionTime);
        isUnderExtraction = false;
        ExtractionFinished();
    }

    private void ExtractionFinished()
    {
        Destroy(gameObject);
    }
}
