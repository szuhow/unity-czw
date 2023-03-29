using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDCard : MonoBehaviour
{
    public string name;
    public int plus;
    public string plusIndex;
    public int minus;
    public string minusIndex;
    public int pieniadze;
    public int wiarygodnosc;
    public string category;
    public Button TwojeNowiny;
    public int UserIndexParent;
    void Start()
    {
        TwojeNowiny.onClick.AddListener(delegate {
            GameObject.FindWithTag("MainGameManager").GetComponent<MainGameManager>().TwojeNowinyButton();
            UpdatePoints();
            DeactivateButton();
        });
    } //klikniecie powoduje dezaktywacje przycisku i aktualizacje punktow

    public void DeactivateButton()
    {
        TwojeNowiny.interactable = false;
    }
    public void UpdatePoints()
    {
        MainGameManager gameManager = GameObject.FindWithTag("MainGameManager").GetComponent<MainGameManager>();
        //dac funkcje przeciazone
        gameManager.SetSelfUserScore(gameManager.uimanagerPrefab.activeUser);
        gameManager.pdbupdate.IncreasePDB(wiarygodnosc);
        gameManager.SetOtherUserScoreLeft(minus,minusIndex);
        gameManager.SetOtherUserScoreRight(plus,plusIndex);
    }
}
