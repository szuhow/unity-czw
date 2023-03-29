using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PDBUpdate : MonoBehaviour
{
    public UIManager uimanager;
    public float variable = 5f;
    public float actualvariable ;
    public Slider PDBSlider;
    // Example damage
    public  float prevValue = 0;
    public float thedamage = 1.0f;
    public Text PDBValue;
    public PDBControl pdbcontrol;
   
    public void Start()
    {
        actualvariable = prevValue;
        //StartCoroutine("IncreasePDBOverTime", .5f);

    }


    public void IncreasePDB(int wynik)
    {

        prevValue = actualvariable;
        actualvariable = wynik+prevValue;
        uimanager.PDBScore = (int)actualvariable;
        //if(sliderActive) 
            StartCoroutine("IncreasePDBOverTime", 1.0f);
       
    }
  
    
    IEnumerator IncreasePDBOverTime(float seconds)
    {
        
        float animationTime = 0f;
        uimanager.animacja.PDBPanelON();
        uimanager.animacja.UserIndicatorPDBBarOn();
        yield return new WaitForSeconds(1);
        while (animationTime < seconds)
        {
            
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            PDBSlider.value = Mathf.Lerp(prevValue / 30f, actualvariable / 30f, lerpValue);
            PDBValue.text = " " + (int)(PDBSlider.value * 30f) + "/30";
            //if (PDBSlider.value <= 0) uimanager.gameManager.EndOfGame();
            yield return null;
        }
        yield return new WaitForSeconds(1);
        uimanager.animacja.PDBPanelOFF();
        uimanager.animacja.UserIndicatorPDBBarOff();
       
    }

  

}
