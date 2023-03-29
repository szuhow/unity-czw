using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerujPunktacje : MonoBehaviour
{
    //public GameObject singlePointObject;
    public GameObject PDBPanel;
    public TweenAnimations animacja;
    // public GameObject SinglePrefabPoint;

    public bool isPanel=false;
    void Start()
    {
        PDBPanel.SetActive(false);
    }
    public void GenerujPanel()
    {
     
        animacja.PDBPanelON();
       

    }
    public void UkryjPanel()
    {

       animacja.PDBPanelOFF();
        isPanel = false;

    }




    public void PokazPanel()
    {
       // animacja.PDBPanelShowOn();
        if (PDBPanel.GetComponent<PDBControl>().locked) 
           PDBPanel.SetActive(true);
       
    }
    public void SchowajPanel()
    {

     
       // if(isPanel)
          //  PDBPanel.SetActive(false);
       // isPanel = false;
    }
   
}