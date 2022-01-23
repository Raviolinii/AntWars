using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surowiec : MonoBehaviour
{
    private float extractionTime = 2f;
    private IEnumerator extractCoroutine;
    private bool isUnderExtraction = false;
    private int resourceAmmount = 100;
    private int resourceExtractionAmmount = 20;
    private MrowkaRobotnica extractingAnt;

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

    public void Extract(MrowkaRobotnica ant)
    {
        if (isUnderExtraction)
            return;

        isUnderExtraction = true;
        extractCoroutine = ExtractCoroutine();
        extractingAnt = ant;
        StartCoroutine(extractCoroutine);
    }

    public void AbortExtraction()
    {
        if (extractCoroutine != null)
        {
            StopCoroutine(extractCoroutine);
            extractingAnt = null;
            isUnderExtraction = false;
        }
    }

    IEnumerator ExtractCoroutine()
    {
        yield return new WaitForSeconds(extractionTime);
        ExtractionFinished();
    }

    private void ExtractionFinished()
    {
        if (resourceAmmount > resourceExtractionAmmount)
        {
            extractingAnt.TakeFood(resourceExtractionAmmount);
            resourceAmmount -= resourceExtractionAmmount;
        }
        else
        {
            extractingAnt.TakeFood(resourceAmmount);
            resourceAmmount = 0;
        }

        extractingAnt.FinishExtraction();

        if (resourceAmmount == 0)
            Destroy(gameObject);
        
        isUnderExtraction = false;
    }
}
