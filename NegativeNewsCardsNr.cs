using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeNewsCardsNr : MonoBehaviour
{
    public Component[] NewsCards;
    public int nrOfNegativeCards=0;
    public int CheckNr()
    {
        NewsCards = GetComponentsInChildren(typeof(IDCard), true);
        foreach (IDCard newscard in NewsCards)
            if (newscard.wiarygodnosc < 0) nrOfNegativeCards += 1;
        return nrOfNegativeCards;
    }
}
