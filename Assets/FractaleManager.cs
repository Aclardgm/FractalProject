using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FractaleManager : MonoBehaviour
{
    Material fractale;

    float Zoom = 1000.0f;
    float IterationMax = 10;

    //-2.1 -> 0.6
    float OffsetX = 0f;
    //-1.2 -> 1.2
    float OffsetY = 0.5f;
    float OffsetR = 0.5f;
    float OffsetG = 0.5f;
    float OffsetB = 0.5f;
    float RotAngle = 0;
    float OffSetComplexA = 0;
    float OffSetComplexB = 0;
    float PowCount = 0;


    float targetOffsetX = 0.5f;
    float targetOffsetY = 0.5f;
    float targetZoom = 1;
    float targetIteratioMax = 10;
    float targetOffsetR = 0.5f;
    float targetOffsetG= 0.5f;
    float targetOffsetB= 0.5f;
    float targetRotAngle= 0;
    float targetOffsetComplexA = 0;
    float targetOffsetComplexB = 0;
    float targetPowCount = 0;

    float startAnim;
    float timeAnim = 5f;
    

    public Text IterationCountText;


    bool StopMove = false;

    // Start is called before the first frame update
    void Start()
    {
        fractale = GetComponent<Image>().material;
        StartCoroutine("Animate");
        IterationCountText.text = ((int)targetIteratioMax).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        SetValues();
    }

    public void AddIteration()
    {
        targetIteratioMax++;
        IterationCountText.text = ((int)targetIteratioMax).ToString();
    }

    public void ReduceIteration()
    {
        if(targetIteratioMax-1 != 1)
        {
            targetIteratioMax--;
            IterationCountText.text = ((int)targetIteratioMax).ToString();
        }
    }

    public void ToggleMove()
    {
        StopMove = !StopMove;
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(timeAnim);
        startAnim = Time.time;
        OffsetX = targetOffsetX;
        OffsetY = targetOffsetY;
        OffsetR = targetOffsetR;
        OffsetG = targetOffsetG;
        OffsetB = targetOffsetB;
        Zoom = targetZoom;
        RotAngle = targetRotAngle;
        OffSetComplexA = targetOffsetComplexA;
        OffSetComplexB = targetOffsetComplexB;
        IterationMax = targetIteratioMax;
        PowCount = targetPowCount;

        if (!StopMove)
        {
            if ((int)(Random.value * 2) % 2 == 0)
            {
                targetOffsetX = Random.Range(-1.3f, 0.3f) + 1.5f;
            }
            if ((int)(Random.value * 2) % 2 == 0)
            {
                targetOffsetY = Random.Range(-0.8f, 0.8f) + 0.5f;
            }
            targetZoom = Random.value * Mathf.Pow(10000, Random.Range(1, 2));// ;
            targetRotAngle = Random.value * 360;
        }

        if ((int)(Random.value * 2) % 2 == 0)
        {
            targetOffsetComplexA = Random.Range(-1.5f, 1.5f);
        }
        if ((int)(Random.value * 2) % 2 == 0)
        {
            targetOffsetComplexB = Random.Range(-1.5f, 1.5f);
        }


        //if ((int)(Random.value * 2) % 2 == 0)
        //{
        //    targetPowCount = Random.Range(2, 2);
        //}
        targetPowCount = 2;


        //if ((int)(Random.value*2) % 2 == 0)
        //{
        //    targetIteratioMax = Random.Range(10, 100);
        //}
        if ((int)(Random.value * 2) % 2 == 0)
        {
            targetOffsetR = Random.Range(0.0f, 1.0f);
        }
        if ((int)(Random.value * 2) % 2 == 0)
        {
            targetOffsetG = Random.Range(0.0f, 1.0f);
        }
        if ((int)(Random.value * 2) % 2 == 0)
        {
            targetOffsetB = Random.Range(0.0f, 1.0f);
        }
        timeAnim = Random.Range(5.0f, 8.0f);
        StartCoroutine("Animate");
    }

    void SetValues()
    {

        float x = Mathf.Lerp(OffsetX, targetOffsetX, (Time.time - startAnim) / timeAnim);
        float y = Mathf.Lerp(OffsetY, targetOffsetY, (Time.time - startAnim) / timeAnim);
        float r = Mathf.Lerp(OffsetR, targetOffsetR, (Time.time - startAnim) / timeAnim);
        float g = Mathf.Lerp(OffsetG, targetOffsetG, (Time.time - startAnim) / timeAnim);
        float b = Mathf.Lerp(OffsetB, targetOffsetB, (Time.time - startAnim) / timeAnim);
        float z = Mathf.Lerp(Zoom, targetZoom, (Time.time - startAnim) / timeAnim);
        float i = (int)Mathf.Lerp(IterationMax, targetIteratioMax, (Time.time - startAnim) / timeAnim);
        float rot = Mathf.Lerp(RotAngle, targetRotAngle, (Time.time - startAnim) / timeAnim) * Mathf.Deg2Rad;


        float offCompA = Mathf.Lerp(OffSetComplexA, targetOffsetComplexA, (Time.time - startAnim) / timeAnim);
        float offCompB = Mathf.Lerp(OffSetComplexB, targetOffsetComplexB, (Time.time - startAnim) / timeAnim);


        float powC = Mathf.Lerp(PowCount, targetPowCount, (Time.time - startAnim) / timeAnim);

        //x = 0.6f;
        //y = 0;

        if(!StopMove)
        {
            fractale.SetFloat("_OffsetX", x);
            fractale.SetFloat("_OffsetY", y);
            fractale.SetFloat("_Zoom", z);
            //fractale.SetFloat("_RotAngle", rot);
        }

        fractale.SetFloat("_OffsetR", r);
        fractale.SetFloat("_OffsetG", g);
        fractale.SetFloat("_OffsetB", b);
        fractale.SetFloat("_IterationMax", targetIteratioMax);


        if (fractale.HasProperty("_OffSetComplexA") && fractale.HasProperty("_OffSetComplexB"))
        {
            fractale.SetFloat("_OffSetComplexA", offCompA);
            fractale.SetFloat("_OffSetComplexB", offCompB);
        }


        if (fractale.HasProperty("_PowCount"))
        {
            fractale.SetFloat("_PowCount", (int)powC);
        }

    }
}
