using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardNewsDataBase : MonoBehaviour
{
   public static List<Card> cardNewsList = new List<Card>();

 

    void Awake() {
        readTextFile();
        //cardNewsListInspector = cardNewsList;
    }



    void readTextFile()
    {


     
            TextAsset DataCSV = Resources.Load<TextAsset>("cards");

            string[] line = DataCSV.text.Split(new char[] { '\n' });

            for (int i = 0; i < line.Length; i++)
            {
                string[] part = line[i].Split(new char[] { '\'' });
                cardNewsList.Add(new Card(int.Parse(part[0]), part[1], part[2], part[3], float.Parse(part[6]), float.Parse(part[7]), float.Parse(part[4]), float.Parse(part[5]), part[8], part[9]));
            //Debug.Log(int.Parse(part[0]));      
        }
        


    }

}
