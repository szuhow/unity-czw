using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
  
    public int liczbaTur = 5;
    public int obecnaTura= 1;
    public Text turaUIText;
    public int currentUserId;
    public MainGameManager MainGameManagerObject;
    void Start()
    {
        SetRoundUI();
      
    }
    /*
    public void SetTourNumber(int i)
    {

        switch (i+1)
        {
            case 2:
                liczbaTur = 5;
                break;
            case 3:
                liczbaTur = 6;
                break;
            case 4:
                liczbaTur = 7;
                break;
            case 5:
                liczbaTur = 8;
                break;


            default:
                break;
        }

        SetRoundUI();
    }
    */
    public void SetRoundUI()
    {
        turaUIText.text = obecnaTura + "/" + liczbaTur ;
        if (obecnaTura == liczbaTur)
        {
            MainGameManagerObject.OstatniaTura();
        }
        else if (obecnaTura > liczbaTur)
        {   
            Debug.Log("OSTATNIA TURA ROUNDMANAGER");
            MainGameManagerObject.EndOfGame();
            obecnaTura = liczbaTur;
            turaUIText.text = obecnaTura + "/" + liczbaTur;
          //  MainGameManagerObject.uimanagerPrefab.animacja.HideAllAnyway();

        }
    }

   

}
