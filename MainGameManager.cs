using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public class RankingPoints
    {
        public int punktyZwyciestwaRanking;
        public string playerName;
        

        public RankingPoints(int punkty, string punkty2)
        {
            punktyZwyciestwaRanking = punkty;
            playerName = punkty2;
        }
    }
    public CoroutineMethods iMethods;
    public RandomGenerator rand;
    public UIManager uimanagerPrefab;
    public PDBUpdate pdbupdate;
    public GameObject singlePointObject = null;
    public GameObject parentPanel;
    public GameObject[] TriangleInfo;
    public GameObject[] playerCharacters;
    public GameObject[] playerCharactersWithGrey;
    public GameObject grey;
    public List<GameObject> turnUserList = new List<GameObject>();
    public List<GameObject> NewsCardList = new List<GameObject>();
    public List<GameObject> turnHumanUserList = new List<GameObject>();
    public PlayerNewsDeck newsDeck;
    public PlayerQuestionDeck questionDeck;
    public static System.Random random = new System.Random();
    public List<Card> toPrint = new List<Card>();
    public GameObject newsCardParent;
    public GameObject newsCardPrefab;
    //  public GameObject[] currentNewsCard;
    public List<GameObject> currentNewsCard = new List<GameObject>();
    public List<GameObject> chosenNewsCardList = new List<GameObject>();
    public GameObject[] chosenNewsCard;
    public GameObject questionCard;
    public GameObject currentQuestionCard;
    public GameObject questionCardParent;
    public ToggleGroup cardNewsGroup;
    public GameObject dicePlus;
    public GameObject dice;
    public GameObject globalNewsCard;
    public Sprite GradientIMG;
    public bool diceflag = true;
    public int panelGraczaIndex = 1;
    public RoundManager roundManager;
    public bool ifNewsCardAvailable = false;
    public bool isEndOfRound = false;
    public bool unlockPanel = false;
    public float delay = 0.01f;
    public string fullText;
    public string currentText = "";
    // public int prevValueScore;
    // public int actualvariablescore;
    public int playerUserOrder = 0;
    public int playerUserQuestionOrder = 0;
    public int userToRepeatWithPanel = 0;
    public bool leaveOneCardFlag = false; //Twoje nowiny
    public bool leaveOneCardFlagWithPoints = false; //Wiadomosci z polski
    public bool koniecFazyRedakcyjnej = true;
    public bool fazaRedakcyjna = false;
    public bool fazaNewsow=false;
    public List<GameObject> punktyZwyciestwa = new List<GameObject>();
    public List<ToggleGroup> toggleGroupsNewsCard = new List<ToggleGroup>();
    public GameObject WybierzKarteNewsaButton;
    public Sprite x1;
    public Sprite x2;
    public Sprite x3;
    public Sprite x4; 
    public bool TwojeNowinyButtonFlag = false;
    public bool IfUsePoints = true;
    public int MnoznikPunktow = 1;
    public bool EndOfGameFlag = false;
    public int a = 0;
    public List<int> sortPoparcie_Sorted = new List<int>(5);
    public List<int> sortWiarygodnosc_Sorted = new List<int>(5);
    public List<int> sortFinanse_Sorted = new List<int>(5);
    public RankingCharts rankingObject;
    public List<RankingPoints> rankingPoints = new List<RankingPoints>();
    public int userWithDice = 0;
    public int iAlpha = 0;
    public int globalTourCounter = 1;
    public RestartGame restartGame;
    public int displayScore;
    public NegativeNewsCardsNr negGameObject;
    public bool BlokadaOstatniejTury = true;
    public List<Text> punktyZwyciestwaOnGUI = new List<Text>(5);
    public bool koniecGryTag = false;

    public int numberOfClicks = 0;
    // List<KeyValuePair<int, int>> wiarygodnosc = new List<KeyValuePair<int, int>>();
    //List<KeyValuePair<int, int>> finanse = new List<KeyValuePair<int, int>>();
    // Dictionary<int, GameObject> punktyZw = new Dictionary<int, GameObject>();    public enum PunktyZW{zielony =0 , czerwony, niebieski, czarny, szary, none};
   


    public enum PlayerColors { zielony, czerwony, niebieski, czarny, szary, bialy, none }
    public enum Players
    {
        zielony = 22,
        czerwony = 21,
        niebieski = 23,
        czarny = 24,
        szary = 25,
        bialy = 0,
    }

   
    public enum CardColors
    {
        none, fakeNews, trescTabloidowa, trescPropagandowa, zmanipulowanaTresc, rzetelneDziennikarstwo, poglebionaAnaliza, dziennikarstwoSledcze, rzetelnyReportaz
    }
    //public int score;
    //And you also need a variable that holds the increasing score number, let's call it display score
   
    void Start()
    {
  
      //  WybierzGracza = PunktyZW.none;
        // uimanagerPrefab.animacja.DiceOn();
        //score = 0;
        displayScore = 0;
        //punktyZw.Add(0, punktyZwyciestwa[0]);
        //punktyZw.Add(1, punktyZwyciestwa[1]);
        //punktyZw.Add(2, punktyZwyciestwa[2]);
        //punktyZw.Add(3, punktyZwyciestwa[3]);
        //punktyZw.Add(4, punktyZwyciestwa[4]);
        // actualvariablescore = prevValueScore;
    }
    // Start is called before the first frame update
    public void Generuj()
    {
        // Debug.Log(rand.sampleNormal());
        //rand.RandomGen();
        // text.text = GameManager.instance.users.user[0];

    }
   
    public void PlayGame()
    {
        pdbupdate.pdbcontrol.locked = true;
        StartCoroutine("SetData");
        uimanagerPrefab.animacja.UITuraON();
        uimanagerPrefab.animacja.ResetButtonOff();
        //uimanagerPrefab.animacja.OffMainMenu();
        GetOrderBySupport();

        //uimanagerPrefab.animacja.DiceOn();
        // GenerujKartePytan();
    }
    public void OstatniaTura()
    {
        MnoznikPunktow = 2;
        //PoliczPunkty();
    }
    public void EndOfGame()
    {
        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(32);
        koniecGryTag = true;
        SortListAtEndOfGame();
        EndOfGameFlag = true;
        Debug.Log("Koniec gry");
        uimanagerPrefab.animacja.NewsCardPanelOffWithButtons();
        uimanagerPrefab.animacja.HideAllAnyway();
    }


    public void SortListAtEndOfGame()
    {
        uimanagerPrefab.animacja.HideAllAnyway();
        //RankingScore();
        PDBScore();
        RankingScore();

        rankingObject.SetChartsPoints();
        foreach (GameObject Character in playerCharacters)
        {
            // Debug.Log(Character.GetComponent<ID>().poparcie);
            //ID charTracker = Character.GetComponent<ID>();
            // NewsCardList = 

            // playerCharacters.OrderBy(x => x.GetComponent<ID>().poparcie).ToList();
        }





    }

    public void PDBScore()
    {
        if (uimanagerPrefab.PDBScore > 10 && uimanagerPrefab.PDBScore < 21) GameManager.instance.users.punktyZwyciestwa[4] += 1;
        else if (uimanagerPrefab.PDBScore > 20 && uimanagerPrefab.PDBScore < 31) GameManager.instance.users.punktyZwyciestwa[4] += 3;
    }




    public void RankingScore()
    {
        var sortPoparcie = new Dictionary<int, int>();
        var sortWiarygodnosc = new Dictionary<int, int>();
        var sortFinanse = new Dictionary<int, int>();

        var sortDziennikarstwoSledcze = new Dictionary<int, int>();
        var sortRzetelneDziennikarstwo = new Dictionary<int, int>();
        var sortRzetelnyReportaz = new Dictionary<int, int>();
        var sortPoglebionaAnaliza = new Dictionary<int, int>();

        for (int numItem4 = 0; numItem4 < 4; numItem4++)
        {
            sortDziennikarstwoSledcze.Add(numItem4, playerCharacters[numItem4].GetComponent<ID>().categoryCardScore["Dziennikarstwo śledcze"]);
            sortRzetelneDziennikarstwo.Add(numItem4, playerCharacters[numItem4].GetComponent<ID>().categoryCardScore["Rzetelne dziennikarstwo"]);
            sortRzetelnyReportaz.Add(numItem4, playerCharacters[numItem4].GetComponent<ID>().categoryCardScore["Rzetelny reportaż"]);
            sortPoglebionaAnaliza.Add(numItem4, playerCharacters[numItem4].GetComponent<ID>().categoryCardScore["Pogłębiona analiza"]);
        }
        List<int> sortDziennikarstwoSledcze_Sorted = new List<int>(5);
        List<int> sortRzetelneDziennikarstwo_Sorted = new List<int>(5);
        List<int> sortRzetelnyReportaz_Sorted = new List<int>(5);
        List<int> sortPoglebionaAnaliza_Sorted = new List<int>(5);

        sortDziennikarstwoSledcze_Sorted = sortDziennikarstwoSledcze.Values.ToList();
        sortRzetelneDziennikarstwo_Sorted = sortRzetelneDziennikarstwo.Values.ToList();
        sortRzetelnyReportaz_Sorted = sortRzetelnyReportaz.Values.ToList();
        sortPoglebionaAnaliza_Sorted = sortPoglebionaAnaliza.Values.ToList();

        sortDziennikarstwoSledcze_Sorted.Sort();
        sortRzetelneDziennikarstwo_Sorted.Sort();
        sortRzetelnyReportaz_Sorted.Sort();
        sortPoglebionaAnaliza_Sorted.Sort();

        sortDziennikarstwoSledcze_Sorted.Reverse();
        sortRzetelnyReportaz_Sorted.Reverse();
        sortPoglebionaAnaliza_Sorted.Reverse();
        sortPoglebionaAnaliza_Sorted.Reverse();

        if (sortDziennikarstwoSledcze.FirstOrDefault(x => x.Value == sortDziennikarstwoSledcze_Sorted[0]).Key == 1 && sortDziennikarstwoSledcze_Sorted[0] != 0)
        {
            GameManager.instance.users.punktyZwyciestwa[1] += 2;
            Debug.Log("Zwycięzca w kategorii dziennikarstwo sledcze:" + sortDziennikarstwoSledcze_Sorted[0] + " wygral gracz " + sortDziennikarstwoSledcze.FirstOrDefault(x => x.Value == sortDziennikarstwoSledcze_Sorted[0]).Key);

        }
        if (sortRzetelneDziennikarstwo.FirstOrDefault(x => x.Value == sortRzetelneDziennikarstwo_Sorted[0]).Key == 0 && sortRzetelneDziennikarstwo_Sorted[0] != 0)
        {
            GameManager.instance.users.punktyZwyciestwa[0] += 2;
            Debug.Log("Zwycięzca w kategorii rzetelne dziennikarstwo:" + sortRzetelneDziennikarstwo_Sorted[0] + " wygral gracz " + sortRzetelneDziennikarstwo.FirstOrDefault(x => x.Value == sortRzetelneDziennikarstwo_Sorted[0]).Key);

        }
        if (sortRzetelnyReportaz.FirstOrDefault(x => x.Value == sortRzetelnyReportaz_Sorted[0]).Key == 2 && sortRzetelnyReportaz_Sorted[0] != 0)
        {
            GameManager.instance.users.punktyZwyciestwa[2] += 2;
            Debug.Log("Zwycięzca w kategorii rzetelny reportaz:" + sortRzetelnyReportaz_Sorted[0] + " wygral gracz " + sortRzetelnyReportaz.FirstOrDefault(x => x.Value == sortRzetelnyReportaz_Sorted[0]).Key);

        }
        if (sortPoglebionaAnaliza.FirstOrDefault(x => x.Value == sortPoglebionaAnaliza_Sorted[0]).Key == 3 && sortPoglebionaAnaliza_Sorted[0] != 0)
        {
            GameManager.instance.users.punktyZwyciestwa[3] += 2;
            Debug.Log("Zwycięzca w kategorii poglebiona analiza:" + sortPoglebionaAnaliza_Sorted[0] + " wygral gracz " + sortPoglebionaAnaliza.FirstOrDefault(x => x.Value == sortPoglebionaAnaliza_Sorted[0]).Key);

        }

        // sprawdzenie liczby kart zmniejszajacych wiarygodnosc gracza bezstronnego
        GameManager.instance.users.punktyZwyciestwa[4] -= negGameObject.CheckNr();

        for (int numItem = 0; numItem < GameManager.instance.users.poziomPoparciaGraczy.Count; numItem++)
        {
            sortPoparcie.Add(numItem, GameManager.instance.users.poziomPoparciaGraczy[numItem]);
            sortWiarygodnosc.Add(numItem, GameManager.instance.users.poziomWiarygodnosciGraczy[numItem]);
            sortFinanse.Add(numItem, GameManager.instance.users.poziomFinansowGraczy[numItem]);
        }

        sortPoparcie_Sorted = sortPoparcie.Values.ToList();
        sortWiarygodnosc_Sorted = sortWiarygodnosc.Values.ToList();
        sortFinanse_Sorted = sortFinanse.Values.ToList();
        sortPoparcie_Sorted.Sort();
        sortWiarygodnosc_Sorted.Sort();
        sortFinanse_Sorted.Sort();
        sortPoparcie_Sorted.Reverse();
        sortWiarygodnosc_Sorted.Reverse();
        sortFinanse_Sorted.Reverse();


        // pierwszy warunek celu, obczaic o co tu biegalo  - jesli zwyciestwo indywidualne to daj 3 punkty, jesli remisowe to 1 punkt
        var duplicateValues = sortPoparcie.GroupBy(x => x.Value).Where(x => x.Count() > 1);
        if (duplicateValues.Count() > 1)
        {
            if (sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Value == duplicateValues.ElementAt(0).Key)
            {
                if (sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Key != 4)
                {
                    GameManager.instance.users.punktyZwyciestwa[sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Key] += 1;
                }
                Debug.Log("powtórzenie poparcia");
            }
        }
        else if (duplicateValues.Count() == 0)
        {
            Debug.Log("Brak powtórzeń poparcia");
            if (sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Key != 4)
            {
                GameManager.instance.users.punktyZwyciestwa[sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Key] += 3;
            }
        }


        int diff12 = Mathf.Abs(GameManager.instance.users.poziomPoparciaGraczy[0] - GameManager.instance.users.poziomPoparciaGraczy[1]);
        int diff13 = Mathf.Abs(GameManager.instance.users.poziomPoparciaGraczy[0] - GameManager.instance.users.poziomPoparciaGraczy[2]);
        int diff14 = Mathf.Abs(GameManager.instance.users.poziomPoparciaGraczy[0] - GameManager.instance.users.poziomPoparciaGraczy[3]);
        int diff23 = Mathf.Abs(GameManager.instance.users.poziomPoparciaGraczy[1] - GameManager.instance.users.poziomPoparciaGraczy[2]);
        int diff24 = Mathf.Abs(GameManager.instance.users.poziomPoparciaGraczy[1] - GameManager.instance.users.poziomPoparciaGraczy[3]);
        int diff34 = Mathf.Abs(GameManager.instance.users.poziomPoparciaGraczy[2] - GameManager.instance.users.poziomPoparciaGraczy[3]);

        if (diff12 <= 5)
        {
            GameManager.instance.users.punktyZwyciestwa[4] += 1;
           // Debug.Log("diff12");
        }
        if (diff13 <= 5)
        { 
            GameManager.instance.users.punktyZwyciestwa[4] += 1;
           // Debug.Log("diff13");
        }

        if (diff14 <= 5)
        {
            GameManager.instance.users.punktyZwyciestwa[4] += 1;
           //// Debug.Log("diff14");
        }
        if (diff23 <= 5)
        {
            GameManager.instance.users.punktyZwyciestwa[4] += 1;
            Debug.Log("diff23");
        }
        if (diff24 <= 5)
        {
            GameManager.instance.users.punktyZwyciestwa[4] += 1;
            //Debug.Log("diff24");
        }
        if (diff34 <= 5)
        {
            GameManager.instance.users.punktyZwyciestwa[4] += 1;
        }
        if (GameManager.instance.users.poziomPoparciaGraczy[0] <= 24
        && GameManager.instance.users.poziomPoparciaGraczy[1] <= 24
        && GameManager.instance.users.poziomPoparciaGraczy[2] <= 24
        && GameManager.instance.users.poziomPoparciaGraczy[3] <= 24)
            GameManager.instance.users.punktyZwyciestwa[4] += 2;

        if ((int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[0] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[0]) / 4) > 0) GameManager.instance.users.punktyZwyciestwa[0] += (int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[0] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[0]) / 4); // drugi warunek w karcie celow      
        if ((int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[1] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[1]) / 4) > 0) GameManager.instance.users.punktyZwyciestwa[1] += (int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[1] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[1]) / 4);
        if ((int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[2] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[2]) / 4) > 0) GameManager.instance.users.punktyZwyciestwa[2] += (int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[2] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[2]) / 4);
        if ((int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[3] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[3]) / 4) > 0) GameManager.instance.users.punktyZwyciestwa[3] += (int)Mathf.Floor((GameManager.instance.users.poziomPoparciaGraczy[3] - GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[3]) / 4);

        // 3 warunek karty celow gracza bezstronnego
        if (GameManager.instance.users.poziomPoparciaGraczy[4] < 30) GameManager.instance.users.punktyZwyciestwa[4] += 2; 

        if (GameManager.instance.users.poziomPoparciaGraczy[3] > GameManager.instance.users.poziomPoparciaGraczy[2]) GameManager.instance.users.punktyZwyciestwa[1] += 2;
        if (GameManager.instance.users.poziomPoparciaGraczy[2] > GameManager.instance.users.poziomPoparciaGraczy[1]) GameManager.instance.users.punktyZwyciestwa[0] += 2;
        if (GameManager.instance.users.poziomPoparciaGraczy[0] > GameManager.instance.users.poziomPoparciaGraczy[3]) GameManager.instance.users.punktyZwyciestwa[2] += 2;
        if (GameManager.instance.users.poziomPoparciaGraczy[1] > GameManager.instance.users.poziomPoparciaGraczy[0]) GameManager.instance.users.punktyZwyciestwa[3] += 2;

        GameManager.instance.users.punktyKartCelow.AddRange(GameManager.instance.users.punktyZwyciestwa);
        
        //-----------------------------------------------------------------------------------------------------------------------------------------
        // z tabeli instrukcji
        if (uimanagerPrefab.turnList.Count == 3)
        {
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[0]).Key] += 2;
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[1]).Key] += 1;

            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[0]).Key] += 2;
            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[1]).Key] += 1;
        }
        else if (uimanagerPrefab.turnList.Count == 4)
        {
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[0]).Key] += 3;
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[1]).Key] += 1;
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[2]).Key] += 1;

            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[0]).Key] += 3;
            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[1]).Key] += 1;
            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[2]).Key] += 1;
        }
        else if (uimanagerPrefab.turnList.Count == 5)
        {

            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[0]).Key] += 4;
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[1]).Key] += 3;
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[2]).Key] += 2;
            GameManager.instance.users.punktyZwyciestwa[sortFinanse.FirstOrDefault(x => x.Value == sortFinanse_Sorted[3]).Key] += 1;

            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[0]).Key] += 4;
            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[1]).Key] += 3;
            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[2]).Key] += 2;
            GameManager.instance.users.punktyZwyciestwa[sortWiarygodnosc.FirstOrDefault(x => x.Value == sortWiarygodnosc_Sorted[3]).Key] += 1;
        }

        Debug.Log("Zwycięzca w kategorii poparcie:" + sortPoparcie_Sorted[0] + " wygral gracz " + sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Key);
        Debug.Log("Zwycięzca w kategorii finanse:" + sortFinanse_Sorted[0] + " wygral gracz " + sortFinanse.FirstOrDefault(y => y.Value == sortFinanse_Sorted[0]).Key);
        Debug.Log("Zwycięzca w kategorii wiarygodnosc:" + sortWiarygodnosc_Sorted[0] + " wygral gracz " + sortWiarygodnosc.FirstOrDefault(z => z.Value == sortWiarygodnosc_Sorted[0]).Key);
        //uimanagerPrefab.scoreLog.text = "Zwycięzca w kategorii poparcie: " + sortPoparcie_Sorted[0] + " wygral gracz " + TriangleInfo[sortPoparcie.Keys.ToList().IndexOf(sortPoparcie.FirstOrDefault(x => x.Value == sortPoparcie_Sorted[0]).Key)].GetComponent<ID>().Name + "\n";
        //uimanagerPrefab.scoreLog.text += "Zwycięzca w kategorii finanse: " + sortFinanse_Sorted[0] + " wygral gracz " + TriangleInfo[sortFinanse.Keys.ToList().IndexOf(sortFinanse.FirstOrDefault(y => y.Value == sortFinanse_Sorted[0]).Key)].GetComponent<ID>().Name + "\n";
        //uimanagerPrefab.scoreLog.text += "Zwycięzca w kategorii wiarygodnosc: " + sortWiarygodnosc_Sorted[0] + " wygral gracz " + TriangleInfo[sortWiarygodnosc.Keys.ToList().IndexOf(sortWiarygodnosc.FirstOrDefault(z => z.Value == sortWiarygodnosc_Sorted[0]).Key)].GetComponent<ID>().name + "\n";
        //rankingPoints.Add(new RankingPoints(GameManager.instance.users.punktyZwyciestwa, GameManager.instance.users.userName));
        string name;
        for (int cnt = 0; cnt < GameManager.instance.users.punktyZwyciestwa.Count; cnt++)
        {
           
            if (TriangleInfo[cnt].GetComponent<ID>().Name == "") name = TriangleInfo[cnt].GetComponent<ID>().name; else name = TriangleInfo[cnt].GetComponent<ID>().Name;
            uimanagerPrefab.scoreLog.text += name +  " - liczba punktów zwycięstwa: " + GameManager.instance.users.punktyZwyciestwa[cnt] + "\n";

        }
        int maxValue = GameManager.instance.users.punktyZwyciestwa.Max();
        int maxIndex = GameManager.instance.users.punktyZwyciestwa.ToList().IndexOf(maxValue);
        uimanagerPrefab.scoreLog.text += "Najwięcej punktów zdobył: " + TriangleInfo[maxIndex].GetComponent<ID>().gameObject.name;
    }


    private IEnumerator SetData()
    {
        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(15);
        uimanagerPrefab.metoda.PokazPanel();
        uimanagerPrefab.animacja.ShowAll();
        uimanagerPrefab.animacja.LogoOn();
        SetUserData();
        SetPoints(5, true);
        yield return new WaitForSeconds(5.0f);
        uimanagerPrefab.metoda.SchowajPanel();
        uimanagerPrefab.animacja.HideAll();
        uimanagerPrefab.animacja.LogoOff();
        //  uimanagerPrefab.Dalej_Button.SetActive(false);
        //// uimanagerPrefab.animacja.ExitButton.SetActive(false);
        // uimanagerPrefab.resetButton.gameObject.SetActive(false);
        //  uimanagerPrefab.StartRoundButton.SetActive(true);
        uimanagerPrefab.animacja.StartRoundButtonPolozenieWyzej();
        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(16);
        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(25);
    }
     
    private static Hashtable hueColourValues = new Hashtable{
         { PlayerColors.zielony,     new Color32( 23 ,166 , 0, 255 ) },
         { PlayerColors.czerwony,     new Color32( 236 , 34 , 32, 255 ) },
         { PlayerColors.niebieski,     new Color32( 7 , 170 , 234, 255 ) },
         { PlayerColors.czarny,     new Color32( 22 , 17 , 17, 255 ) },
         { PlayerColors.szary,     new Color32( 137 , 137 , 137, 255 ) },
         { PlayerColors.none,     new Color32( 0 , 0 , 0, 255 ) },
         { PlayerColors.bialy,     new Color32( 255 , 255 , 255,255) },
     };

    private static Hashtable hueColourValuesNewsCard = new Hashtable{
         { CardColors.fakeNews,     new Color32( 237 ,125 , 49, 255 ) },
         { CardColors.trescTabloidowa,     new Color32( 255,51 , 204, 255 ) },
         { CardColors.trescPropagandowa,     new Color32( 255,255 , 0, 255 ) },
         { CardColors.zmanipulowanaTresc,     new Color32( 204 , 204 , 0, 255 ) },
         { CardColors.rzetelneDziennikarstwo,     new Color32( 31 , 78 ,121, 255 ) },
         { CardColors.poglebionaAnaliza,     new Color32( 153 , 0 , 51, 255 ) },
         { CardColors.dziennikarstwoSledcze,     new Color32( 153, 153 , 255, 255 ) },
         { CardColors.rzetelnyReportaz,     new Color32( 0 , 128 , 128,255) },
         { CardColors.none,     new Color32( 0 , 0, 0,255) },
     };

    public static Color32 HueColourValue(PlayerColors color)
    {
        return (Color32)hueColourValues[color];
    }
    public static Color32 HueColourValueNewsCard(CardColors color)
    {
        return (Color32)hueColourValuesNewsCard[color];
    }
    public void SetUserData()
    {


        foreach (GameObject child in TriangleInfo)
        {
            // child.GetComponent<ID>().Name = 

            for (int i = 0; i < GameManager.instance.users.userName.Count; i++)
            {
                if (GameManager.instance.users.KartyCelu.IndexOf(child.GetComponent<ID>().KartaCelu) > -1)
                {
                    child.GetComponent<ID>().Name = GameManager.instance.users.userName[GameManager.instance.users.KartyCelu.IndexOf(child.GetComponent<ID>().KartaCelu)];
                    // child.GetComponent<ID>().Wiek = int.Parse(GameManager.instance.users.userAge[GameManager.instance.users.KartyCelu.IndexOf(child.GetComponent<ID>().KartaCelu)]);

                    foreach (Text text in child.GetComponentsInChildren<Text>())
                    {

                        if (text.name == "UserNameAgeText")
                        {
                            text.text = GameManager.instance.users.userName[GameManager.instance.users.KartyCelu.IndexOf(child.GetComponent<ID>().KartaCelu)];// + "," + GameManager.instance.users.userAge[GameManager.instance.users.KartyCelu.IndexOf(child.GetComponent<ID>().KartaCelu)];
                        }

                    }
                }
            }

        }


    }
    public void GetOrderBySupport()
    {

        //wywołane przed każdą turą

        //playerCharacters = GameObject.FindGameObjectsWithTag("Gracz");
        foreach (GameObject Character in playerCharacters)
        {
            // Debug.Log(Character.GetComponent<ID>().poparcie);
            //ID charTracker = Character.GetComponent<ID>();
            turnUserList = playerCharacters.OrderBy(x => x.GetComponent<ID>().poparcie).ToList();

        }
        turnUserList.Add(grey);

        foreach (GameObject player in turnUserList)
        {
            if (player.GetComponent<ID>().Name != "") turnHumanUserList.Add(player);
        }
    }

    public void TurnTogglesOnOff(bool flag)
    {


        for (int i = 0; i < playerCharactersWithGrey.Length; i++)
        {
            playerCharactersWithGrey[i].GetComponent<ID>().TurnToggles(flag);
        }
    }

    public void TurnTogglesOnOffExcept(bool flag, int wyjatek)
    {


        for (int i = 0; i < playerCharactersWithGrey.Length; i++)
        {
            playerCharactersWithGrey[i].GetComponent<ID>().TurnTogglesExcept(flag, wyjatek);
        }
    }

    public void NextPlayerWithoutNewsCard()
    {
        if(fazaNewsow==true){
        int temp = int.Parse(punktyZwyciestwaOnGUI[uimanagerPrefab.activeUser].text)-1;
        punktyZwyciestwaOnGUI[uimanagerPrefab.activeUser].text = temp.ToString();
        NextPlayer(false);}
    }

    public void NextPlayer(bool waitFlag) //, bool IfShowCard = true
    {
      
        if (!EndOfGameFlag && roundManager.obecnaTura < 6)
        {
            
            if (BlokadaOstatniejTury)  uimanagerPrefab.currentUserIndicator.text = "Aktualny gracz: " + turnHumanUserList[playerUserOrder].GetComponent<ID>().Name + " (" + turnHumanUserList[playerUserOrder].GetComponent<ID>().kolor + ")";
            else uimanagerPrefab.currentUserIndicator.text = "";



            if (!leaveOneCardFlag && !leaveOneCardFlagWithPoints)
            {
                fazaRedakcyjna = false;
                fazaNewsow=true;
                
              // if (BlokadaOstatniejTury){ 
                 //   Debug.Log("KOLEJNY GRACZ" + " po "+BlokadaOstatniejTury);
                if(!(roundManager.obecnaTura == 5 && numberOfClicks == GameManager.instance.users.userName.Count()))
                   uimanagerPrefab.animacja.UserCardOnOff(turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify, waitFlag);
//               Debug.Log("Pierwszy wylot karty");
//}

                    uimanagerPrefab.GetComponent<SetCurrentActiveUser>().SetActiveUser(turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify);
                    IfUsePoints = true;
                if (isEndOfRound)
                {
                    //Debug.Log("KoniecRundy");
                  //  if (!EndOfGameFlag) uimanagerPrefab.animacja.NewsCardPanelOffWithButtons();
                    //zamiast zwiekszenia rundy, dodac kolejny etap gry - faza pracy redakcyjnej

                    // FazaPracyRedakcyjnej();


                    if (playerUserQuestionOrder < turnHumanUserList.Count)
                    {                                          
                       if (!EndOfGameFlag) uimanagerPrefab.animacja.NewsCardPanelOffWithButtons();
                       //TurnTogglesOnOff(false);
                       TurnTogglesOnOffExcept(false, 1);
                       GenerujKartePytan();                                           
                       playerUserQuestionOrder++;

                        if (roundManager.obecnaTura == 5 ) BlokadaOstatniejTury = false;
                      //  Debug.Log("LINIA593:" +playerUserQuestionOrder);
                    }
                    else
                    {
                        
                        TurnTogglesOnOff(true);
                        isEndOfRound = false;
                        // Debug.Log("Koniec pracy redakcyjnej");
                        playerUserOrder = 0;
                        Destroy(currentQuestionCard);
                        // uimanagerPrefab.animacja.NewsCardPanelOn(); //?????????????????????
                        playerUserQuestionOrder = 0;
                        roundManager.obecnaTura++;
                      
                        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(25);
                        roundManager.SetRoundUI(); //koniec gry
                        if (BlokadaOstatniejTury) StartCoroutine(KolejnaTura());
                        else StartCoroutine(KoniecGry());
                        if (!EndOfGameFlag)
                        {
                            numberOfClicks = 0;
                            uimanagerPrefab.animacja.NewsCardPanelOnOffWithoutButton(true);
                            WybieranieKartNewsow(8);
                           
                        }
                        //Debug.Log("2");
                    }
                }
                playerUserOrder++;
                if (playerUserOrder < turnHumanUserList.Count) { }
                else
                {
                    isEndOfRound = true;
                    playerUserOrder = 0;
                }        
            
            }
            else
            {
                if (!isEndOfRound)
                {
                   // if (!EndOfGameFlag){
                    uimanagerPrefab.animacja.UserCardOnOff(turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify, waitFlag);
                   // Debug.Log("Pierwszy wylot karty");
                    //}
                    uimanagerPrefab.GetComponent<SetCurrentActiveUser>().SetActiveUser(turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify);
                    playerUserOrder++;
                    IfUsePoints = true;
                    if (playerUserOrder < turnHumanUserList.Count) { }
                    else
                    {
                        isEndOfRound = true;
                       // Debug.Log("ISENDOFROUND2");
                        Debug.Log("Koniec rundy");
                        playerUserOrder = 0;
                    }
                }
                else
                {
                    if (leaveOneCardFlag && !leaveOneCardFlagWithPoints)
                    {
                        IfUsePoints = false;
                        Debug.Log(userToRepeatWithPanel);
                        uimanagerPrefab.activeUser = userToRepeatWithPanel;
                        if (userToRepeatWithPanel != 4) uimanagerPrefab.animacja.UserCardOnOff(userToRepeatWithPanel, waitFlag); //czemu 4?
                        uimanagerPrefab.GetComponent<SetCurrentActiveUser>().SetActiveUser(userToRepeatWithPanel);
                        playerUserOrder = 0;
                        leaveOneCardFlag = false;
                    }
                    else if (!leaveOneCardFlag && leaveOneCardFlagWithPoints)
                    {
                        IfUsePoints = true;
                        Debug.Log(userToRepeatWithPanel);
                        uimanagerPrefab.activeUser = userToRepeatWithPanel;
                        if (userToRepeatWithPanel != 4) uimanagerPrefab.animacja.UserCardOnOff(userToRepeatWithPanel, waitFlag); //czemu 4?
                        uimanagerPrefab.GetComponent<SetCurrentActiveUser>().SetActiveUser(userToRepeatWithPanel);
                        playerUserOrder = 0;
                        leaveOneCardFlagWithPoints = false;
                    }
                }

             
            }
            globalTourCounter++;
        }
        
    }
    IEnumerator FazaRedakcyjna()
    {


       uimanagerPrefab.dialogueManager.Clear();
       uimanagerPrefab.animacja.FazaRedakcyjna();
        yield return new WaitForSeconds(1);
        //GenerujKartePytan();
        currentQuestionCard.SetActive(true);

    }

    IEnumerator KolejnaTura()
    {



        uimanagerPrefab.animacja.KolejnaTura();
        yield return new WaitForSeconds(4);

        //GenerujKartePytan();
        

    }
    IEnumerator KoniecGry()
    {



        uimanagerPrefab.animacja.KoniecGry();
        yield return new WaitForSeconds(4);

        //GenerujKartePytan();


    }
    IEnumerator StartAnimacji()
    {
        yield return KolejnaTura();


        

        //GenerujKartePytan();


    }

    public void FazaPracyRedakcyjnej()
    {

        GenerujKartePytan();


        uimanagerPrefab.animacja.UserCardOnOff(turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify, false);
        uimanagerPrefab.GetComponent<SetCurrentActiveUser>().SetActiveUser(turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify);
        //  uimanagerPrefab.StartRoundButton.transform.Find("Text").GetComponent<Text>().text = "Następny Gracz";
        // Debug.Log("next gracz" + turnHumanUserList[playerUserOrder].GetComponent<ID>().Identify);
        //if (leaveOneCardFlag)
        //{
        //    playerUserOrder = 0;
        //    isEndOfRound = true;
        //    leaveOneCardFlag = false;
        //}
        // }
        playerUserOrder++;

        if (playerUserOrder < turnHumanUserList.Count) { }
        else
        {
            isEndOfRound = false;
            koniecFazyRedakcyjnej = false;
            //  Debug.Log("Koniec rundy");
            playerUserOrder = 0;
            fazaRedakcyjna = false;
            fazaNewsow=true;
             
        }
        //isEndOfRound = false;
        //roundManager.obecnaTura++;
        //roundManager.SetRoundUI();
        //WybieranieKartNewsow(8);
    }

    public void WriteUsername(string text)

    {
        StopCoroutine("ShowText");
        fullText = text;
        StartCoroutine("ShowText");
    }
    // zmienić klase na bardziej uniwersalną, żeby
    // tekst był dowolnie wybranym obiektem a nie tylko górna belka
    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            uimanagerPrefab.currentUserIndicator.text = currentText;
            yield return new WaitForSeconds(delay);

        }
    }

    public void AnswerPoints(int a)
    {

        if (a == 1) currentQuestionCard.GetComponent<SetAnswer>().PointsPanelPlusOn();              //Debug.Log("Błędna odpowiedz");
        //else if (a == 0) currentQuestionCard.GetComponent<SetAnswer>().PointsPanelMinusOn();           //Debug.Log("Prawidlowa odpowiedz");
    }
    public void SetPoints(int value, bool ifPDB)
    {

        for (int i = 0; i < 5; i++)
        {
            AddPointsToCard(5, 5, i);
            StartCoroutine(iMethods.InitialSupport(TriangleInfo[i].GetComponent<ID>().poczatkowePoparcieText, TriangleInfo[i].GetComponent<ID>().poczatkowePoparcie));

        }
       
        if (ifPDB) pdbupdate.IncreasePDB(value);


    }

    public void AddPointsToCard(int wiarygodnosc, int finanse, int index)
    {
        //int index = 0;
        //     string[] array = fileInfo.Name.Split(char.Parse("_"));
        Text[] txts = TriangleInfo[index].GetComponentsInChildren<Text>();
        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    StartCoroutine(ScoreUpdater(item, wiarygodnosc));
                    GameManager.instance.users.poziomWiarygodnosciGraczy[index] += wiarygodnosc;
                    // item.text = "a";
                    break;
                case "finanse":

                    StartCoroutine(ScoreUpdater(item, finanse));
                    GameManager.instance.users.poziomFinansowGraczy[index] += finanse;
                    //item.text = "B";
                    break;
                default:

                    break;
            }
        }
      
      

    }


    public void AddPointsToUser(int wiarygodnosc, int finanse, int index)
    {
       
        //int index = 0;
        //     string[] array = fileInfo.Name.Split(char.Parse("_"));
        Text[] txts = TriangleInfo[index].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, wiarygodnosc);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[index] += wiarygodnosc;
                    // item.text = "a";
                    break;
                case "finanse":
                      //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (finanse != 999)
                    {
                        IncreaseScore(item, finanse);
                        GameManager.instance.users.poziomFinansowGraczy[index] += finanse;
                    }
                        //item.text = "B";
                    break;
                default:

                    break;
            }
        }

    }

    public void AddPointsToUser(int wiarygodnosc, int finanse, int index, int a)
    {
        //int index = 0;
        //     string[] array = fileInfo.Name.Split(char.Parse("_"));
        Text[] txts = TriangleInfo[index].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, wiarygodnosc, 0);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[index] += wiarygodnosc;
                    // item.text = "a";
                    break;
                case "finanse":

                    IncreaseScore(item, finanse, 0);
                    GameManager.instance.users.poziomFinansowGraczy[index] += finanse;
                    //item.text = "B";
                    break;
                default:

                    break;
            }
        }

    }



    public void AddPointsOfVictory(int punkty, int index)
    {
        //zwiekszanie punktow zwyciestwa

        int prevValueScore = int.Parse(punktyZwyciestwa[index].GetComponent<Text>().text);
        int actualvariablescore = punkty + prevValueScore;
        GameManager.instance.users.punktyZwyciestwa[index] = actualvariablescore;
        StartCoroutine(IncreaseVictoryPoints(punktyZwyciestwa[index].GetComponent<Text>(), prevValueScore, actualvariablescore));
    }
    public void IncreaseScore(Text item, int wynik)
    {
       // Debug.Log("zmiana wartosci kost");
        //aktualizacja wiarygodnosci i pieniedzy
        int prevValueScore = int.Parse(item.text);
        int actualvariablescore = wynik + prevValueScore;
       
        if (actualvariablescore >= 0 && actualvariablescore <= 15)
        {
          // item.fontSize = 28;
          //  item.color = Color.red;
            StartCoroutine(IncreaseScoreOverTime(item, prevValueScore, actualvariablescore));
            //// 
           // 
        }

        else if (actualvariablescore < 0) actualvariablescore = 0;
        else if (actualvariablescore > 15) actualvariablescore = 15;
       // item.color = Color.white;
        // item.fontSize = 20;
        // Debug.Log(prevValueScore+ "oraz"+ actualvariablescore +  "oraz"+wynik);
    }

    public void IncreaseScore(Text item, int wynik, int a)
    {
        //aktualizacja wiarygodnosci i pieniedzy
        int prevValueScore = int.Parse(item.text);
        int actualvariablescore = wynik + prevValueScore;

        if (actualvariablescore >= 0 && actualvariablescore <= 15)
        {
           //item.fontSize = 28;
           // item.color = Color.red;
            int score = actualvariablescore;
            item.text = score.ToString();
          //  item.fontSize = 20;
           // item.color = Color.white;
        }
        else if (actualvariablescore < 0) actualvariablescore = 0;
        else if (actualvariablescore > 15) actualvariablescore = 15;
        // Debug.Log(prevValueScore+ "oraz"+ actualvariablescore +  "oraz"+wynik);
    }


    IEnumerator IncreaseVictoryPoints(Text item, int prevValueScore, int actualvariablescore)
    {
        float seconds = 1.0f;
        float animationTime = 0f;
        Debug.Log("dodawanie z rzutu");
        yield return new WaitForSeconds(0.5f);
        while (animationTime < seconds)
        {
          //  item.fontSize = 28;
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            int score = (int)Mathf.Lerp(prevValueScore, actualvariablescore, lerpValue);
            item.text = score.ToString();
          //  item.fontSize = 20;////
            yield return null;
        }

        yield return new WaitForSeconds(1);

    }





    IEnumerator IncreaseScoreOverTime(Text item, int prevValueScore, int actualvariablescore)
    {
        float seconds = 1.0f;
        float animationTime = 0f;
        
        yield return new WaitForSeconds(0.5f);
        while (animationTime < seconds)
        {
            item.color = Color.red;
            item.fontSize = 28;
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            int score = (int)Mathf.Lerp(prevValueScore, actualvariablescore, lerpValue);       
           // item.color = Color.white;
            item.text = score.ToString();
          ////  item.fontSize = 20;
            yield return null;

        }
        item.color = Color.white;
        item.fontSize = 20;

        yield return new WaitForSeconds(1);
       
    }
    private IEnumerator ScoreUpdater(Text scoreUI, int score)
    {

        while (displayScore < score + 1)
        {
            displayScore = int.Parse(scoreUI.text);
            if (displayScore < score)
            {
                //scoreUI.fontSize = 28;
               // scoreUI.color=Color.red;
                displayScore++; //Increment the display score by 1
               // scoreUI.fontSize = 20;
               // scoreUI.color = Color.white;
                scoreUI.text = displayScore.ToString(); //Write it to the UI


            }

            // else scoreUI.fontSize = 17;
            yield return new WaitForSeconds(0.5f); // I used .2 secs but you can update it as fast as you want

        }

    }




    public void AnulujKarte()
    {




        WybierzKarteNewsaButton.GetComponent<Button>().interactable = true;
        //CheckReference(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
        if (cardNewsGroup.ActiveToggles().FirstOrDefault() != null)
        {
            if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.tag == "KartaNewsa")
                uimanagerPrefab.dialogueManager.DisplaySpecificSentence(29);
            else
            {
                SetSelfUserScore(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().UserIndexParent, 0);
                pdbupdate.IncreasePDB(-cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                SetOtherUserScoreLeft(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject, 0);
                SetOtherUserScoreRight(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject, 0);

                Destroy(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                uimanagerPrefab.animacja.CancelButtonOff();
            }

        }

        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(30);



    }

    public void UzyjPaneluGracza(string panelGracza)
    {
        if (ifNewsCardAvailable == false)
        {
            // uimanagerPrefab.dialogueManager.DisplaySpecificSentence(18);
            unlockPanel = false;
        }
        else
        {
            // uimanagerPrefab.dialogueManager.DisplaySpecificSentence(19);
            unlockPanel = true;
        }
        if (unlockPanel)
        {

            switch (panelGracza)
            {
                case "CalaPrawda":
                    uimanagerPrefab.animacja.CancelButtonOn();
                    uimanagerPrefab.animacja.NewsCardPanelOff();
                    WybierzKarteNewsaButton.GetComponent<Button>().interactable = false;
                    break;

                case "PopatrzTV":
                    PopatrzTV();
                    break;
                case "TwojeNowiny":
                    userToRepeatWithPanel = uimanagerPrefab.activeUser;
                    leaveOneCardFlag = true;
                    //  TwojeNowinyButtonFlag = true;
                    break;
                case "UslyszTO":
                    if (SprawdzCzyPierwszyWKolejce()) WybieranieKartNewsow(8);
                    break;
                case "WiadomosciZPolski":
                    userToRepeatWithPanel = uimanagerPrefab.activeUser;
                    leaveOneCardFlagWithPoints = true;
                    break;



            }


        }
        TriangleInfo[uimanagerPrefab.activeTriangle].GetComponent<ID>().IfPanelToggleUsable(false);
        // Debug.Log(TriangleInfo[uimanagerPrefab.activeUser].name);
        uimanagerPrefab.TogglePanel(false);
    }

    public bool SprawdzCzyPierwszyWKolejce()
    {

        bool flag = false;

        if (GameObject.FindGameObjectsWithTag("KartaNewsa").Length == 8)
        {
            flag = true;
            Debug.Log("Anulowanie");
        }
        return flag;
    }

    public bool ostatniaTura()
    {

        if (roundManager.obecnaTura == roundManager.liczbaTur) return true;
        else return false;
    }
    public void PopatrzTV()
    {
        if (!ostatniaTura())
        panelGraczaIndex = 2;
        else Debug.Log("Uzyto panelu gracza");
    }



    public void WybieranieKartNewsow(int numberOfCards)
    {
        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(50);
        newsDeck.Shuffle();
        toPrint.Clear();
        ifNewsCardAvailable = true;
        //  Debug.Log(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.name);
        for (int i = 0; i < numberOfCards; i++)
        {

            if (newsDeck.deck.Count >= numberOfCards) toPrint.Add(newsDeck.deck[i]);


        }
        UsunKartyNewsow(); // bez usuwania kart. niech beda np. przeniesione albo dezaktywowane
                           //  if(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject!=null) currentNewsCard.Remove(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);


        foreach (Card i in toPrint) newsDeck.deck.Remove(i); //????
        GenerujKartyNewsow(numberOfCards);

    }


    public void UsunKartyNewsow()
    {

        for (var i = 0; i < currentNewsCard.Count; i++)
        {

            Destroy(currentNewsCard[i]);
        }


    }

    public void GenerujKartyNewsow(int numberOfCards)
    {
        // currentNewsCard = new GameObject[numberOfCards];
        currentNewsCard = new List<GameObject>(new GameObject[numberOfCards]);
        if (toPrint.Count >= 8)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                currentNewsCard[i] = (GameObject)Instantiate(newsCardPrefab, newsCardParent.transform.position, transform.rotation);
                currentNewsCard[i].transform.parent = newsCardParent.transform;
                currentNewsCard[i].GetComponent<Toggle>().group = cardNewsGroup;
                cardNewsGroup.SetAllTogglesOff();
                currentNewsCard[i].tag = "KartaNewsa";
                foreach (Transform t in currentNewsCard[i].transform)
                {

                    currentNewsCard[i].GetComponent<IDCard>().plusIndex = toPrint[i].partiaZysk.ToString();
                    currentNewsCard[i].GetComponent<IDCard>().minusIndex = toPrint[i].partiaStrata.ToString();
                    if (t.name == "Name")
                    {
                        t.Find("name").gameObject.GetComponent<Text>().text = toPrint[i].cardName;
                        currentNewsCard[i].GetComponent<IDCard>().name = toPrint[i].cardName;
                        t.Find("category").gameObject.GetComponent<Text>().text = toPrint[i].cardCategory;
                        currentNewsCard[i].GetComponent<IDCard>().category = toPrint[i].cardCategory;
                        t.GetComponent<Image>().color = GetNewsCardColor(toPrint[i].cardCategory);
                    }
                    else if (t.name == "1")
                    {

                        t.GetComponent<Image>().color = GetColor(toPrint[i].partiaZysk.ToString());
                        if (toPrint[i].zysk.ToString() != "0")
                            t.Find("plus").gameObject.GetComponent<Text>().text = toPrint[i].zysk.ToString();
                        else t.Find("plus").gameObject.GetComponent<Text>().text = "Rzut";
                        currentNewsCard[i].GetComponent<IDCard>().plus = int.Parse(toPrint[i].zysk.ToString());
                    }

                    else if (t.name == "2")
                    {

                        //Debug.Log(toPrint[i].partiaStrata.ToString());
                        if (toPrint[i].strata.ToString() != "0")
                            t.Find("minus").gameObject.GetComponent<Text>().text = toPrint[i].strata.ToString();
                        //  else if (toPrint[i].partiaStrata.ToString() == "x") t.GetComponent<Image>().sprite = GradientIMG;
                        else t.Find("minus").gameObject.GetComponent<Text>().text = "Rzut";


                        t.GetComponent<Image>().color = GetColor(toPrint[i].partiaStrata.ToString());
                        if (toPrint[i].partiaStrata.ToString().Contains("x1")) t.GetComponent<Image>().sprite = x1;
                        else if (toPrint[i].partiaStrata.ToString().Contains("x2")) t.GetComponent<Image>().sprite = x2;
                        else if (toPrint[i].partiaStrata.ToString().Contains("x3")) t.GetComponent<Image>().sprite = x3;
                        else if (toPrint[i].partiaStrata.ToString().Contains("x4")) t.GetComponent<Image>().sprite = x4;
                        //private Color c = new Color(1F, 1F, 1F, 0.5F);
                        // t.GetComponent<Image>().color



                        currentNewsCard[i].GetComponent<IDCard>().minus = int.Parse(toPrint[i].strata.ToString());
                    }
                    else if (t.name == "3")
                    {
                        if (toPrint[i].money.ToString() != "999")
                        {
                            t.Find("pieniadze").gameObject.GetComponent<Text>().text = toPrint[i].money.ToString();
                           
                        } else t.Find("pieniadze").gameObject.GetComponent<Text>().text = "+Rzut";
                        currentNewsCard[i].GetComponent<IDCard>().pieniadze = int.Parse(toPrint[i].money.ToString());
                    }
                    else if (t.name == "4")
                    {
                        t.Find("wiarygodnosc").gameObject.GetComponent<Text>().text = toPrint[i].wiarygodnosc.ToString();
                        currentNewsCard[i].GetComponent<IDCard>().wiarygodnosc = int.Parse(toPrint[i].wiarygodnosc.ToString());
                    }
                    else if (t.name == "Description")
                    {
                        t.gameObject.GetComponentInParent<Text>().text = toPrint[i].cardDescription;

                    }

                }

            }
            //wylaczanie wszystkich kart
        }
        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(14);
    }

    public void GenerujKartePytan()
    {



        fazaRedakcyjna = true;
      
        fazaNewsow=false;
        int i = UnityEngine.Random.Range(0, 100 - a);
        Destroy(currentQuestionCard);
        currentQuestionCard = (GameObject)Instantiate(questionCard, questionCardParent.transform.position+new Vector3(0,600,0), transform.rotation);
        currentQuestionCard.transform.parent = questionCardParent.transform;
        uimanagerPrefab.animacja.QuestionCardOn(currentQuestionCard);
        // if(questionDeck.questionDeck[i]) 
        currentQuestionCard.GetComponent<SetAnswer>().answer = questionDeck.questionDeck[i].answer;
        foreach (Transform t in currentQuestionCard.transform)
        {

            if (t.name == "question") t.Find("name").gameObject.GetComponent<Text>().text = questionDeck.questionDeck[i].question;
            else if (t.name == "answer1") t.Find("odpowiedz1").gameObject.GetComponent<Text>().text = questionDeck.questionDeck[i].firstAnswer;
            else if (t.name == "answer2") t.Find("odpowiedz2").gameObject.GetComponent<Text>().text = questionDeck.questionDeck[i].secondAnswer;
            else if (t.name == "answer3") t.Find("odpowiedz3").gameObject.GetComponent<Text>().text = questionDeck.questionDeck[i].thirdAnswer;

        }
        questionDeck.Remove(i);
        a++;
       if (playerUserQuestionOrder == 0) StartCoroutine(FazaRedakcyjna());
       else
        currentQuestionCard.SetActive(true);
    }
    /*

    public void SetOtherUserScoreLeftWithDice(string wartosc)
    {

      
            GameObject cardNews = globalNewsCard;
            int user_2 = 0;
        Debug.Log(uimanagerPrefab.activeUser + " ooraz " + dicePlus.GetComponent<Dice>().resultPlus);
        switch (cardNews.GetComponent<IDCard>().minusIndex)
            {
                case "n":
                    user_2 = 2;
                    break;
                case "b":
                    user_2 = 4;
                    break;
                case "r":
                    user_2 = 1;
                    break;
                case "z":
                    user_2 = 0;
                    break;
                case "c":
                    user_2 = 3;
                    break;
                default:
                    break;
            }

        //  IncreaseScore(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), cardNews.GetComponent<IDCard>().minus);
        //      Debug.Log(uimanagerPrefab.activeUser + " " + dicePlus.GetComponent<Dice>().resultPlus);

       if (CheckIfAllowed(user_2, dicePlus.GetComponent<Dice>().resultPlus, 1) && CheckIfAllowed(user_2, dicePlus.GetComponent<Dice>().resultPlus, 0))
        {
            int prevValueScore = int.Parse(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
            int actualvariablescore = prevValueScore - dicePlus.GetComponent<Dice>().resultPlus;
            StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
            TriangleInfo[user_2].GetComponent<ID>().poparcie = actualvariablescore; //czy to jest potrzebne?
            GameManager.instance.users.poziomPoparciaGraczy[user_2] = actualvariablescore;
            //AddPointsToUser(0, dicePlus.GetComponent<Dice>().resultPlus, uimanagerPrefab.activeUser);
       }
       else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(38);
    }
    */
  
    public int ReturnPlayer(GameObject cardNews, string index)
    {
        string text = "";
        int user = 0;
        if (index == "plus") text = cardNews.GetComponent<IDCard>().plusIndex;
        else if (index == "minus") text = cardNews.GetComponent<IDCard>().minusIndex;

        switch (text)
        {
            case "n":
                user = 2;
                break;
            case "b":
                user = 4;
                break;
            case "r":
                user = 1;
                break;
            case "z":
                user = 0;
                break;
            case "c":
                user = 3;
                break;
            default:
                break;
        }
        return user;
    }


    public void AddNewsCardToUserDB()
    {
        try
        {
            if (!EndOfGameFlag)
            {
                if (pdbupdate.actualvariable + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc <= 0)
                {
                    
                    Debug.Log(pdbupdate.actualvariable + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                    //uimanagerPrefab.dialogueManager.DisplaySpecificSentence(33);
                    uimanagerPrefab.WybierzKarteNewsaButton.GetComponent<Button>().interactable = false;
                    uimanagerPrefab.dialogueManager.DisplaySpecificSentenceWithAdditionalText(34, TriangleInfo[uimanagerPrefab.activeUser].GetComponent<ID>().kolor);
                    uimanagerPrefab.animacja.RestartButtonOn();
                    pdbupdate.IncreasePDB(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                }

                else
                {
                    if (CheckIfAllowed(uimanagerPrefab.activeUser, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject))
                    {
                        //uimanagerPrefab.dialogueManager.DisplaySpecificSentence(50);
                        if (cardNewsGroup.ActiveToggles().FirstOrDefault() != null)
                        {
                            //uimanagerPrefab.dialogueManager.Clear();
                            
                            int userIndex = uimanagerPrefab.activeUser;
                            if (TwojeNowinyButtonFlag && userIndex == userToRepeatWithPanel)
                            {
                                cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.transform.Find("TwojeNowiny").gameObject.SetActive(true);
                                TwojeNowinyButtonFlag = false;
                            }
                            if (leaveOneCardFlag && !TwojeNowinyButtonFlag)
                            {
                                TwojeNowinyButtonFlag = true;
                            }
                            if (IfUsePoints)
                            {
                                cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().UserIndexParent = uimanagerPrefab.activeUser;
                                if (!dice.GetComponent<Dice>().wartoscFlag) SetSelfUserScore(userIndex);
                                //jeden z warunkow kart celow - obnizanie punktacji za obniżanie PDB
                               // if (userIndex == 4) cardNewsGroup.ActiveToggles().FirstOrDefault().GetComponent<NegativeNewsCardsNr>().CheckNr();
                               // if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc < 0 && userIndex == 4) AddPointsOfVictory(-1, 4);
                                pdbupdate.IncreasePDB(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                                SetOtherUserScoreLeft(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                                SetOtherUserScoreRight(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                            }
                            currentNewsCard.Remove(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                            cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.tag = "KartaNewsaGracza";
                            playerCharactersWithGrey[userIndex].GetComponent<ID>().categoryCardScore[cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().category] += 1;
                            playerCharactersWithGrey[userIndex].GetComponent<ID>().userNewsDeck.Add(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                            cardNewsGroup.ActiveToggles().FirstOrDefault().transform.parent = playerCharactersWithGrey[userIndex].transform.GetComponent<ID>().KartNewsow.transform;
                            transform.localScale = transform.localScale;
                            if (diceflag) NextPlayer(true);
                            if (panelGraczaIndex == 2) panelGraczaIndex = 1;
                        }
                        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(12);
                        GameManager.instance.users.Suma4Graczy();

                    }
                    else
                    {
                        uimanagerPrefab.dialogueManager.DisplaySpecificSentence(41);
                        /*

                                                if (cardNewsGroup.ActiveToggles().FirstOrDefault() != null)
                                                {



                                                    uimanagerPrefab.dialogueManager.Clear();
                                                    int userIndex = uimanagerPrefab.activeUser;
                                                    if (TwojeNowinyButtonFlag && userIndex == userToRepeatWithPanel)
                                                    {
                                                        cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.transform.Find("TwojeNowiny").gameObject.SetActive(true);
                                                        TwojeNowinyButtonFlag = false;
                                                    }
                                                    if (leaveOneCardFlag && !TwojeNowinyButtonFlag)
                                                    {
                                                        TwojeNowinyButtonFlag = true;
                                                    }
                                                    if (IfUsePoints)
                                                    {
                                                        cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().UserIndexParent = uimanagerPrefab.activeUser;



                                                        switch (CheckIfAllowed(uimanagerPrefab.activeUser, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject, 0))
                                                        {
                                                            case 7:
                                                                uimanagerPrefab.dialogueManager.DisplaySpecificSentence(42);
                                                                Debug.Log("Do 0");
                                                                if (!dice.GetComponent<Dice>().wartoscFlag) SetSelfUserWiarygodnoscMin(userIndex);
                                                                break;
                                                            case 8:
                                                                uimanagerPrefab.dialogueManager.DisplaySpecificSentence(45);
                                                                if (!dice.GetComponent<Dice>().wartoscFlag) SetSelfUserWiarygonoscMax(userIndex);
                                                                break;
                                                            case 9:
                                                                uimanagerPrefab.dialogueManager.DisplaySpecificSentence(44);
                                                                SetSelfUserFinanseMin(userIndex);
                                                                break;
                                                            case 10:
                                                                uimanagerPrefab.dialogueManager.DisplaySpecificSentence(43);
                                                                SetSelfUserFinanseMax(userIndex);
                                                                break;
                                                            case 11:
                                                                SetSelfUserFinanseMinWiarygodnoscMin(userIndex);
                                                                break;
                                                            case 12:
                                                                SetSelfUserFinanseMaxWiarygodnoscMax(userIndex);
                                                                break;
                                                            case 13:
                                                                SetSelfUserFinanseMaxWiarygodnoscMin(userIndex);
                                                                break;
                                                            case 14:
                                                                SetSelfUserFinanseMinWiarygodnoscMax(userIndex);
                                                                break;
                                                            default:
                                                                break;
                                                        }

                                                        if (CheckIfAllowed(uimanagerPrefab.activeUser, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject, 0) != 15 && CheckIfAllowed(uimanagerPrefab.activeUser, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject, 0)  != 16) { 



                                                        pdbupdate.IncreasePDB(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                                                        SetOtherUserScoreLeft(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                                                        SetOtherUserScoreRight(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);

                                                        currentNewsCard.Remove(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                                                        cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.tag = "KartaNewsaGracza";
                                                        playerCharactersWithGrey[userIndex].GetComponent<ID>().categoryCardScore[cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().category] += 1;
                                                        playerCharactersWithGrey[userIndex].GetComponent<ID>().userNewsDeck.Add(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject);
                                                        cardNewsGroup.ActiveToggles().FirstOrDefault().transform.parent = playerCharactersWithGrey[userIndex].transform.GetComponent<ID>().KartNewsow.transform;
                                                        transform.localScale = transform.localScale;
                                                        if (diceflag) NextPlayer(true);
                                                        if (panelGraczaIndex == 2) panelGraczaIndex = 1;
                                                        }
                                                        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(39);
                                                    }
                                                    else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(12);
                                                    GameManager.instance.users.Suma4Graczy();
                                                }

                        */


                    }
                }
            }
        }
        catch
        {
            uimanagerPrefab.dialogueManager.DisplaySpecificSentence(12);
        }
    }



    /// <summary>
    /// Funkcje warunkowe do sprawdzenia, czy można przyznać punkty z danje karty
    /// </summary>
    /// <param name="IndexUser"></param>
    /// <param name="karta"></param>
    /// <returns></returns>

















    public bool CheckIfAllowed(int IndexUser, GameObject karta)
    {
        bool ifCardAllowed = true;
      /*
        Debug.Log(
                         (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] - cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc)
            + " oraz " + (GameManager.instance.users.poziomFinansowGraczy[IndexUser] - cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze)
            + " oraz " + (GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze)
            + " oraz " + (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc)
            + " oraz " + (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")))
            + " oraz " + (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""))));
            */
        int dolnaGranica;
        int gornaGranica;
        if (ReturnPlayer(karta, "plus") == 4)
        {
            gornaGranica = 64;
        }
        else
        {
            gornaGranica = 44;
        }

        if (ReturnPlayer(karta, "minus") == 4)
        {
            dolnaGranica = 15;
        }
        else
        {
            dolnaGranica = 5;
        }

        if ((GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc < 0)
    || (GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze < 0)
    || (GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze > 15)
    || (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc > 15)
    || (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) > gornaGranica)
    || (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < dolnaGranica))
        {
            ifCardAllowed = false;
            Debug.Log("NOT ALLOWED");
        }
        else ifCardAllowed = true;





        if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze == 999)
        {

            if ((GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc < 0)
          || (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc > 15)
          || (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < dolnaGranica)
         // || (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < dolnaGranica)
          )
            {
                ifCardAllowed = false;
                Debug.Log("999   "+ karta.GetComponent<IDCard>().plus +"     " + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")));
            }


            // czy false i true nie powinno być odwrotnie????????????????????????????? 
            else
            {
                ifCardAllowed = true;
                Debug.Log("9992");
            }
        }

        // Debug.Log(karta.GetComponent<IDCard>().plus +" z poparciem "+  int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) + " oraz " + karta.GetComponent<IDCard>().minus + " z poparciem " + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")));



        return ifCardAllowed;
    }
    public int CheckIfAllowed(int IndexUser, GameObject karta, int a)
    {
        int ifCardAllowedCode = 0;
        /*
                Debug.Log(
                                 (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] - cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc)
                    + " oraz " + (GameManager.instance.users.poziomFinansowGraczy[IndexUser] - cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze)
                    + " oraz " + (GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze)
                    + " oraz " + (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc)
                    + " oraz " + (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")))
                    + " oraz " + (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""))));
                    */
       // Debug.Log("HALO");
        int dolnaGranica;
        int gornaGranica;
        if (ReturnPlayer(karta, "plus") == 4)
        {
            gornaGranica = 64;
        }
        else
        {
            gornaGranica = 44;
        }

        if (ReturnPlayer(karta, "minus") == 4)
        {
            dolnaGranica = 15;
        }
        else
        {
            dolnaGranica = 5;
        }


       // if (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc < 0) ifCardAllowedCode = 1;
       // if (GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze < 0) ifCardAllowedCode = 2;
       // if (GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze > 15) ifCardAllowedCode = 3;
       // if (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc > 15) ifCardAllowedCode = 4;
       // if (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) > gornaGranica) ifCardAllowedCode = 5;
      //  if (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < dolnaGranica) ifCardAllowedCode = 6;
        
        int wiarygodnoscToSum = GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc;
        int finanseToSum = GameManager.instance.users.poziomFinansowGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze;
       
        if (wiarygodnoscToSum < 0 && finanseToSum <= 15 && finanseToSum >= 0) ifCardAllowedCode = 7;
        else if (wiarygodnoscToSum > 15 && finanseToSum <= 15 && finanseToSum >= 0) ifCardAllowedCode = 8;

        else if(finanseToSum < 0 && wiarygodnoscToSum <= 15 && wiarygodnoscToSum >= 0) ifCardAllowedCode = 9;
        else if(finanseToSum > 15 && wiarygodnoscToSum <= 15 && wiarygodnoscToSum >= 0) ifCardAllowedCode = 10;

        else if (wiarygodnoscToSum < 0 && finanseToSum < 0) ifCardAllowedCode = 11;
        else if (wiarygodnoscToSum >15 && finanseToSum > 15) ifCardAllowedCode = 12;

        else if (finanseToSum > 15 && wiarygodnoscToSum < 0) ifCardAllowedCode = 13;
        else if (finanseToSum < 0 && wiarygodnoscToSum > 15) ifCardAllowedCode = 14;
        else if (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) > gornaGranica) ifCardAllowedCode = 15;
        else if (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < dolnaGranica) ifCardAllowedCode = 16;

        if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze == 999)
        {

           if (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc < 0) ifCardAllowedCode = 1;
            if (GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc > 15) ifCardAllowedCode = 4;
            if (karta.GetComponent<IDCard>().plus + int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) > gornaGranica) ifCardAllowedCode = 5;
            if (karta.GetComponent<IDCard>().minus + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < dolnaGranica) ifCardAllowedCode = 6;


            // czy false i true nie powinno być odwrotnie????????????????????????????? 
            // else ifCardAllowed = true;
        }

        // Debug.Log(karta.GetComponent<IDCard>().plus +" z poparciem "+  int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) + " oraz " + karta.GetComponent<IDCard>().minus + " z poparciem " + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")));



        return ifCardAllowedCode;
    }
    public bool CheckIfAllowed(int IndexUser, int poparcie, int sign)
    {
        bool ifCardAllowed = true;
        int dolnaGranica;
        int gornaGranica;
        if(IndexUser == 4)
        {
            dolnaGranica = 15;
            gornaGranica = 64;
        }
        else
        {
            dolnaGranica = 5;
            gornaGranica = 44;
        }

        if (sign == 0) { 

        if ((GameManager.instance.users.poziomPoparciaGraczy[IndexUser] - poparcie) < dolnaGranica)
        {
            Debug.Log(GameManager.instance.users.poziomPoparciaGraczy[IndexUser] - poparcie + " index " + IndexUser);

            ifCardAllowed = false;

        }
        else ifCardAllowed = true;
        }//// int.Parse(TriangleInfo[IndexUser].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) zamiast poparcia by
        else if(sign ==1)
          if ((GameManager.instance.users.poziomPoparciaGraczy[IndexUser] + poparcie) >gornaGranica)
            {
                Debug.Log(GameManager.instance.users.poziomPoparciaGraczy[IndexUser] + poparcie + " index " + IndexUser);

                ifCardAllowed = false;

            }
            else ifCardAllowed = true;


        // Debug.Log(karta.GetComponent<IDCard>().plus +" z poparciem "+  int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) + " oraz " + karta.GetComponent<IDCard>().minus + " z poparciem " + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")));



        return ifCardAllowed;
    }
    public bool CheckIfAllowed(int IndexUser, int poparcie)
    {
        bool ifCardAllowed = true;

     

            if ((GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] - int.Parse(TriangleInfo[IndexUser].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) < 5)
            ||(GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + int.Parse(TriangleInfo[IndexUser].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) > 44))
            {
                Debug.Log(GameManager.instance.users.poziomWiarygodnosciGraczy[IndexUser] + poparcie);

                ifCardAllowed = false;
                uimanagerPrefab.dialogueManager.DisplaySpecificSentence(39);
        }
            else ifCardAllowed = true;


        // Debug.Log(karta.GetComponent<IDCard>().plus +" z poparciem "+  int.Parse(TriangleInfo[ReturnPlayer(karta, "plus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")) + " oraz " + karta.GetComponent<IDCard>().minus + " z poparciem " + int.Parse(TriangleInfo[ReturnPlayer(karta, "minus")].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")));



        return ifCardAllowed;
    }




    /*
  




   */

    public void SetSelfUserScore(int userIndex)
    {
        //Debug.Log(userIndex + "uzytkownik");
        //Debug.Log("Uzytkownik: " + userIndex);
        AddPointsToUser(cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze, userIndex);

    }

    public void SetSelfUserWiarygonoscMax(int userIndex)
    {
        //Debug.Log(userIndex + "uzytkownik");
        //Debug.Log("Uzytkownik: " + userIndex);
        // AddPointsToUser(15, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc, userIndex);


        int WiarygodnoscToMax = 15-GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex];
        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, -WiarygodnoscToMax);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] -= WiarygodnoscToMax;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] += cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }



    }
    public void SetSelfUserWiarygodnoscMin(int userIndex)
    {

        int WiarygodnoscToMin = GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();


        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, -WiarygodnoscToMin);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] -= WiarygodnoscToMin;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] += cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }
    }
    public void SetSelfUserFinanseMax(int userIndex)
    {
        //Debug.Log(userIndex + "uzytkownik");
        //Debug.Log("Uzytkownik: " + userIndex);
        // AddPointsToUser(15, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc, userIndex);


        int FinanseToMax = 15 - GameManager.instance.users.poziomFinansowGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] += cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, FinanseToMax);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] += FinanseToMax;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }


        }


    }
    public void SetSelfUserFinanseMin(int userIndex)
    {

        int FinanseToMin = GameManager.instance.users.poziomFinansowGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] += cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, -FinanseToMin);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] -= FinanseToMin;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }
        }
    public void SetSelfUserFinanseMaxWiarygodnoscMin(int userIndex)
    {
        int WiarygodnoToMin = GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex];
        int FinanseToMax =15- GameManager.instance.users.poziomFinansowGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item,-WiarygodnoToMin);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] -= WiarygodnoToMin;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, FinanseToMax);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex]+= FinanseToMax;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }
    }
    public void SetSelfUserFinanseMinWiarygodnoscMin(int userIndex)
    {

        int WiarygodnoscToMin = GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex];
        int FinanseToMin = GameManager.instance.users.poziomFinansowGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, WiarygodnoscToMin);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] -= WiarygodnoscToMin;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, -FinanseToMin);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] -= FinanseToMin;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }
    }
    public void SetSelfUserFinanseMaxWiarygodnoscMax(int userIndex)
    {

        int WiarygodnoscToMax =15- GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex];
        int FinanseToMin = GameManager.instance.users.poziomFinansowGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, WiarygodnoscToMax);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] += WiarygodnoscToMax;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, -FinanseToMin);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] -= FinanseToMin;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }
    }
    public void SetSelfUserFinanseMinWiarygodnoscMax(int userIndex)
    {

        int WiarygodnoscToMax = 15-GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex];
        int FinanseToMin = GameManager.instance.users.poziomFinansowGraczy[userIndex];

        Text[] txts = TriangleInfo[userIndex].GetComponentsInChildren<Text>();

        //displayScore = 0;
        foreach (var item in txts)
        {
            switch (item.name)
            {
                case "wiarygodnosc":

                    IncreaseScore(item, WiarygodnoscToMax);
                    GameManager.instance.users.poziomWiarygodnosciGraczy[userIndex] += WiarygodnoscToMax;
                    // item.text = "a";
                    break;
                case "finanse":
                    //   Debug.Log(wiarygodnosc + " oraz " + finanse + " index " + index);
                    if (cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze != 999)
                    {
                        IncreaseScore(item, -FinanseToMin);
                        GameManager.instance.users.poziomFinansowGraczy[userIndex] -= FinanseToMin;
                    }
                    //item.text = "B";
                    break;
                default:

                    break;
            }

        }
    }


    public void SetSelfUserScore(int userIndex_to, int sign)
    {
        Debug.Log(userIndex_to + "uzytkownik");
        //Debug.Log("Uzytkownik: " + userIndex);
        AddPointsToUser(-cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().wiarygodnosc, -cardNewsGroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<IDCard>().pieniadze, userIndex_to, 0);

    }

    /// <summary>
    /// Funkcja updetująca punkty początkowe
    /// </summary>
    /// <param name="minus"></param>
    /// <param name="minusIndex"></param>
   
   

    /// <summary>
    /// ////anulowanie karty
    /// </summary>
    /// <param name="cardNews"></param>
    /// <param name="sign"></param>
    public void SetOtherUserScoreLeft(GameObject cardNews, int sign)
    {
        string gradientFlag = "0";
        int user_2 = 0;
        globalNewsCard = cardNews;


        switch (cardNews.GetComponent<IDCard>().minusIndex)
        {
            case "n":
                user_2 = 2;
                break;
            case "b":
                user_2 = 4;
                break;
            case "r":
                user_2 = 3;
                break;
            case "z":
                user_2 = 0;
                break;
            case "c":
                user_2 = 1;
                break;
            case "x1":
                gradientFlag = "x1";
                break;
            case "x2":
                gradientFlag = "x2";
                break;
            case "x3":
                gradientFlag = "x3";
                break;
            case "x4":
                gradientFlag = "x4";
                break;
            default:

                break;
        }

        //  IncreaseScore(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), cardNews.GetComponent<IDCard>().minus);
        // Debug.Log(user_2 + " oraz " + cardNews.GetComponent<IDCard>().minusIndex);

        if (cardNews.GetComponent<IDCard>().minus.ToString() != "0" && !cardNews.GetComponent<IDCard>().minusIndex.Contains("x"))
        {
            int prevValueScore = int.Parse(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
            int actualvariablescore = MnoznikPunktow * (-cardNews.GetComponent<IDCard>().minus) * panelGraczaIndex + prevValueScore; //minus w nawiasie, czemu byłw 
            //   if (panelGraczaIndex == 2) panelGraczaIndex = 1;
            StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
            TriangleInfo[user_2].GetComponent<ID>().poparcie = actualvariablescore;
            GameManager.instance.users.poziomPoparciaGraczy[user_2] = actualvariablescore;
        }
        else if (!cardNews.GetComponent<IDCard>().minusIndex.Contains("x"))
        {
            uimanagerPrefab.animacja.DDiceOn();
            uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
            diceflag = false;
        }
        else
        {
            int i = 0;
            switch (gradientFlag)
            {

                case "x1":
                    GradientSum(1, 2, 3);
                    break;
                case "x2":
                    GradientSum(0, 1, 2);
                    break;
                case "x3":
                    GradientSum(0, 1, 3);
                    break;
                case "x4":
                    GradientSum(2, 0, 3);
                    break;

                default: break;

            }
        }
    }
    public void SetOtherUserScoreRight(GameObject cardNews, int sign)
    {
        int user = 0;
        globalNewsCard = cardNews;
        switch (cardNews.GetComponent<IDCard>().plusIndex)
        {
            case "n":
                user = 2;
                break;
            case "b":
                user = 4;
                break;
            case "r":
                user = 3;
                break;
            case "z":
                user = 0;
                break;
            case "c":
                user = 1;
                break;
            default:
                break;
        }

        if (cardNews.GetComponent<IDCard>().minus.ToString() != "0") //czy powinien byc plus czy minus
        {
            int prevValueScore = int.Parse(TriangleInfo[user].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
            int actualvariablescore = (-cardNews.GetComponent<IDCard>().plus) * panelGraczaIndex + prevValueScore;
            if (panelGraczaIndex == 2) panelGraczaIndex = 1;
            StartCoroutine(IncreaseScoreOverTime2(TriangleInfo[user].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
            TriangleInfo[user].GetComponent<ID>().poparcie = actualvariablescore;
            GameManager.instance.users.poziomPoparciaGraczy[user] = actualvariablescore;
        }
        else
        {
            uimanagerPrefab.animacja.DDiceOn();
            uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
        }
    }

    //
    /// <summary>
    /// funkcje do panelu gracza twoje nowiny
    /// </summary>
    /// <param name="plus"></param>
    /// <param name="plusIndex"></param>
    public void SetOtherUserScoreRight(int plus, string plusIndex)
    {
        int user = 0;
        switch (plusIndex)
        {
            case "n":
                user = 2;
                break;
            case "b":
                user = 4;
                break;
            case "c":
                user = 3;
                break;
            case "z":
                user = 0;
                break;
            case "r":
                user = 1;
                break;
            default:
                break;
        }

        if (plus.ToString() != "0") // czy powinien byc plus czy minus
        {
            int prevValueScore = int.Parse(TriangleInfo[user].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
            int actualvariablescore = MnoznikPunktow * plus * panelGraczaIndex + prevValueScore;
            if (panelGraczaIndex == 2) panelGraczaIndex = 1;
            StartCoroutine(IncreaseScoreOverTime2(TriangleInfo[user].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
            TriangleInfo[user].GetComponent<ID>().poparcie = actualvariablescore;
            GameManager.instance.users.poziomPoparciaGraczy[user] = actualvariablescore;
        }
        else
        {
            uimanagerPrefab.animacja.DDiceOn();

            uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
        }
    }
    public void SetOtherUserScoreLeft(int minus, string minusIndex)
    {
        string gradientFlag = "0";
        int user_2 = 0;



        switch (minusIndex)
        {
            case "n":
                user_2 = 2;
                break;
            case "b":
                user_2 = 4;
                break;
            case "c":
                user_2 = 3;
                break;
            case "z":
                user_2 = 0;
                break;
            case "r":
                user_2 = 1;
                break;
            case "x1":
                gradientFlag = "x1";
                break;
            case "x2":
                gradientFlag = "x2";
                break;
            case "x3":
                gradientFlag = "x3";
                break;
            case "x4":
                gradientFlag = "x4";
                break;
            default:

                break;
        }

        //  IncreaseScore(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), cardNews.GetComponent<IDCard>().minus);
        // Debug.Log(user_2 + " oraz " + cardNews.GetComponent<IDCard>().minusIndex);
        if (CheckIfAllowed(user_2, MnoznikPunktow * minus * panelGraczaIndex))
            if (minus.ToString() != "0" && !minusIndex.Contains("x"))
            {
                int prevValueScore = int.Parse(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
                int actualvariablescore = MnoznikPunktow * minus * panelGraczaIndex + prevValueScore;
                //   if (panelGraczaIndex == 2) panelGraczaIndex = 1;
                StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
                TriangleInfo[user_2].GetComponent<ID>().poparcie = actualvariablescore;
                GameManager.instance.users.poziomPoparciaGraczy[user_2] = actualvariablescore;
            }
            else if (minusIndex.Contains("x"))
            {
                uimanagerPrefab.animacja.DDiceOn();
                uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
                diceflag = false;
            }
            else
            {
                int i = 0;
                switch (gradientFlag)
                {

                    case "x1":
                        GradientSum(1, 2, 3);
                        break;
                    case "x2":
                        GradientSum(0, 1, 2);
                        break;
                    case "x3":
                        GradientSum(0, 1, 3);
                        break;
                    case "x4":
                        GradientSum(2, 0, 3);
                        break;

                    default: break;

                }
            }
        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(39);
    }
    //



    public void SetOtherUserScoreRightWithDice(string wartosc)
    {


        Debug.Log(uimanagerPrefab.activeUser + " ooraz " + dicePlus.GetComponent<Dice>().resultPlus);
        //czy funkcja z dice jest potrzebna, moze wystarczy bez dice
        GameObject cardNews = globalNewsCard;
        int user_2 = 0;

        switch (cardNews.GetComponent<IDCard>().plusIndex)
        {
            case "n":
                user_2 = 2;
                break;
            case "b":
                user_2 = 4;
                break;
            case "r":
                user_2 = 1;
                break;
            case "z":
                user_2 = 0;
                break;
            case "c":
                user_2 = 3;
                break;
            default:
                break;
        }
        if (CheckIfAllowed(user_2, dicePlus.GetComponent<Dice>().resultPlus, 1) && CheckIfAllowed(user_2, dicePlus.GetComponent<Dice>().resultPlus, 0))
        {


            //  IncreaseScore(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), cardNews.GetComponent<IDCard>().minus);
            // Debug.Log(user_2 + " oraz " + cardNews.GetComponent<IDCard>().minusIndex);

            if (wartosc == "poparcie")
            {
                int prevValueScore = int.Parse(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", "")); ///W TEJ LINII COS NIE TAK
                int actualvariablescore = prevValueScore + dicePlus.GetComponent<Dice>().resultPlus;
                StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
                //czemu overtime1 a nie overtime2 
                TriangleInfo[user_2].GetComponent<ID>().poparcie = actualvariablescore;
                GameManager.instance.users.poziomPoparciaGraczy[user_2] = actualvariablescore;
            }
            else
            {
                AddPointsToUser(0, dicePlus.GetComponent<Dice>().resultPlus, uimanagerPrefab.activeUser);
                // Debug.Log("halo");
            }

        }
        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(37);
    }

    public void SetOtherUserScoreBothWithDice(string wartosc)
    {


        GameObject cardNews = globalNewsCard;
        int user_minus = 0;
        int user_plus = 0;
        Debug.Log(uimanagerPrefab.activeUser + " ooraz " + dicePlus.GetComponent<Dice>().resultPlus);
        switch (cardNews.GetComponent<IDCard>().minusIndex)
        {
            case "n":
                user_minus = 2;
                break;
            case "b":
                user_minus = 4;
                break;
            case "r":
                user_minus = 1;
                break;
            case "z":
                user_minus = 0;
                break;
            case "c":
                user_minus = 3;
                break;
            default:
                break;
        }
        switch (cardNews.GetComponent<IDCard>().plusIndex)
        {
            case "n":
                user_plus = 2;
                break;
            case "b":
                user_plus = 4;
                break;
            case "r":
                user_plus = 1;
                break;
            case "z":
                user_plus = 0;
                break;
            case "c":
                user_plus = 3;
                break;
            default:
                break;
        }

        //  IncreaseScore(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), cardNews.GetComponent<IDCard>().minus);
        //      Debug.Log(uimanagerPrefab.activeUser + " " + dicePlus.GetComponent<Dice>().resultPlus);

        if (CheckIfAllowed(user_minus, dicePlus.GetComponent<Dice>().resultPlus, 1)
            && CheckIfAllowed(user_minus, dicePlus.GetComponent<Dice>().resultPlus, 0)
            && CheckIfAllowed(user_plus, dicePlus.GetComponent<Dice>().resultPlus, 1)
            && CheckIfAllowed(user_plus, dicePlus.GetComponent<Dice>().resultPlus, 0)
            )
        {
            if (wartosc == "poparcie")
            {
                int prevValueScore = int.Parse(TriangleInfo[user_minus].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
                int actualvariablescore = prevValueScore - MnoznikPunktow * dicePlus.GetComponent<Dice>().resultPlus;
                StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_minus].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
                TriangleInfo[user_minus].GetComponent<ID>().poparcie = actualvariablescore; //czy to jest potrzebne?
                GameManager.instance.users.poziomPoparciaGraczy[user_minus] = actualvariablescore;




                prevValueScore = int.Parse(TriangleInfo[user_plus].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
                actualvariablescore = prevValueScore + MnoznikPunktow * dicePlus.GetComponent<Dice>().resultPlus;
                StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_plus].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
                TriangleInfo[user_plus].GetComponent<ID>().poparcie = actualvariablescore; //czy to jest potrzebne?
                GameManager.instance.users.poziomPoparciaGraczy[user_plus] = actualvariablescore;
                //AddPointsToUser(0, dicePlus.GetComponent<Dice>().resultPlus, uimanagerPrefab.activeUser);

            }
            else
            {
                AddPointsToUser(0, dicePlus.GetComponent<Dice>().resultPlus, uimanagerPrefab.activeUser);
                // Debug.Log("halo");
            }
        }
        else uimanagerPrefab.dialogueManager.DisplaySpecificSentence(38);
    }

    public void GradientSum(int a, int b, int c)
    {
       // Debug.Log("gradient");
        int prevValueScoreInside;
        prevValueScoreInside = int.Parse(TriangleInfo[a].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
        int actualvariablescore = prevValueScoreInside + 2;
        TriangleInfo[a].transform.Find("triangle/poparcie").GetComponent<Text>().text = prevValueScoreInside.ToString();
        StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[a].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScoreInside, actualvariablescore));
        GameManager.instance.users.poziomPoparciaGraczy[a] = actualvariablescore;

        prevValueScoreInside = int.Parse(TriangleInfo[b].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
        actualvariablescore = prevValueScoreInside + 2;
        TriangleInfo[b].transform.Find("triangle/poparcie").GetComponent<Text>().text = prevValueScoreInside.ToString();
        StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[b].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScoreInside, actualvariablescore));
        GameManager.instance.users.poziomPoparciaGraczy[b] = actualvariablescore;

        prevValueScoreInside = int.Parse(TriangleInfo[c].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
        actualvariablescore = prevValueScoreInside + 2;
        TriangleInfo[b].transform.Find("triangle/poparcie").GetComponent<Text>().text = prevValueScoreInside.ToString();
        StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[c].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScoreInside, actualvariablescore));
        GameManager.instance.users.poziomPoparciaGraczy[c] = actualvariablescore;

    }


    public void SetOtherUserScoreRight(GameObject cardNews)
    {
        int user = 0;
        globalNewsCard = cardNews;
        switch (cardNews.GetComponent<IDCard>().plusIndex)
        {
            case "n":
                user = 2;
                break;
            case "b":
                user = 4;
                break;
            case "c":
                user = 3;
                break;
            case "z":
                user = 0;
                break;
            case "r":
                user = 1;
                break;
            default:
                break;
        }

        if (cardNews.GetComponent<IDCard>().plus.ToString() != "0") //czy powinien byc plus czy minus
        {
            int prevValueScore = int.Parse(TriangleInfo[user].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
            int actualvariablescore = MnoznikPunktow*cardNews.GetComponent<IDCard>().plus * panelGraczaIndex + prevValueScore;
            //Debug.Log("PLUSprev" + prevValueScore + "PLUSactual " + actualvariablescore);
            if (panelGraczaIndex == 2) panelGraczaIndex = 1;
            StartCoroutine(IncreaseScoreOverTime2(TriangleInfo[user].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
            TriangleInfo[user].GetComponent<ID>().poparcie = actualvariablescore;
            GameManager.instance.users.poziomPoparciaGraczy[user] = actualvariablescore;
           // Debug.Log("PLUSprevPO" + prevValueScore + "PLUSactualPO " + actualvariablescore);
        }
        else
        {
            dice.GetComponent<Dice>().wartoscFlag = false;
            uimanagerPrefab.animacja.DDiceOn();
            uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
        }

        if (cardNews.GetComponent<IDCard>().pieniadze.ToString() == "999") {
            dice.GetComponent<Dice>().wartoscFlag = true;
                    uimanagerPrefab.animacja.DDiceOn();
            userWithDice = user;
            diceflag = false;
            uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
                }
    }

    public void SetOtherUserScoreLeft(GameObject cardNews)
    {
        string gradientFlag = "0";
        int user_2 = 0;
        globalNewsCard = cardNews;


        switch (cardNews.GetComponent<IDCard>().minusIndex)
        {
            case "n":
                user_2 = 2;
                break;
            case "b":
                user_2 = 4;
                break;
            case "c":
                user_2 = 3;
                break;
            case "z":
                user_2 = 0;
                break;
            case "r":
                user_2 = 1;
                break;
            case "x1":
                gradientFlag = "x1";
                break;
            case "x2":
                gradientFlag = "x2";
                break;
            case "x3":
                gradientFlag = "x3";
                break;
            case "x4":
                gradientFlag = "x4";
                break;
            default:

                break;
        }

        //  IncreaseScore(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), cardNews.GetComponent<IDCard>().minus);
        // Debug.Log(user_2 + " oraz " + cardNews.GetComponent<IDCard>().minusIndex);

        if (cardNews.GetComponent<IDCard>().minus.ToString() != "0" && !cardNews.GetComponent<IDCard>().minusIndex.Contains("x"))
        {
            int prevValueScore = int.Parse(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>().text.Replace("%", ""));
            int actualvariablescore = MnoznikPunktow * cardNews.GetComponent<IDCard>().minus * panelGraczaIndex + prevValueScore;
            //Debug.Log("MINUSprev" + prevValueScore + " MINUSactual " + actualvariablescore);
            //   if (panelGraczaIndex == 2) panelGraczaIndex = 1;
            StartCoroutine(IncreaseScoreOverTime1(TriangleInfo[user_2].transform.Find("triangle/poparcie").GetComponent<Text>(), prevValueScore, actualvariablescore));
            TriangleInfo[user_2].GetComponent<ID>().poparcie = actualvariablescore;
            GameManager.instance.users.poziomPoparciaGraczy[user_2] = actualvariablescore;
            // Debug.Log("MINUSprevPO" + prevValueScore + " MINUS " + actualvariablescore);
        }
        else if (!cardNews.GetComponent<IDCard>().minusIndex.Contains("x"))
        {
            uimanagerPrefab.animacja.DDiceOn();
            uimanagerPrefab.WybierzKarteNewsaButton.interactable = false;
            diceflag = false;
        }
        else
        {

            int i = 0;
            switch (gradientFlag)
            {

                case "x1":
                    GradientSum(1, 2, 3);
                    break;
                case "x2":
                    GradientSum(0, 1, 2);
                    break;
                case "x3":
                    GradientSum(0, 1, 3);
                    break;
                case "x4":
                    GradientSum(2, 0, 3);
                    break;

                default: break;

            }
        }
    }



    IEnumerator IncreaseScoreOverTime1(Text item, int prevValueScore, int actualvariablescore)
    {
        float seconds = 1.0f;
        float animationTime = 0f;

        yield return new WaitForSeconds(1);
        while (animationTime < seconds)
        {
            item.fontSize = 28;
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            int score = (int)Mathf.Lerp(prevValueScore, actualvariablescore, lerpValue);
            item.text = score.ToString() + "%";
            item.fontSize = 20;
            yield return null;
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator IncreaseScoreOverTime2(Text item, int prevValueScore, int actualvariablescore)
    {
        float seconds = 1.0f;
        float animationTime = 0f;

        yield return new WaitForSeconds(1);
        while (animationTime < seconds)
        {
            item.fontSize = 28;
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            int score = (int)Mathf.Lerp(prevValueScore, actualvariablescore, lerpValue);
           
            item.fontSize = 20;
            item.text = score.ToString() + "%";

            yield return null;
        }

        yield return new WaitForSeconds(1);
    }

    public void SetToggleGroup(GameObject go)
    {

        Transform[] allChildren = go.GetComponentsInChildren<Transform>();
        //foreach (Transform child in allChildren)
        //{
        //child.GetComponent<Toggle>().group = go.GetComponent<ToggleGroup>();
        //}

    }

    public void GetActiveNews()
    {

        Debug.Log(cardNewsGroup.ActiveToggles().FirstOrDefault().name);

    }
    public Color32 GetColor(string partia)
    {
        Color32 kolor;
        if (partia.Contains("r"))
        {
            kolor = HueColourValue(PlayerColors.czerwony);

        }
        else if (partia.Contains("b"))
        {
            kolor = HueColourValue(PlayerColors.szary);

        }
        else if (partia.Contains("z"))
        {
            kolor = HueColourValue(PlayerColors.zielony);

        }
        else if (partia.Contains("c"))
        {
            kolor = HueColourValue(PlayerColors.czarny);

        }
        else if (partia.Contains("n"))
        {
            kolor = HueColourValue(PlayerColors.niebieski);

        }
        else if (partia.Contains("x"))
        {
            kolor = HueColourValue(PlayerColors.bialy);

        }
        else
        {
            kolor = HueColourValue(PlayerColors.none);

        }

        return kolor;
    }

    public Color32 GetNewsCardColor(string karta)
    {
        Color32 kolorCard;
        if (karta.Contains("Fake news"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.fakeNews);

        }
        else if (karta.Contains("Treść propagandowa"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.trescPropagandowa);

        }
        else if (karta.Contains("Treść tabloidowa"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.trescTabloidowa);

        }
        else if (karta.Contains("Zmanipulowana treść"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.zmanipulowanaTresc);

        }
        else if (karta.Contains("Rzetelne dziennikarstwo"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.rzetelneDziennikarstwo);

        }
        else if (karta.Contains("Pogłębiona analiza"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.poglebionaAnaliza);

        }
        else if (karta.Contains("Dziennikarstwo śledcze"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.dziennikarstwoSledcze);

        }
        else if (karta.Contains("Rzetelny reportaż"))
        {
            kolorCard = HueColourValueNewsCard(CardColors.rzetelnyReportaz);

        }
        else
        {
            kolorCard = HueColourValueNewsCard(CardColors.none);

        }

        return kolorCard;
    }
    public void TwojeNowinyButton()
    {
        Debug.Log("halo");
    }



    ///////////////////////////////////// KONIEC GRY
    public void PoliczPunkty()
    {

    }

}
