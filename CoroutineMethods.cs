using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoroutineMethods : MonoBehaviour
{

    private int displayScore;
    public IEnumerator InitialSupport(Text scoreUI, int score)
    {

        while (displayScore < score + 1)
        {
         
            displayScore = int.Parse(scoreUI.text);
            if (displayScore < score)
            {
                //scoreUI.fontSize = 28;
                // scoreUI.color=Color.red;
                displayScore++; //Increment the display score by 1
                                // scoreUI.fontSize = 20;
                                // scoreUI.color = Color.white;
                scoreUI.text = displayScore.ToString(); //Write it to the UI


            }

            // else scoreUI.fontSize = 17;
            yield return new WaitForSeconds(0.5f); // I used .2 secs but you can update it as fast as you want

        }

    }


}
