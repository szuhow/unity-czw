using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 using UnityEngine.SceneManagement;
public class UserManager : MonoBehaviour
{


    public List<string> userName = new List<string>();
    public List<string> userAge = new List<string>();
    public List<int > PanelGracza = new List<int>(5);
    public List<int> KartyCelu = new List<int>(5);
    public List<int> poziomPoparciaGraczy= new List<int>(5);
    public List<int> poziomPoczatkowegoPoparciaGraczy = new List<int>(5);
    public List<int> poziomWiarygodnosciGraczy = new List<int>(5);
    public List<int> poziomFinansowGraczy = new List<int>(5);
    public List<int> punktyZwyciestwa= new List<int>(5);
    public List<int> punktyKartCelow = new List<int>(5);
    public int sumaPoparcia=0;
    public int TargetCard;
    public string UserName;
    public string UserAge;
    public List<int> ID = new List<int>();
    public List<string> _dates;

    public void Start() {

        for (int i = 0; i < 5; i++)
        {
            poziomWiarygodnosciGraczy.Add(0);
            poziomFinansowGraczy.Add(0);
            punktyZwyciestwa.Add(0);
        }
    }
void OnEnable() {
      SceneManager.sceneLoaded += OnSceneLoaded;
  }
 
  void OnDisable() {
      SceneManager.sceneLoaded -= OnSceneLoaded;
  }
 
  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    userName.Clear();
    userAge.Clear();
    PanelGracza.Clear();
    KartyCelu.Clear();
    poziomPoparciaGraczy=new List<int>(5);
    poziomPoczatkowegoPoparciaGraczy=new List<int>(5);
    poziomWiarygodnosciGraczy=new List<int>(5);
    poziomFinansowGraczy=new List<int>(5);
    punktyZwyciestwa=new List<int>(5);
    punktyKartCelow=new List<int>(5);
    if(_dates.Count>0) _dates.Clear();
  }
    
    public void Suma4Graczy()
    { //suma pooparcia 4 czy 5 graczy???????????????????????????????
        sumaPoparcia = 0;
        for(int i =0; i < 4; i++)
        {
           
            sumaPoparcia += poziomPoparciaGraczy[i];
        }


    }
    public void AddUsers( int TC, string name, string age)
    {
      //  PanelGracza = PG;
        TargetCard = TC;
        UserName = name;
        UserAge = age;


    }


}
