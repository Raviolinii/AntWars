using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surowiec : MonoBehaviour
{
    private float extractionTime = 2f;
    private IEnumerator extractCoroutine;
    private bool isUnderExtraction = false;
    private int resourceAmmount = 100;

    // dodac zmienna z robotnica, ktora zbiera zasob

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExtractionTime(float time) => extractionTime = time;
    public float GetExtractionTime() => extractionTime;

    public bool GetIsUnderExtraction() => isUnderExtraction;

    public void Extract()
    {
        if (isUnderExtraction)
            return;

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
        // dolozyc oddawanie surowcow jak bedzie kod jednostek (u robotnicy funkcja bedzie odpalona)
    }
}
