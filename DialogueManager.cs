using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    // private Queue<string> sentences;
    private List<string> sentences_string;
    public Text nameText;
    public Text dialogueText;
    public int i = 0;
    public int globalIndex=0;
    public TypeWriter writ = new TypeWriter();
    public UIManager uimanager;
    // Start is called before the first frame update
    public void Start()
    {
        i = 0;
        //sentences = new Queue<string>();
        sentences_string = new List<string>();
        StartCoroutine(Welcome());
    }
    
    public void Clear()
    {
        writ.Write("");
    }

    IEnumerator Welcome()
    {
        
        StartDialogue(dialogue);
        
        yield return new WaitForSeconds(2);

        DisplaySpecificSentence(1);
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
       // Debug.Log("Starting" + dialogue.name);
        //sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            //sentences.Enqueue(sentence);
            sentences_string.Add(sentence);
        }




        DisplaySpecificSentence(0);
    }



    public void DisplaySpecificSentence(int a)
    {
       
       
        if (a >= 0)
        {
            string sentence = sentences_string[a];
            //if(!sentence.Equals(dialogueText.text)) writ.Write(sentence);
            if (a!=globalIndex) writ.Write(sentence);
        }
        globalIndex = a;
    }

    public void DisplaySpecificSentenceWithAdditionalText(int a, string text)
    {
        globalIndex = a;

        if (a >= 0)
        {
            string sentence = sentences_string[a]+text;
            writ.Write(sentence);
        }

    }
    public void DisplayNextSentence(string a)
    {

        if (a == "+") i++;
        else if (a == "-") i--;
       // else;
        // if(sentences.Count == 0)
        //{
        // EndDialogue();
        // return;
        // }
        Debug.Log(i);
        //string sentence = sentences.Dequeue();
        if (i >=0)
        {
            string sentence = sentences_string[i];
            writ.Write(sentence);
        }
       // Debug.Log(i);
       

            }


    void EndDialogue()
    {
        

    }
}
