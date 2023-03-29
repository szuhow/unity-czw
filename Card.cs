using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{

    public int id;
    public string cardName;
    public string cardCategory;
    public string cardDescription;
    public float wiarygodnosc;
    public float money;
    public float zysk;
    public float strata;
    public string partiaZysk;
    public string partiaStrata;

 

    public Card(int Id, string CardName, string CardCategory, string CardDescription, float Wiarygodnosc, float Money, float Zysk, float Strata, string PartiaZysk, string PartiaStrata)
    {
        id = Id;
        cardName = CardName;
        cardCategory = CardCategory;
        cardDescription = CardDescription;
        wiarygodnosc = Wiarygodnosc;
        money = Money;
        zysk = Zysk;
        strata = Strata;
        partiaZysk = PartiaZysk;
        partiaStrata = PartiaStrata;



    }


}
