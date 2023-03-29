using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private int whosTurn = 1;
    private bool coroutineAllowed = true;
    public int resultPlus;

    public int resultMinus;
    public MainGameManager gameManager;
    public bool diceFlag;
    public bool wartoscFlag = false;
	// Use this for initialization
	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];
	}

    private void OnMouseDown()
    {

        if (coroutineAllowed)
        {
            if (diceFlag) StartCoroutine("RollTheDicePlus");
           // else StartCoroutine("RollTheDiceMinus");
           
          
        }

    }


    public void ResetDice()
    {
        int resetDice = 5;  
        rend.sprite = diceSides[resetDice];
    }
    private IEnumerator RollTheDicePlus()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            resultPlus = randomDiceSide + 1;
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        if (wartoscFlag) {
        gameManager.SetOtherUserScoreRightWithDice("pieniadze");
       // gameManager.SetOtherUserScoreLeftWithDice("pieniadze");
        }
        else
        {
            
            gameManager.SetOtherUserScoreBothWithDice("poparcie");
           // gameManager.SetOtherUserScoreBothWithDice("poparcie");
        }
        gameManager.uimanagerPrefab.animacja.DDiceOff();
        gameManager.diceflag = true;
        gameManager.uimanagerPrefab.WybierzKarteNewsaButton.interactable = true;
        gameManager.NextPlayer(true);
        coroutineAllowed = true;
        wartoscFlag = false;
    }
    /*
    private IEnumerator RollTheDiceMinus()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            resultMinus = randomDiceSide+1;
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        gameManager.SetOtherUserScoreLeftWithDice("cos");
        gameManager.uimanagerPrefab.animacja.DDiceOff();
        gameManager.diceflag = true;
        gameManager.NextPlayer(true);
        coroutineAllowed = true;
    }
    */
}
