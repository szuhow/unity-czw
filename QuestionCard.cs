using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class QuestionCard
{

   // public int id;
    public string question;
    public string firstAnswer;
    public string secondAnswer;
    public string thirdAnswer;
    public int answer;
   


    public QuestionCard(string Question, string FirstAnswer, string SecondAnswer, string ThirdAnswer, int Answer)
    {
      //  id = Id;
        question=Question;
        firstAnswer=FirstAnswer;
        secondAnswer=SecondAnswer;
        thirdAnswer=ThirdAnswer;
        answer=Answer;
    }


}
