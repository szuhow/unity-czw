using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsDeck : MonoBehaviour
{
    public List<QuestionCard> questionDeck = new List<QuestionCard>();
    //public List<QuestionCard> container = new List<QuestionCard>();
    //  public int x;

    void Start()
    {
        // x = 0;

        for (int i = 0; i < 100; i++)
        {

            questionDeck[i] = QuestionsDataBase.QuestionsList[i];

        }
    }

    //public void Shuffle()
    //{
    //    for (int i = 0; i < deck.Count - 1; i++)
    //    {

    //        // Debug.Log(i);
    //        container[0] = deck[i];
    //        int randomIndex = Random.Range(i, deck.Count);
    //        deck[i] = deck[randomIndex];
    //        deck[randomIndex] = container[0];

    //    }


    //}

}
