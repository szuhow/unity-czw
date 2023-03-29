using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;
using System.Globalization;
using System;
public class UIManager : MonoBehaviour
{
    public TweenAnimations animacja;
    public EnterPush enterPush;
    public GameObject Dalej_Button;
    public DialogueManager dialogueManager;
    public Dropdown chosenNumber;
    public Button resetButton;
    public GameObject DalejUser;
    public Button playButton;
    public Button addUserButton;
    public GameObject UserAgeInput;
    public Button dalejUserButton;
    public GameObject PaneleGraczaKarty;
    public GameObject TextGroup;
    public GameObject prefabText;
    public string userName;
    public string userAge;
    public string userDetailAge;
    public InputField ageMonthYearField;
    public InputField nameField;
    public InputField ageField;
    public GameObject[] AgeNameDisplay;
    public int flag=0;
    private int usersCount;
    private string[] userNames;
    public ToggleGroup PaneleGracza;
    public ToggleGroup KartyCelu;
    public int userCountColor = 0;
    private Toggle theActiveToggleSecond;
    private Toggle theActiveToggle;
    public Button nextUserButton;
    public Button previousUserButton;
    public int flagNextButton = 0;
    public List<GameObject> Children;
    public List<GameObject> turnList = new List<GameObject>();
    public List<GameObject> turnListReverse = new List<GameObject>();
    public GameObject[] playerCharacters;
    public List<int> turnList_Age = new List<int>();
    public RandomGenerator resetWykresow;// = new RandomGenerator();
    public GameObject prefabTextObject;
    public PopWindow popWindow;
    public bool resetFlag;
    public GenerujPunktacje metoda;// = new GenerujPunktacje();
    //public TypeWriter writ = new TypeWiter();
    public GameObject[] KartyCeluPrefab = new GameObject[5];
    public GameObject[] PaneleGraczaPrefab = new GameObject[5];
    public List<GameObject> PaneleGraczaList = new List<GameObject>();
    private GameObject currentTargetCard;
    public GameObject currentTargetPanel;
    public ToggleGroup[] listToggleGroup = new ToggleGroup[5];
   // public Toggle[] listTogglePaneli = new Toggle[5];
    public int flagaTrojkata;
    public GameObject parentPanel;
    public GameObject StartRoundButton;
    public RoundManager roundManager;
    public MainGameManager gameManager;
    public int currentPanelValue;
    public int activeUser;
    public Text currentUserIndicator;
    public int activeTriangle;
    public Button WybierzKarteNewsaButton;
    public int PDBScore;
    public Text scoreLog;
    // public GameObject PDBPanel;
    void Start()
    {
       
       // writ.Write("Witaj w grze \"Czwarta Władza\"");
        Menus(0,0);
        resetButton.onClick.AddListener(() => Reset(false));
        playButton.onClick.AddListener(() => Play());
        animacja.ExitButton.GetComponent<Button>().onClick.AddListener(() => Exit(flag));
        addUserButton.GetComponent<Button>().onClick.AddListener(() => StoreUser());
        chosenNumber.onValueChanged.AddListener(delegate { 
            Reset(true); 
            Dalej_Button.GetComponent<Button>().interactable = true;
            //roundManager.SetTourNumber(chosenNumber.value);
            //metoda.UkryjPanel();
        });

    }



    
    public void Menus(int i, int current)
    {
       // if (globalFlag) {
            //Debug.Log(i);
            switch (i)
            {

                case 0:
                    animacja.ResetButtonOff();
                    animacja.OnMainmenu();
                    animacja.SelectUserNumberOff(true);
                dialogueManager.Start();
                break;

                case 1:
                    animacja.ResetButtonOn();
                    animacja.SelectUserNumberOn();
                    animacja.OffMainMenu();
                    //animacja.OnInstructionArrow();
                    animacja.AddingUserOff();
                    dialogueManager.DisplaySpecificSentence(2);
                    Dalej_Button.GetComponent<Button>().interactable = true;
                    //dialogueManager.DisplayNextSentence("+");
                    Dalej_Button.GetComponent<Button>().onClick.RemoveAllListeners();
                    Dalej_Button.GetComponent<Button>().onClick.AddListener(() => FirstMenu());
                    break;


                case 2:
                    // addUserButton.GetComponent<Button>().interactable = true;

                    // if (GameObject.FindGameObjectsWithTag("TextUser").Length == Children.Count() && Children.Count() == chosenNumber.value+1)
                    if (GameObject.FindGameObjectsWithTag("TextUser").Length == chosenNumber.value + 3)

                    {
                        Dalej_Button.GetComponent<Button>().interactable = true;

                    }
                    else Dalej_Button.GetComponent<Button>().interactable = false;
                    // Debug.Log(GameObject.FindGameObjectsWithTag("TextUser").Length +" "+ Children.Count());
                    animacja.SelectUserNumberOff(false);
                    animacja.AddingUserOn();
                    animacja.PaneleGraczaKartyOff();
                    animacja.DalejButtonPolozenieNizej();
                    animacja.ExitButtonPolozenieNizej();                   
                    dialogueManager.DisplaySpecificSentence(3);
                    Dalej_Button.GetComponent<Button>().onClick.RemoveAllListeners();
                    //Dalej_Button.GetComponent<Button>().onClick.AddListener(() => DodanieUzytkownikow(flag));



                    break;

                case 3:

                    animacja.AddingUserOff();
                    animacja.PaneleGraczaKartyOn();
                    animacja.DalejButtonPolozenieWyzej();
                    animacja.KartyCeluOff();
                    animacja.ExitButtonPolozenieWyzej();
                    SetTogglesOnOff("KartaCelu", true);
                    Dalej_Button.GetComponent<Button>().onClick.RemoveAllListeners();
                    Dalej_Button.GetComponent<Button>().onClick.AddListener(() => WyborKart());
                    dialogueManager.DisplaySpecificSentence(4);


                    break;
                case 4:
                    Dalej_Button.GetComponent<Button>().onClick.RemoveAllListeners();
                    Dalej_Button.GetComponent<Button>().onClick.AddListener(() => WyborKart());
                Dalej_Button.GetComponent<Button>().interactable = true;
                // SetTogglesOnOff("KartaCelu", true);
                //SetTogglesInteractable("KartaCelu");
                animacja.PaneleGraczaKartyOff();
                    animacja.KartyCeluOn();
                    animacja.GeneratorOff();
                   if(GameManager.instance.users.KartyCelu.Contains(0)) dialogueManager.DisplaySpecificSentence(5);
                   else dialogueManager.DisplaySpecificSentence(11);
                // metoda.UkryjPanel();
                break;

                case 5:
                    // jak wchodzi przycisk "generuj", dezaktywuj przycisk dalej, żeby 
                    // nie przejsc dalej, zanim nie zaczeka sie na wyswietlenie wszystkich wynikow
                    Coloring();
                    // SetTogglesInteractable();
                    Dalej_Button.GetComponent<Button>().onClick.RemoveAllListeners();
                    Dalej_Button.GetComponent<Button>().onClick.AddListener(() => StartGame());
                    Dalej_Button.GetComponent<Button>().interactable = false;
                    animacja.PaneleGraczaKartyOff();
                    animacja.KartyCeluOff();
                    animacja.ResetButtonOff();
                    animacja.GeneratorOn();
                    animacja.UserIndicatorOff();
                    dialogueManager.DisplaySpecificSentence(10);
                // metoda.SchowajPanel();
                    animacja.lockedCard = false;
                    break;

                //animacja.GeneratorOff();

                case 6:
                //  metoda.PokazPanel();
                    dialogueManager.Clear();
                    animacja.GeneratorOff();
                    animacja.lockedCard = true;
                    animacja.OffSecondaryMenu();
                   // animacja.UserIndicatorOn();
                   // animacja.OnMainmenu();
                    gameManager.PlayGame();
                    break;

                default: break;


         
        }



    }

    
   public void ToggleUserNewsDeck()
    {


        if (true)
        {
            //  Debug.Log("a");
            int index;
            if (GameManager.instance.users.KartyCelu.IndexOf(activeUser+20) != -1)
            {
                index = GameManager.instance.users.PanelGracza[GameManager.instance.users.KartyCelu.IndexOf(activeUser + 20)];
                Debug.Log(index);
            }
            else index = -1;

            //Debug.Log("b" + index);
            if (index > -1)
            {

                currentTargetPanel = (GameObject)Instantiate(PaneleGraczaPrefab[index], parentPanel.transform.position, transform.rotation);
                currentTargetPanel.transform.parent = parentPanel.transform;
                currentTargetPanel.GetComponent<ID>().IfPanelUsable(GetActiveUserUsablePanel());
            }
            else
            {
                // Debug.Log("c");
                popWindow.ConfirmPopUp("UWAGA!", "Dla danego użytkownika nie wybrano panelu gracza!");
                foreach (ToggleGroup a in listToggleGroup)
                {
                    Toggle active = a.ActiveToggles().FirstOrDefault();
                    ToggleOff(active);

                }
            }
        }
        else
        {
         //   foreach (ToggleGroup a in listToggleGroup)
          //  {
          //      Toggle active = a.ActiveToggles().FirstOrDefault();
          //      ToggleOff(active);
          //  }


           // Destroy(currentTargetPanel);
        }





    }
    //FUNKCJA WYWOŁYWANA PRZEZ BUTTON PANEL GRACZA
      public void SetActiveTriangle(int a)
      {
        activeTriangle = a;
      }
    //FUNKCJA WYWOŁYWANA PRZEZ BUTTON KARTA NEWSOW
    public void SetNewsCardPanel(bool i)
    {

        ToggleNewsPanel(i);

    }
    public void ToggleNewsPanel(bool currentValue)
    {
        animacja.NewsCardPanelOnOffWithoutButton(!currentValue);
     }
    

    public void ToggleCard(bool currentValue)
    {
        if (gameManager.fazaRedakcyjna==false)
            animacja.NewsCardPanelOnOffWithoutButton(!currentValue);
        if (currentValue)
        {
            currentTargetCard = (GameObject)Instantiate(KartyCeluPrefab[flagaTrojkata], parentPanel.transform.position, transform.rotation);
            currentTargetCard.transform.parent = parentPanel.transform;
        }
        else
        {
            foreach (ToggleGroup a in listToggleGroup)
            {
                Toggle active = a.ActiveToggles().FirstOrDefault();
                ToggleOff(active);
            }
            Destroy(currentTargetCard);
        }
    }
    public void TogglePanel(bool currentValue)
    {
       if(gameManager.fazaRedakcyjna==false) 
            animacja.NewsCardPanelOnOffWithoutButton(!currentValue);
        if (currentValue)
        {
           
            //  Debug.Log("a");
            int index;
            if (GameManager.instance.users.KartyCelu.IndexOf(activeTriangle + 20)!=-1)
                 index = GameManager.instance.users.PanelGracza[GameManager.instance.users.KartyCelu.IndexOf(activeTriangle + 20)];
            else index = -1;
           // Debug.Log(GameManager.instance.users.KartyCelu.IndexOf(activeUser + 20));
            //Debug.Log("b" + index);
            if (index > -1)
            {
               
               currentTargetPanel = (GameObject)Instantiate(PaneleGraczaPrefab[index], parentPanel.transform.position, transform.rotation);
               currentTargetPanel.transform.parent = parentPanel.transform;
               currentTargetPanel.GetComponent<ID>().IfPanelUsable(GetActiveUserUsablePanel());
            }
            else
            {
               // Debug.Log("c");
                popWindow.ConfirmPopUp("UWAGA!", "Dla danego użytkownika nie wybrano panelu gracza!");
                foreach (ToggleGroup a in listToggleGroup)
                {
                    Toggle active = a.ActiveToggles().FirstOrDefault();
                    ToggleOff(active);
                
                }
            }
        }
        else
        {
            foreach (ToggleGroup a in listToggleGroup)
            {
                Toggle active = a.ActiveToggles().FirstOrDefault();
                ToggleOff(active);
            }


            Destroy(currentTargetPanel);
        }
    }

    //public void TogglePanel(bool currentValue)
    //{
    //    if (currentValue)
    //    {
    //        Debug.Log("a");
    //        int index = GameManager.instance.users.KartyCelu.IndexOf(flagaTrojkata + 21);
    //        if (index > -1)
    //        {
    //            Debug.Log("b" + index);
    //            currentTargetPanel = (GameObject)Instantiate(PaneleGraczaPrefab[GameManager.instance.users.KartyCelu.IndexOf(flagaTrojkata + 21)], parentPanel.transform.position, transform.rotation);
    //            currentTargetPanel.transform.parent = parentPanel.transform;
    //        }
    //        else
    //        {
    //            Debug.Log("c");
    //            popWindow.ConfirmPopUp("UWAGA!", "Dla danego użytkownika nie wybrano panelu gracza!");
    //            foreach (ToggleGroup a in listToggleGroup)
    //            {
    //                Toggle active = a.ActiveToggles().FirstOrDefault();
    //                ToggleOff(active);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (ToggleGroup a in listToggleGroup)
    //        {
    //            Toggle active = a.ActiveToggles().FirstOrDefault();
    //            ToggleOff(active);
    //        }


    //        Destroy(currentTargetPanel);
    //    }
    //}
    public void ToggleOff(Toggle toggle)
    {
        if(toggle!=null) toggle.isOn = false;
    }
    public bool GetActiveUserUsablePanel()
    {
        bool usablePanel = true;
        



        return usablePanel;

    }
    public void Play()

    {
        flag = 1;
        Menus(flag,0);
 
    }


    void FirstMenu()
    {
        flag = 2;
        Menus(flag, 0);
    }

   void StartGame(){
        Menus(6, 0);

    }

    public void GenerujPanel()
    {
       Vector3 velocity = Vector3.zero;
   // singlePointObject = (GameObject)Instantiate(SinglePrefabPoint, parentPanel.transform.position, transform.rotation);



        //singlePointObject.transform.position, Quaternion.identity);                                                                                                          
       // singlePointObject.transform.parent = parentPanel.transform;                                                                                                        
     //   singlePointObject.transform.position = Vector3.zero;                                                                                                           
       // Vector3 initPos = singlePointObject.transform.position;                                                                                                           
        // Vector3 newPos = new Vector3(0, 0, 0);                                                                                                          
        // singlePointObject.transform.position =  Vector3.Lerp(Vector3.zero, newPos, 0.3f);
        //for (int i = 0; i < 31; i++) {
        //    singlePointObject = (GameObject)Instantiate(Resources.Load("SinglePrefabPoint"));
        //    singlePointObject.transform.SetParent(parentPanel.transform);
        //    Vector3 initPos = singlePointObject.transform.position;
        //    Vector3 newPos = new Vector3(1, initPos.y, initPos.z);
        //    // float dirX = i * 30 * Time.deltaTime;
        //    //  Vector3 finalTranform = new Vector3(dirX, 0, 0);
        //    singlePointObject.transform.position= Vector3.Lerp(singlePointObject.transform.position, newPos, 50f *Time.deltaTime); ;


        //}




    }
    //zmienic dodawanie uzytkownikow - njpie wpisujemy dane, a potem klikamy dpodaj, przez to przycisk dodawania bedzie dodwawaj userow a nie przycisk dalej

    void WyborKart()
    {
        switch (flagNextButton)
        {
            case 0:
                //FAZA WYBORU PANELU GRACZA

                SetTogglesOnOff("Panel", true);
                if (userCountColor < Children.Count)
                {
                   //dodac do dialog system info o tytm, że nalezy wybrac panele gracza, aby przejsc dalej
                                
                    GetActiveToggle(PaneleGracza);
                    if (PaneleGracza.AnyTogglesOn())
                    {

                       // currentPanelValue = theActiveToggle.GetComponent<ID>().PanelGracza;
                        dialogueManager.DisplaySpecificSentence(4);
                        GameManager.instance.users.PanelGracza[turnList[userCountColor].GetComponent<ID>().Identify] = theActiveToggle.GetComponent<ID>().PanelGracza;
                        turnList[userCountColor].GetComponent<ID>().PanelGracza = theActiveToggle.GetComponent<ID>().PanelGracza;
                        SetToggles(PaneleGracza, false);
                       // Debug.Log("teraz");
                        theActiveToggle.interactable = false;
                        // theActiveToggle.GetComponent<Toggle>().disabledColor = Color.red;
                        turnList[userCountColor].GetComponent<Text>().color = Color.white;
                        if (userCountColor < Children.Count - 1)
                        {
                            userCountColor++;
                            turnList[userCountColor].GetComponent<Text>().color = Color.red; // blad przy powtornym wybraniu czegos
                          
                        }
                        else
                        {
                          
                            flag = 4;
                            Menus(flag, 0);
                            userCountColor = 0;
                            // SetTogglesInteractableSelected
                            SetTogglesOnOff("KartaCelu", true);
                            flagNextButton = 1;
                            turnListReverse[userCountColor].GetComponent<Text>().color = Color.red;
                        }
                       
                    }
                    else if (flagNextButton == 0) dialogueManager.DisplaySpecificSentence(8);
                    // Debug.Log("po blokadzie");
                    // Menus(flag, 0);

                }
                else {
                    SetTogglesOnOff("Panel", false);
                    SetTogglesOnOff("KartaCelu", false);
                    
                    flag = 4; 
                    Menus(flag, 0); }

                break;

            

            case 1:
                //FAZA WYBORU KARTY CELU


                    GetActiveToggle(KartyCelu);
               if (KartyCelu.AnyTogglesOn())
                    {
                    //GameManager.instance.users.KartyCelu.IndexOf(flagaTrojkata + 21);
                        //gameManager.playerCharacters[flagaTrojkata].GetComponent<ID>().PanelGracza = currentPanelValue;
                        GameManager.instance.users.KartyCelu[turnListReverse[userCountColor].GetComponent<ID>().Identify] = theActiveToggleSecond.GetComponent<ID>().KartaCelu;
                        turnListReverse[userCountColor].GetComponent<ID>().KartaCelu = theActiveToggleSecond.GetComponent<ID>().KartaCelu;
                        turnListReverse[userCountColor].GetComponent<ID>().color = theActiveToggleSecond.GetComponent<ID>().color;
                        SetToggles(KartyCelu, false);
                        theActiveToggleSecond.interactable = false;
                   
                        //theActiveToggleSecond.gameObject.SetActive(false);
                        if (userCountColor < Children.Count - 1)
                        {
                        dialogueManager.DisplaySpecificSentence(5);
                        turnListReverse[userCountColor].GetComponent<Text>().color = Color.white;
                            userCountColor++;

                            turnListReverse[userCountColor].GetComponent<Text>().color = Color.red;
                        }
                        else
                        {
                            // flag++;
                            nextUserButton.interactable = false;
                            turnListReverse[userCountColor].GetComponent<Text>().color = Color.white;
                        if (!GameManager.instance.users.KartyCelu.Contains(24))
                        {
                            dialogueManager.DisplaySpecificSentence(6);
                            // popWindow.ConfirmPopUp("UWAGA!", "Nie wybrano karty bezstronnego dziennikarza!");
                            userCountColor = 0;
                            Reset(false);
                            nextUserButton.interactable = true;
                            //  GameManager.instance.users.KartyCelu.Clear();

                            turnListReverse[userCountColor].GetComponent<Text>().color = Color.red;
                            SetTogglesInteractable("KartaCelu");
                        }
                       
                            
                       
                        
                        else if (!GameManager.instance.users.KartyCelu.Contains(0))
                        {
                            //dialogueManager.DisplaySpecificSentence(1);
                            //  Menus(++flag, 0);
                            flag = 5;
                            Menus(flag, 0);
                            //userCountColor = 0;
                            //  Menus(flag, 0);
                            // flagNextButton = 2;                           
                            //WyborKart();
                        }
                        //foreach (GameObject player in gameManager.playerCharacters)
                        //{
                        //    Debug.Log(player.name);
                        //    if (GameManager.instance.users.KartyCelu.Contains(player.GetComponent<ID>().KartaCelu))
                        //    {
                        //        Debug.Log(GameManager.instance.users.KartyCelu.IndexOf(player.GetComponent<ID>().KartaCelu));
                        //        player.GetComponent<ID>().PanelGracza = GameManager.instance.users.PanelGracza[GameManager.instance.users.KartyCelu.IndexOf(player.GetComponent<ID>().KartaCelu)];

                        //    }


                        //}

                        // }
                        
                    }
                         
                }
               

                // else if(!GameManager.instance.users.KartyCelu.Contains(0))
                // {
                //   SetTogglesOnOff("KartaCelu", false);
                // Debug.Log("po blokadzie 2"); 
                // flag = 5;
                //    Menus(++flag, 0);
                // }

                else if (flag==3)
                {
                    if (!GameManager.instance.users.KartyCelu.Contains(0)) SetTogglesOnOff("KartaCelu", false);
                    Menus(++flag, 0);
                }
                else if (flag == 4 && !GameManager.instance.users.KartyCelu.Contains(0))
                {
                    SetTogglesOnOff("KartaCelu", false);
                    Menus(++flag, 0);
                }
              //  else if(GameManager.instance.users.KartyCelu.Count==3) dialogueManager.DisplaySpecificSentence(10);
                else dialogueManager.DisplaySpecificSentence(9);




                break;






           // case 2:
               // Menus(flag, 0);
                // Debug.Log("GENERATOR");
                // PokazKartyCelu.SetBool("isHidden", true);
                // Generator.SetBool("isHidden", false);
                // popWindow.ConfirmPopUp("UWAGA!", "Nie wybrano karty bezstronnego dziennikarza!");
          
        }



      //  Menus(flag, 0);


    }

    public void Reset(bool reset)
    {
        
       
        bool[] localFlag = new bool[5];
        // reset uzytkownikow i wieku
        if (flag == 2 || reset)
        {
            
            GameManager.instance.users.PanelGracza.Clear();
            GameManager.instance.users.KartyCelu.Clear();
            GameManager.instance.users.userAge.Clear();
            GameManager.instance.users.userName.Clear();
            //Dalej_Button.GetComponent<Button>().interactable = false;
            foreach (GameObject userInfo in AgeNameDisplay)
            {
                userInfo.GetComponentInChildren<InputField>().text = "";
            }
            usersCount = 0;
            flagNextButton = 0;
            addUserButton.interactable = true;
            Dalej_Button.GetComponent<Button>().interactable = false;
            GameObject[] whatever = GameObject.FindGameObjectsWithTag("TextUser");
            Children.Clear();
            turnList.Clear();
            turnListReverse.Clear();
            SetTogglesOnOff("Panel", true); //?;
            SetTogglesInteractable("Panel");
            SetTogglesInteractable("KartaCelu");
            foreach (GameObject enemy in whatever)
            {
                Destroy(enemy.gameObject);

            }
           

        }
        else if ((flag == 3 && GameManager.instance.users.PanelGracza.Count > 0) || reset)
        {
            SetTogglesOnOff("Panel", true);
            SetTogglesOnOff("KartaCelu", true);
            //GameManager.instance.users.PanelGracza.Clear();
            SetTogglesInteractable("Panel");
            SetTogglesInteractable("KartaCelu");
            flagNextButton = 0;
            userCountColor = 0;
            Dalej_Button.GetComponent<Button>().interactable = true;
            GameManager.instance.users.PanelGracza.Clear();
            GameManager.instance.users.KartyCelu.Clear();
            for (int i = 0; i < Children.Count(); i++) {
                GameManager.instance.users.PanelGracza.Add(0);
                GameManager.instance.users.KartyCelu.Add(0);
            }

            //ponowne zaznaczenie, który użytkownik wybiera
            GameObject[] users = GameObject.FindGameObjectsWithTag("TextUser");
            foreach (GameObject Character in users)
            {
                Character.GetComponent<Text>().color = Color.white;
            }
            turnList[userCountColor].GetComponent<Text>().color = Color.red;
            localFlag[2] = true;
        }
        else if (flag == 4 || reset || localFlag[2])
        {
            Dalej_Button.GetComponent<Button>().interactable = true;
            userCountColor = 0;
            GameManager.instance.users.KartyCelu.Clear();
            for (int i = 0; i < Children.Count(); i++)
            {
                GameManager.instance.users.KartyCelu.Add(0);
            }

            GameObject[] users = GameObject.FindGameObjectsWithTag("TextUser");
            foreach (GameObject Character in users)
            {
                Character.GetComponent<Text>().color = Color.white;
            }
            turnListReverse[userCountColor].GetComponent<Text>().color = Color.red;

            SetTogglesOnOff("KartaCelu", true);         
            SetTogglesInteractable("KartaCelu");
            localFlag[2] = false;
            localFlag[3] = true;
        }
        else if (flag == 5 || reset || localFlag[3])
        {
            
            resetWykresow.ResetGenerator();
            localFlag[3] = false;
           

        }

    }



    //public void SetNumber()

    //{
    //    number.text = (System.Convert.ToInt32(number) + 1).ToString();

    //    if (System.Convert.ToInt32(number) <= chosenNumber.value)
    //    {
    //        right.interactable = true;

    //    }

    //    else
    //    {
    //        right.interactable = false;

    //    }


    //  }

    public void Exit(int currentState)
    {
       //dialogueManager.DisplayNextSentence("-");
        // flag;
        Menus(--flag,flag+1);
        

        if (currentState==1)
        {
        //    animacja.SelectUserNumberOff(true);
        }  
        else if (currentState == 2 || currentState == 3)
        {
            
            //    GameManager.instance.users.userAge.Clear();
            //    GameManager.instance.users.userName.Clear();

            //    foreach (GameObject userInfo in AgeNameDisplay)
            //    {
            //        userInfo.GetComponentInChildren<InputField>().text = "";
            //    }
            //    usersCount = 0;
            //    addUserButton.interactable = true;
            //    GameObject[] whatever = GameObject.FindGameObjectsWithTag("TextUser");
            //    foreach (GameObject enemy in whatever)
            //    {
            //        Destroy(enemy.gameObject);
            //  Dalej_Button.GetComponent<Button>().interactable = true;
        }

        //    }
        else if (currentState == 4)
        {
           // SetTogglesOnOff("Panel", false);

          


            // SetTogglesInteractablePanele();
        }
        else if (currentState == 5)
        {
           // SetTogglesOnOff("KartaCelu", false);




            // SetTogglesInteractablePanele();
        }

        //userCountColor = 0;
        //flagNextButton = 0;

    }



   public static void Coloring()
    {
        /* funkcja kolorujaca nazwy uzytkownikow
         *  i przyporzadkowujaca im punktacje z wykresow kolowych
         */

        GameObject[] users = GameObject.FindGameObjectsWithTag("TextUser");
        foreach (GameObject Character in users)
        {
            Character.GetComponent<Text>().color =  Character.GetComponent<ID>().color;
        }

    }


    void CheckIfUsersAreAdded() 
    {
       // Debug.Log(TextGroup.transform.childCount +" "+chosenNumber.value);
        if (TextGroup.transform.childCount < chosenNumber.value+3)
        {
            Dalej_Button.GetComponent<Button>().interactable = false;
        
        }
        else Dalej_Button.GetComponent<Button>().interactable = true;

    }

    void GetActiveToggle(ToggleGroup group)
    {
        // get first active toggle (and actually there should be only one in a group)
        foreach (var item in group.ActiveToggles())
        {

            theActiveToggle = item;
            theActiveToggleSecond = item;
            break;
        }
    }
   void SetToggles(ToggleGroup group, bool flag)
    {
        // get first active toggle (and actually there should be only one in a group)
        foreach (var item in group.ActiveToggles())
        {
            
            item.isOn = flag;
            
        }
    }



    void SetTogglesInteractable(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        // get first active toggle (and actually there should be only one in a group)
        foreach (GameObject item in objs)
        {
            item.GetComponent<Toggle>().interactable = true;
            //   item.interactable = flag;

        }
    }

  
     void SetTogglesOnOff(string tag, bool setting)
    {
       // Debug.Log("Wylaczono" + tag);
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        // get first active toggle (and actually there should be only one in a group)
        foreach (GameObject item in objs)
        {
            if(item.GetComponent<Toggle>().interactable == true) item.GetComponent<Toggle>().enabled = setting;
            //   item.interactable = flag;

        }
    }


    public void ReverseTurnOrder()
    {
        turnListReverse = new List<GameObject>(turnList);
        turnListReverse.Reverse();

    }
   public void turnOrder()
    {
        // string[] formats = {"M/d/yyyy", "M/d/yyyy",
        //                  "MM/dd/yyyy", "M/d/yyyy",
        //                  "M/d/yyyy", "M/d/yyyy",
        //                  "M/d/yyyy", "M/d/yyyy",
        //                  "MM/dd/yyyy", "M/dd/yyyy",
        //                  "MM/d/yyyy" };

         string[] formats = {"d/M/yyyy",
                         "dd/MM/yyyy",
                         "dd/MM/yyyy",
                         "d/MM/yyyy" };

        playerCharacters = GameObject.FindGameObjectsWithTag("TextUser");
        foreach (GameObject Character in playerCharacters)
        {

            //ID charTracker = Character.GetComponent<ID>();
            try
            {
                turnList = playerCharacters.OrderBy(x => System.DateTime.ParseExact(x.GetComponent<ID>().WiekSzczegolowy, formats, new CultureInfo("en-US"), DateTimeStyles.None)).ToList();
            }
            catch (FormatException e)
            {
                Debug.Log("zly format");

            }
        }

        turnList.Reverse();

        //playerCharacters = GameObject.FindGameObjectsWithTag("TextUser");
        //foreach (GameObject Character in playerCharacters)
        //{
        //    //ID charTracker = Character.GetComponent<ID>();

        //    turnList = playerCharacters.OrderBy(x => x.GetComponent<ID>().Wiek).ToList();
        //}


        ReverseTurnOrder();
    }
    //public void PreviousButton()
    // {

    // if (userCountColor>0)
    // {
    //     Children[userCountColor].GetComponent<Text>().color = Color.white;
    //     userCountColor--;
    //     Children[userCountColor].GetComponent<Text>().color = Color.red;

    // }
    // }
    void DodanieUzytkownikow(int currentState)
    {


        flag = 3;
        Menus(flag, 0);
        if (GameManager.instance.users.PanelGracza.Count()<= GameManager.instance.users.UserName.Count()) {
        foreach (Transform child in TextGroup.transform)
        {
            //Debug.Log("raz");
            Children.Add(child.gameObject);
            GameManager.instance.users.PanelGracza.Add(0);
            GameManager.instance.users.KartyCelu.Add(0);
        }

        turnOrder();
        ReverseTurnOrder();
        turnList[0].GetComponent<Text>().color = Color.red;

        }

    }

    public void StoreUser()
    {


        flag = 2;
        userName = nameField.text;
     //   userAge = ageField.text;
        userDetailAge = ageMonthYearField.text;
        System.DateTime dateValue;
        if (System.DateTime.TryParseExact(userDetailAge, "d/M/yyyy", new CultureInfo("pl-PL"), DateTimeStyles.None, out dateValue)==false || nameField.text=="")
        {
            if (System.DateTime.TryParseExact(userDetailAge, "d/M/yyyy", new CultureInfo("pl-PL"), DateTimeStyles.None, out dateValue) == false) popWindow.ConfirmPopUp("UWAGA", "Zła wartość wieku!");
            else if (nameField.text == "") popWindow.ConfirmPopUp("UWAGA", "Nie wpisano nazwy gracza!");
        }
        else
        {
           // enterPush.Clear();

            // Debug.Log("dodanie");
            // GameManager.instance.users.userAge.Add(userAge.ToUpper());
            GameManager.instance.users.userName.Add(userName.ToUpper());
        GameManager.instance.users._dates.Add(userDetailAge);
        prefabTextObject = (GameObject)Instantiate(prefabText,new Vector3(50, 0, 0), transform.rotation);
        prefabTextObject.transform.parent = TextGroup.transform;
        Text theText = prefabTextObject.transform.GetComponent<Text>();
            theText.text = GameManager.instance.users.userName[usersCount] + "\n";// "  Wiek: " + GameManager.instance.users.userAge[usersCount] + "\n"; usunieto "Imię: " na początku
        theText.GetComponent<ID>().Identify = usersCount;
        theText.GetComponent<ID>().Name = GameManager.instance.users.userName[usersCount];
       // theText.GetComponent<ID>().Wiek = int.Parse(userAge);
        theText.GetComponent<ID>().WiekSzczegolowy = userDetailAge;
        Interactable(usersCount);
        usersCount++;
        }
        CheckIfUsersAreAdded();
    }
   public void Interactable(int uC)
    {
        if(System.Int32.Parse(chosenNumber.options[chosenNumber.value].text) == uC+1) //? czemu plus 1/3
         {

            DodanieUzytkownikow(flag);
           //addUserButton.interactable = false;
            // dalejUserButton.interactable = true;
         }
    }


}

