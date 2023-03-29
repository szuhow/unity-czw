using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetAnswer : MonoBehaviour
{
    public Button[] answerButton;
    public int answer;
    public int buttonNr;
    public GameObject gameManager;
    public GameObject uiManager;
    public GameObject PlusPanel;
    public GameObject MinusPanel;

    public Button plusDoWiarygodnosci;
    public Button minusDoWiarygodnosci;
    public Button plusDoFinansow;
    public Button minusDoFinansow;
    public Button plusDoZwyciestwa;
    public Button minusDoZwyciestwa;
    public void Start()
    {
        gameManager =GameObject.Find("GMManager");
        uiManager=GameObject.Find("UIManager");
        answerButton[0].GetComponent<Button>().onClick.AddListener(() => SetButtonCheckAnswer(1));
        answerButton[1].GetComponent<Button>().onClick.AddListener(() => SetButtonCheckAnswer(2));
        answerButton[2].GetComponent<Button>().onClick.AddListener(() => SetButtonCheckAnswer(3));

        plusDoWiarygodnosci.onClick.AddListener(() => AktualizujWiarygodnosc("+1"));
        minusDoWiarygodnosci.onClick.AddListener(() => AktualizujWiarygodnosc("-1"));
        plusDoFinansow.onClick.AddListener(() => AktualizujFinanse("+1"));
        minusDoFinansow.onClick.AddListener(() => AktualizujFinanse("-1"));
        plusDoZwyciestwa.GetComponent<Button>().onClick.AddListener(() => AktualizujPunkty("+1"));
        minusDoZwyciestwa.GetComponent<Button>().onClick.AddListener(() => AktualizujPunkty("-1"));
        LockButtons(true);
        for (int i = 0; i < answerButton.Length; i++)
        {
            answerButton[i].GetComponent<Button>().interactable = true;
        }



        }

    public void SetButtonCheckAnswer(int nr)
    {
        buttonNr = nr;
      //  Debug.Log(answer + "oraz" + buttonNr);
             Debug.Log("aaa");
        gameManager.GetComponent<MainGameManager>().numberOfClicks++;
        CheckAnswer(buttonNr);
       // gameManager.GetComponent<MainGameManager>().NextPlayer();
    }
   
    public void CheckAnswer(int nrButton)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            answerButton[i].GetComponent<Button>().interactable = false;
     
        }
        if (answer == nrButton)
        {
//                Debug.Log("65");
        //    answerButton[nrButton-1].GetComponent<Image>().color = Color.green;
        //    gameManager.GetComponent<MainGameManager>().AnswerPoints(1);
            PointsPanelPlusOn();
            uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(19);
        }
        else
        {
        //    answerButton[nrButton-1].GetComponent<Image>().color = Color.red;
        //    gameManager.GetComponent<MainGameManager>().AnswerPoints(0);
       
           // PointsPanelMinusOn();
            uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(20);
//            Debug.Log(gameManager.GetComponent<MainGameManager>().roundManager.obecnaTura);
            if(gameManager.GetComponent<MainGameManager>().roundManager.obecnaTura<6) {
            gameManager.GetComponent<MainGameManager>().NextPlayer(true);///moze tutaj dac sprawdzanie, która to tura? jeśli 5, to nie wyswietlaj karty
          
            }
        }
        answer -= 1;
        for (int i = 0; i < answerButton.Length; i++)
        {
          
            if (answer == i)
            {
                answerButton[answer].GetComponent<Image>().color = Color.green;

            }

            else answerButton[i].GetComponent<Image>().color = Color.red;


        }


    }

    public void LockButtons(bool flag)
    {

        plusDoWiarygodnosci.interactable = flag;
        minusDoWiarygodnosci.interactable = flag;
        plusDoFinansow.interactable = flag;
        minusDoFinansow.interactable = flag;


    }

    public void PointsPanelPlusOn()
    {
        LeanTween.moveLocalY(PlusPanel, -0, 1).setEaseInOutCubic();

    }
    public void PointsPanelPlusOff()
    {
        LeanTween.moveLocalY(PlusPanel, -60, 1).setEaseInOutCubic();

    }

    public void PointsPanelMinusOn()
    {
        LeanTween.moveLocalY(MinusPanel, 0, 1).setEaseInOutCubic();

    }
    public void PointsPanelMinusOff()
    {
        LeanTween.moveLocalY(MinusPanel, -60f, 1).setEaseInOutCubic();

    }

    public void AktualizujWiarygodnosc(string points)
    {
        LockButtons(false);
        switch (points)
        {
            case "-1":

              //  gameManager.GetComponent<MainGameManager>().AddPointsToUser(-1, 0, uiManager.GetComponent<UIManager>().activeUser);
               // Debug.Log("-1 wiarygodnosci dla " + uiManager.GetComponent<UIManager>().activeUser);
               // uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(22);
               // gameManager.GetComponent<MainGameManager>().NextPlayer();
                break;
            case "+1":
                gameManager.GetComponent<MainGameManager>().AddPointsToUser(1, 0, uiManager.GetComponent<UIManager>().activeUser);
              //  Debug.Log("+1 wiarygodnosci dla " + uiManager.GetComponent<UIManager>().activeUser);

                uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(21);
                break;
            default:

                break;
        }
        StartCoroutine(DeactiveCoroutine());
        gameManager.GetComponent<MainGameManager>().NextPlayer(true);


    }

    public void AktualizujFinanse(string points)
    {
        LockButtons(false);
        switch (points)
        {
            case "-1":

               // gameManager.GetComponent<MainGameManager>().AddPointsToUser(0, -1, uiManager.GetComponent<UIManager>().activeUser);
               // Debug.Log("-1 finansow dla " + uiManager.GetComponent<UIManager>().activeUser);
               // uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(23);
               // gameManager.GetComponent<MainGameManager>().NextPlayer();
                break;
            case "+1":
                gameManager.GetComponent<MainGameManager>().AddPointsToUser(0, 1, uiManager.GetComponent<UIManager>().activeUser);
                Debug.Log("+1 finansow dla " + uiManager.GetComponent<UIManager>().activeUser);
                uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(23);
                break;
            default:

                break;
        }
        StartCoroutine(DeactiveCoroutine());
        gameManager.GetComponent<MainGameManager>().NextPlayer(true);

    }

    public void AktualizujPunkty(string points)
    {


        LockButtons(false);
        switch (points)
        {
            case "-1":

               // gameManager.GetComponent<MainGameManager>().AddPointsOfVictory(-1, uiManager.GetComponent<UIManager>().activeUser);
               // Debug.Log("-1 punktow dla " + uiManager.GetComponent<UIManager>().activeUser); 
               // uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(27);
               // gameManager.GetComponent<MainGameManager>().NextPlayer();
                break;
            case "+1":

                gameManager.GetComponent<MainGameManager>().TriangleInfo[uiManager.GetComponent<UIManager>().activeUser].GetComponent<ID>().points++; // nowa linijka z 4 listopada

                gameManager.GetComponent<MainGameManager>().AddPointsOfVictory(1, uiManager.GetComponent<UIManager>().activeUser);
               // Debug.Log("+1 punktow dla " + uiManager.GetComponent<UIManager>().activeUser);
                uiManager.GetComponent<UIManager>().dialogueManager.DisplaySpecificSentence(26);
                break;
            default:

                break;
        }
        StartCoroutine(DeactiveCoroutine());
        gameManager.GetComponent<MainGameManager>().NextPlayer(true);

    }


    IEnumerator DeactiveCoroutine()
    {
        foreach(Button obj in answerButton){
            obj.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(0.5f);

        foreach (Button obj in answerButton)
        {
            obj.GetComponent<Button>().interactable = true;
        }
    }

}
