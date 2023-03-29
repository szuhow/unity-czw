using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingCharts : MonoBehaviour
{

    public GameObject[] VerticalBars;
    public int[] listaPunktow;
    public float lerpSpeed = 2;
    public UIManager uimanager;



    public void FillPointList()
    {


        for(int i =0; i< 4; i++)
        {
            listaPunktow[i] = GameManager.instance.users.poziomPoparciaGraczy[i];
        }

    }
    public void SetChartsPoints()
    {
        FillPointList();
        StartCoroutine(ChartsFilling());
    }
    private IEnumerator ChartsFilling()
    {
        yield return ChartsOn();
        uimanager.dialogueManager.DisplaySpecificSentence(35);
        int maximumOffset = Mathf.Max(listaPunktow);
       // Debug.Log("MAX WARTOSC Z LISTY: " + maximumOffset);
        for (int i = 0; i < VerticalBars.Length; i++)
        {

            for (float k = 0; k < RemapFloat(listaPunktow[i],0, maximumOffset, 0.1f,  1); k += Time.deltaTime)
            {
                VerticalBars[i].GetComponent<Image>().fillAmount = Mathf.Lerp(0, RemapFloat(listaPunktow[i], 0, maximumOffset, 0.1f, 1), k / RemapFloat(listaPunktow[i], 0, maximumOffset, 0.1f, 1));

                yield return null;

            }
        }


    }
    private int Remap(int value, int fromLow, int fromHigh, int toLow, int toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
    public float RemapFloat(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    private IEnumerator ChartsOn()
    {
        uimanager.animacja.RankingChartObjectOn();
        yield return new WaitForSeconds(2); ;
    }
    }
