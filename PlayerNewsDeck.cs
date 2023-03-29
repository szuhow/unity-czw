using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewsDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    //  public int x;

    void Start()
    {
       // x = 0;

        for (int i = 0;i< 100; i++){

            deck[i] = CardNewsDataBase.cardNewsList[i];

        }
    }

   public void Shuffle()
    {
        for(int i = 0; i <deck.Count-1; i++)
        {

           // Debug.Log(i);
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];

        }


    }

}
