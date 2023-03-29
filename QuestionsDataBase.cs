using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionsDataBase : MonoBehaviour
{
  
    public static List<QuestionCard> QuestionsList = new List<QuestionCard>();



    void Awake()
    {
        readQuestions();
    }
    void readQuestions()
    {
        TextAsset DataCSV = Resources.Load<TextAsset>("pytania");

        string[] line = DataCSV.text.Split(new char[] { '\n' });

        for (int i = 0; i < line.Length; i++)
        {
            string[] part = line[i].Split(new char[] { '\'' });
            QuestionsList.Add(new QuestionCard(part[0], part[1], part[2], part[3], int.Parse(part[4])));
           //Debug.Log(part[0]);      
        }
    }

}
