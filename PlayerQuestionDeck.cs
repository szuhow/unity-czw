using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestionDeck : MonoBehaviour
{
    public List<QuestionCard> questionDeck = new List<QuestionCard>();
    public List<QuestionCard> container = new List<QuestionCard>();
    //  public int x;

    void Start()
    {
        // x = 0;

        for (int i = 0; i < 100; i++)
        {

            questionDeck[i] = QuestionsDataBase.QuestionsList[i];
            //Debug.Log(questionDeck[i].question);
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < questionDeck.Count - 1; i++)
        {

            // Debug.Log(i);
            container[0] = questionDeck[i];
            int randomIndex = Random.Range(i, questionDeck.Count);
            questionDeck[i] = questionDeck[randomIndex];
            questionDeck[randomIndex] = container[0];

        }


    }

    public void Remove(int i)
    {
        questionDeck.RemoveAt(i);
    }
}
