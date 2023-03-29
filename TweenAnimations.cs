using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenAnimations : MonoBehaviour
{
    public GameObject logo;
    public GameObject CurrentUserIndicatorGameObject;
    public GameObject playButton2;
    public bool[] flag = new bool[5];
    public GameObject randomGenerator;
    public GameObject DalejUser;
    public GameObject resetButton;
    public GameObject ResetPDBButton;
    public GameObject UserAgeInput;
    public GameObject liczbaUzytkownikowDropdown;
    public GameObject Dalej_Button;
    public GameObject ExitButton;
    public GameObject PaneleGraczaKarty;
    public GameObject instructionButton;
    public GameObject kart;
    public GameObject KartyCeluObiekt;
    public GameObject PDB;
    public GameObject LewaGorna;
    public GameObject PrawaGorna;
    public GameObject LewaDolna;
    public GameObject PrawaDolna;
    public GameObject Srodkowa;
    public GameObject UITura;
    public UIManager uiManager;
    public GameObject dice;
    public GameObject doubleDice;
    public GameObject StartRoundButton;
    public GameObject newsCardPanel;
    public GameObject newsCardPanelWithoutButton;
    public GameObject cancelButton;
    public bool lockedCard = false;
    public GameObject RankingChartsObject;
    public GameObject SettingsDialog;
    public GameObject panelRankings;
    public GameObject InstructionButton;
    public GameObject NoChosenCardButton;
    public GameObject FazaRedakcyjnaText;
    public GameObject KolejnaTuraText;
    public GameObject PanelFazaRed;
    public GameObject PanelTury;
    public GameObject restartButton;
    // Start is called before the first frame update



    public void Start()
    {
        //Debug.Log(flag.Length);
        for (int j= 0;j < flag.Length; j++)
        {
          
            flag[j] = true;

        }

    }
    public void RestartButtonOn()
    {
        LeanTween.moveLocalY(restartButton, 600, 1).setEaseInOutCubic();

    }
    public void RestartButtonOff()
    {
        LeanTween.moveLocalY(restartButton, 0, 1).setEaseInOutCubic();

    }


    public void DiceOn()
    {
        LeanTween.moveLocalY(dice, -12.9f, 1).setEaseInOutCubic();
        
    }

    public void DiceOff()
    {
        LeanTween.moveLocalY(dice, 15, 1).setEaseInOutCubic();
    }
    public void DDiceOn()
    {
        LeanTween.moveLocalY(doubleDice, 125, 1).setEaseInOutCubic();
        dice.GetComponent<Dice>().ResetDice();
    }

    public void DDiceOff()
    {
        LeanTween.moveLocalY(doubleDice, 500, 1).setEaseInOutCubic();
    }

    public void UITuraON()
    {
        LeanTween.moveLocalY(UITura,-28 , 1).setEaseInOutCubic();
        
    }
    public void UITuraOFF()
    {
        LeanTween.moveLocalY(UITura,14 , 1).setEaseInOutCubic();
    }

    public void LogoOn()
    {

        LeanTween.moveLocal(logo, new Vector3(0, 180, 0), 0.5f).setEaseInOutCubic();
    }
    public void LogoOff()
    {

        LeanTween.moveLocal(logo, new Vector3(0, 580, 0), 0.5f).setEaseInOutCubic();
    }


    public void CancelButtonOn()
    {

        LeanTween.moveLocal(cancelButton, new Vector3(0, -130, 0), 0.5f).setEaseInOutCubic();
    }
    public void CancelButtonOff()
    {

        LeanTween.moveLocal(cancelButton, new Vector3(0, -470, 0), 0.5f).setEaseInOutCubic();
    }

    public void OnMainmenu()
    {
        LeanTween.moveLocal(logo, new Vector3(0, 180, 0), 1).setEaseInOutCubic();
        LeanTween.moveLocalX(playButton2, 0, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(instructionButton, 0, 1).setEaseInOutCubic();
        //LeanTween.moveLocalX(NoChosenCardButton, 0, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(ExitButton, -750, 1).setEaseInOutCubic();
    }

    public void FazaRedakcyjna()
    {
        PanelFazaRed.SetActive(true);
         StartCoroutine(FadeInCR());
        // FazaRedakcyjnaText.GetComponent<Text>().CrossFadeAlpha(1, 0.05f, false);
       
    }
    // IEnumerator FazaRedakcyjnaCoroutine()
    // {
    private IEnumerator FadeInCR()
    {
        float duration = 0.5f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            FazaRedakcyjnaText.GetComponent<Text>().color = new Color(FazaRedakcyjnaText.GetComponent<Text>().color.r, FazaRedakcyjnaText.GetComponent<Text>().color.g, FazaRedakcyjnaText.GetComponent<Text>().color.b, alpha);
            PanelFazaRed.GetComponent<Image>().color = new Color(PanelFazaRed.GetComponent<Image>().color.r, PanelFazaRed.GetComponent<Image>().color.g, PanelFazaRed.GetComponent<Image>().color.b, alpha);

            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            FazaRedakcyjnaText.GetComponent<Text>().color = new Color(FazaRedakcyjnaText.GetComponent<Text>().color.r, FazaRedakcyjnaText.GetComponent<Text>().color.g, FazaRedakcyjnaText.GetComponent<Text>().color.b, alpha);
            PanelFazaRed.GetComponent<Image>().color = new Color(PanelFazaRed.GetComponent<Image>().color.r, PanelFazaRed.GetComponent<Image>().color.g, PanelFazaRed.GetComponent<Image>().color.b, alpha);

            currentTime += Time.deltaTime;
           
            yield return null;
            
        }
        PanelFazaRed.SetActive(false);
        yield break;
     
    }



    public void KolejnaTura()
    {

        PanelTury.SetActive(true);
        KolejnaTuraText.GetComponent<Text>().text = "Rozpoczęcie tury " + UITura.transform.parent.GetComponent<RoundManager>().obecnaTura;
        StartCoroutine(KolejnaTuraCoroutine());

        // FazaRedakcyjnaText.GetComponent<Text>().CrossFadeAlpha(1, 0.05f, false);
    }

    public void KoniecGry()
    {

        PanelTury.SetActive(true);
        KolejnaTuraText.GetComponent<Text>().text = "Koniec gry";
        StartCoroutine(KolejnaTuraCoroutine());

        // FazaRedakcyjnaText.GetComponent<Text>().CrossFadeAlpha(1, 0.05f, false);
    }

    // IEnumerator FazaRedakcyjnaCoroutine()
    // {
    private IEnumerator KolejnaTuraCoroutine()
    {
        float duration = 0.5f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            KolejnaTuraText.GetComponent<Text>().color = new Color(KolejnaTuraText.GetComponent<Text>().color.r, KolejnaTuraText.GetComponent<Text>().color.g, KolejnaTuraText.GetComponent<Text>().color.b, alpha);
            PanelTury.GetComponent<Image>().color = new Color(PanelTury.GetComponent<Image>().color.r, PanelTury.GetComponent<Image>().color.g, PanelTury.GetComponent<Image>().color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            KolejnaTuraText.GetComponent<Text>().color = new Color(KolejnaTuraText.GetComponent<Text>().color.r, KolejnaTuraText.GetComponent<Text>().color.g, KolejnaTuraText.GetComponent<Text>().color.b, alpha);
            PanelTury.GetComponent<Image>().color = new Color(PanelTury.GetComponent<Image>().color.r, PanelTury.GetComponent<Image>().color.g, PanelTury.GetComponent<Image>().color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        PanelTury.SetActive(false);
        yield return new WaitForSeconds(1); ;
    }
    // yield return new WaitForSeconds(2);
    // LeanTween.alpha(FazaRedakcyjnaText, 0, fadeDuration).setEase(LeanTweenType.easeInCirc);
    // FazaRedakcyjnaText.gameObject.SetActive(false);
    // }
    public void OffMainMenu()
    {
        LeanTween.moveLocal(logo, new Vector3(0, 580, 0), 1).setEaseInOutCubic();
        LeanTween.moveLocalX(playButton2, 750, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(instructionButton, -750, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(ExitButton, 0, 1).setEaseInOutCubic();
    }

    public void OffSecondaryMenu()
    {
        LeanTween.moveLocalX(Dalej_Button, 750, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(ExitButton, -750, 1).setEaseInOutCubic();
    }
   
    public void OnSecondaryMenu()
    {
        LeanTween.moveLocalX(Dalej_Button, 0, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(ExitButton, 0, 1).setEaseInOutCubic();
    }

    public void SelectUserNumberOn()
    {

        LeanTween.moveLocal(liczbaUzytkownikowDropdown, new Vector3(0, 0, 0), 1).setEaseInOutCubic();
        LeanTween.moveLocalX(Dalej_Button, 0, 1).setEaseInOutCubic();

    }
    public void SelectUserNumberOff(bool flaga)
    {

        LeanTween.moveLocalX(liczbaUzytkownikowDropdown, -750, 1).setEaseInOutCubic();
        if (flaga) LeanTween.moveLocalX(Dalej_Button, 750, 1).setEaseInOutCubic();

    }
    public void AddingUserOn()
    { 

        LeanTween.moveLocalX(DalejUser, 0, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(UserAgeInput, 0, 1).setEaseInOutCubic();

    }
    public void AddingUserOff()
    {

        LeanTween.moveLocalX(DalejUser, -750, 1).setEaseInOutCubic();
        LeanTween.moveLocalX(UserAgeInput, 750, 1).setEaseInOutCubic();

    }
    public void PaneleGraczaKartyOn()
    {

        LeanTween.moveLocalX(PaneleGraczaKarty, 30, 1).setEaseInOutCubic();


    }
    public void PaneleGraczaKartyOff()
    {

        LeanTween.moveLocalX(PaneleGraczaKarty, 1380, 1).setEaseInOutCubic();

    }


    public void ResetButtonOn()
    {

        LeanTween.moveLocalY(resetButton, -250, 1).setEaseInOutCubic();


    }
    public void ResetButtonOff()
    {

        LeanTween.moveLocalY(resetButton, -500, 1).setEaseInOutCubic();

    }

    public void DalejButtonPolozenieWyzej()
    {


        LeanTween.moveLocalY(Dalej_Button, -150, 1).setEaseInOutCubic();

    }

    public void StartRoundButtonPolozenieWyzej()
    {


        LeanTween.moveLocalY(StartRoundButton, -60, 1).setEaseInOutCubic();

    }

    public void StartRoundButtonPolozenieNizej()
    {


        LeanTween.moveLocalY(StartRoundButton, -500, 1).setEaseInOutCubic();

    }
    public void ExitButtonPolozenieNizej()
    {
        LeanTween.moveLocalY(ExitButton, -80, 1).setEaseInOutCubic();

    }
    public void ExitButtonPolozenieWyzej()
    {


        LeanTween.moveLocalY(ExitButton, -190, 1).setEaseInOutCubic();

    }
    public void DalejButtonPolozenieNizej()
    {


        LeanTween.moveLocalY(Dalej_Button, -40, 1).setEaseInOutCubic();

    }
    public void KartyCeluOn()
    {


        LeanTween.moveLocalX(KartyCeluObiekt, 0, 1).setEaseInOutCubic();

    }
    public void KartyCeluOff()
    {


        LeanTween.moveLocalX(KartyCeluObiekt, 1390, 1).setEaseInOutCubic();

    }
    public void OpenSettings()
    {

        LeanTween.moveLocalX(SettingsDialog, 0, 1).setEaseInOutCubic();
        //  dialog.SetBool("isHidden", false);
        //  playButton.SetBool("isHidden", true);
        //  SettingsButton.SetBool("isHidden", true);


    }
    public void CloseSettings()
    {

        LeanTween.moveLocalX(SettingsDialog, 1300, 1).setEaseInOutCubic();
        // playButton.SetBool("isHidden", false);
        // SettingsButton.SetBool("isHidden", false);
        // dialog.SetBool("isHidden", true);

    }
    public void GeneratorOn()
    {

        LeanTween.moveLocalY(randomGenerator, 60, 1).setEaseInOutCubic();

    }
    public void GeneratorOff()
    {

        LeanTween.moveLocalY(randomGenerator, -560, 1).setEaseInOutCubic();

    }

    public void PDBPanelON()
    {

        LeanTween.moveLocalY(PDB, 333, 1).setEaseInOutCubic();

    }
    public void InstructionButtonON()
    {

        LeanTween.moveLocalY(InstructionButton, 312, 1).setEaseInOutCubic();

    }
    public void InstructionButtonOff()
    {

        LeanTween.moveLocalY(InstructionButton, 380, 1).setEaseInOutCubic();

    }
    public void ResetPDBButtonON()
    {

        LeanTween.moveLocalY(NoChosenCardButton, 312, 1).setEaseInOutCubic();

    }
    public void ResetPDBButtonOFF()
    {

        LeanTween.moveLocalY(ResetPDBButton, 380, 1).setEaseInOutCubic();

    }
    public void NoChosenCardButtonON()
    {

        LeanTween.moveLocalY(ResetPDBButton, 312, 1).setEaseInOutCubic();

    }
    public void NoChosenCardButtonOFF()
    {

        LeanTween.moveLocalY(NoChosenCardButton, 380, 1).setEaseInOutCubic();

    }

    public void PDBPanelOFF()
    {

        LeanTween.moveLocalY(PDB,358, 1).setEaseInOutCubic();

    }


    public void ShowAll()
    {
        if (lockedCard)
        {
            LeanTween.moveLocal(LewaGorna, new Vector3(90, -130, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(PrawaGorna, new Vector3(-38, -130, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(PrawaDolna, new Vector3(-38, 78, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(LewaDolna, new Vector3(90, 78, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(Srodkowa, new Vector3(26, 104, 0), 0.5f).setEaseInOutCubic();
        }
    }

    public void HideAll()
    {
        if (lockedCard)
        {
            LeanTween.moveLocal(LewaGorna, new Vector3(-90, 130, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(PrawaGorna, new Vector3(142, 130, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(PrawaDolna, new Vector3(142, -182, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(LewaDolna, new Vector3(-90, -182, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(Srodkowa, new Vector3(26, -210, 0), 0.5f).setEaseInOutCubic();
        }
    }
    public void HideAllAnyway()
    {
 Debug.Log("Koniec gry");
        LeanTween.moveLocal(LewaGorna, new Vector3(-90, 130, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(PrawaGorna, new Vector3(142, 130, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(PrawaDolna, new Vector3(142, -182, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(LewaDolna, new Vector3(-90, -182, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(Srodkowa, new Vector3(26, -210, 0), 0.5f).setEaseInOutCubic();

    }

    public void NewsCardPanelOn()
    {

        LeanTween.moveLocalY(newsCardPanelWithoutButton, 0, 1).setEaseInOutCubic();

    }
    public void NewsCardPanelOff()
    {

        LeanTween.moveLocalY(newsCardPanelWithoutButton, 700, 1).setEaseInOutCubic();
        //Debug.Log("HA");
    }

    public void NewsCardPanelOnOff(bool flag)
    {
        if(flag) LeanTween.moveLocalY(newsCardPanel, 0, 1).setEaseInOutCubic();
        else LeanTween.moveLocalY(newsCardPanel, 700, 1).setEaseInOutCubic();


    }
    public void NewsCardPanelOnOffWithoutButton(bool flag)
    {
       
        if (flag && !uiManager.gameManager.EndOfGameFlag) LeanTween.moveLocalY(newsCardPanelWithoutButton, 0, 1).setEaseInOutCubic();
        else LeanTween.moveLocalY(newsCardPanelWithoutButton, 700, 1).setEaseInOutCubic();


    }

    public void NewsCardPanelOnWithButtons()
    {

        LeanTween.moveLocalY(newsCardPanelWithoutButton, 0, 1).setEaseInOutCubic();
        LeanTween.moveLocalY(StartRoundButton, -60, 1).setEaseInOutCubic();
    }
    public void NewsCardPanelOffWithButtons()
    {

        LeanTween.moveLocalY(newsCardPanelWithoutButton, 700, 1).setEaseInOutCubic();
        LeanTween.moveLocalY(StartRoundButton, -500, 1).setEaseInOutCubic();
    }

    public void RankingChartObjectOn()
    {
        LeanTween.moveLocalY(RankingChartsObject, -100, 1).setEaseInOutCubic();
        LeanTween.size(panelRankings.GetComponent<RectTransform>(), new Vector2(600,250),1f).setDelay(1f);
    }

    public void RankingChartObjectOff()
    {
        LeanTween.moveLocalY(RankingChartsObject, -900, 1).setEaseInOutCubic();
    }
    public void UserCardOnOff(int i)
    {
        StartCoroutine(UserCardOnOffCoroutine(i, false));
    }
    public void UserCardOnOff(int i, bool wait)
    {
        StartCoroutine(UserCardOnOffCoroutine(i, wait));
    }


    public void HideCard()
    {




    }


    public void ShowCard()
    {


    }
    public void QuestionCardOn(GameObject QC)
    {
        StartCoroutine(QuestionCardOnCoroutine(QC));
    }
    IEnumerator QuestionCardOnCoroutine(GameObject questionCardObject)
    {

         yield return new WaitForSeconds(2);
         LeanTween.moveLocal(questionCardObject, new Vector3(0, 0, 0), 0.5f).setEaseInOutCubic();


    }

    IEnumerator UserCardOnOffCoroutine(int i, bool ifWait)
    {
        uiManager.WybierzKarteNewsaButton.interactable = false;
  
        if (ifWait) yield return new WaitForSeconds(2);
        LeanTween.moveLocal(LewaGorna, new Vector3(-90, 130, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(PrawaGorna, new Vector3(142, 130, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(PrawaDolna, new Vector3(142, -182, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(LewaDolna, new Vector3(-90, -182, 0), 0.5f).setEaseInOutCubic();
        LeanTween.moveLocal(Srodkowa, new Vector3(26, -210, 0), 0.5f).setEaseInOutCubic();
        if (lockedCard)
        {
            uiManager.flagaTrojkata = i;
            if (flag[i])
            {
                //Debug.Log("a");
                UserCardOff();
                // NewsCardPanelOff();
                if (i == 0) LeanTween.moveLocal(LewaGorna, new Vector3(90, -130, 0), 0.5f).setEaseInOutCubic();
                if (i == 1) LeanTween.moveLocal(PrawaGorna, new Vector3(-38, -130, 0), 0.5f).setEaseInOutCubic();
                if (i == 2) LeanTween.moveLocal(PrawaDolna, new Vector3(-38, 78, 0), 0.5f).setEaseInOutCubic();
                if (i == 3) LeanTween.moveLocal(LewaDolna, new Vector3(90,78, 0), 0.5f).setEaseInOutCubic();
                if (i == 4) LeanTween.moveLocal(Srodkowa, new Vector3(26, 104, 0), 0.5f).setEaseInOutCubic();

            }
            else
            {
                // NewsCardPanelOn();
                if (i == 0) LeanTween.moveLocal(LewaGorna, new Vector3(-90, 130, 0), 0.5f).setEaseInOutCubic();
                if (i == 1) LeanTween.moveLocal(PrawaGorna, new Vector3(142, 130, 0), 0.5f).setEaseInOutCubic();
                if (i == 2) LeanTween.moveLocal(PrawaDolna, new Vector3(142, -182, 0), 0.5f).setEaseInOutCubic();
                if (i == 3) LeanTween.moveLocal(LewaDolna, new Vector3(-90, -182, 0), 0.5f).setEaseInOutCubic();
                if (i == 4) LeanTween.moveLocal(Srodkowa, new Vector3(26, -210, 0), 0.5f).setEaseInOutCubic();

            if(uiManager.gameManager.koniecGryTag ==false)
                if (uiManager.activeUser == 0) LeanTween.moveLocal(LewaGorna, new Vector3(90, -130, 0), 0.5f).setEaseInOutCubic();
                if (uiManager.activeUser == 1) LeanTween.moveLocal(PrawaGorna, new Vector3(-38, -130, 0), 0.5f).setEaseInOutCubic();
                if (uiManager.activeUser == 2) LeanTween.moveLocal(PrawaDolna, new Vector3(-38, 78, 0), 0.5f).setEaseInOutCubic();
                if (uiManager.activeUser == 3) LeanTween.moveLocal(LewaDolna, new Vector3(90, 78, 0), 0.5f).setEaseInOutCubic();
                if (uiManager.activeUser == 4) LeanTween.moveLocal(Srodkowa, new Vector3(26, 104, 0), 0.5f).setEaseInOutCubic();

                uiManager.ToggleCard(false);
                uiManager.TogglePanel(false);
                //tutaj dac funkcje, ktora po schowaniu wszystkich kart powoduje pojawienie sie tej, ktora nalezy do obecnie rozgrywanego gracza

            }

            flag[i] = !flag[i];
        }
        uiManager.WybierzKarteNewsaButton.interactable = true;
    }

        public void UserCardOff()
    {
        if (lockedCard)
        {
            uiManager.ToggleCard(false);
            uiManager.TogglePanel(false);
            LeanTween.moveLocal(LewaGorna, new Vector3(-90, 130, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(PrawaGorna, new Vector3(142, 130, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(PrawaDolna, new Vector3(142, -182, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(LewaDolna, new Vector3(-90, -182, 0), 0.5f).setEaseInOutCubic();
            LeanTween.moveLocal(Srodkowa, new Vector3(26, -210, 0), 0.5f).setEaseInOutCubic();
            Start();
        }
    }


    public void UserIndicatorOn()
    {

        LeanTween.moveLocalY(CurrentUserIndicatorGameObject, 306, 1).setEaseInOutCubic();

    }
    public void UserIndicatorOff()
    {

        LeanTween.moveLocalY(CurrentUserIndicatorGameObject, 376, 1).setEaseInOutCubic();

    }

    public void UserIndicatorPDBBarOn()
    {

        LeanTween.moveLocalY(CurrentUserIndicatorGameObject, 280.22f, 1).setEaseInOutCubic();

    }
    public void UserIndicatorPDBBarOff()
    {

        LeanTween.moveLocalY(CurrentUserIndicatorGameObject, 308, 1).setEaseInOutCubic();

    }


}
