using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeWriter : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    public string currentText ="";
    public Text text;

    
    public void Write(string text)

    {
        StopCoroutine("ShowText");
        fullText = text;
        StartCoroutine("ShowText");
    }
    // zmienić klase na bardziej uniwersalną, żeby
    // tekst był dowolnie wybranym obiektem a nie tylko górna belka
  IEnumerator ShowText()
    {
        for(int i =0; i< fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            text.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);

        }
    }
}
