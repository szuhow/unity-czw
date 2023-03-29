using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManager uimanager;
    public GameObject[] CircleBars;
    public Text[] poziomPoparcia;
    float timeAmt = 5;
    float time;
    int i = 0;
    public int[] lista;
    
    //var yieldInstruction = new WaitForEndOfFrame();
  
    public void StartCo()
    {
       // time = timeAmt;
        StartCoroutine("DoStuffRepeatedly");
        //Debug.Log("Wynik szarego: " + (100 - (lista[0] + lista[1] + lista[2] + lista[3])));
        while ((100 - (lista[0] + lista[1] + lista[2] + lista[3]) < 15) || (100 - (lista[0] + lista[1] + lista[2] + lista[3]) > 64)) {
            StartCo();
            Debug.Log("Ponowne losowanie");
        }

    }

    public void StartCo2()
    {
        StartCoroutine("SamePoints");

    }







    // Update is called once per frame
    void Update()
    {
        //if (time > 0)
        //{
         //   time -= Time.deltaTime;
         //   CircleBars[i].fillAmount = time / timeAmt;
       // }
    }
    /*
    public double sampleNormal()
    {
        Random rand = new Random();
        float u = (Random.Range(0f,1f) / 24) * 2 - 1;
        float v = (Random.Range(0f, 1f) / 24) * 2 - 1;
        float r = u * u + v * v;
        //if (r == 0 || r > 1) return sampleNormal();
        double c = Mathf.Sqrt(-2 * Mathf.Log(r) / r);
        return u * c;
    }

    */
    public void ResetGenerator()
    {
        //Debug.Log("Reset wykresow");
        for (int i = 0; i < CircleBars.Length; i++)
        {
            CircleBars[i].GetComponent<Image>().fillAmount = 0;
            if( i<4 )poziomPoparcia[i].text = "0";
        }
        }

    public void RandomGen()
    {
        lista = new int[5];
        GameManager.instance.users.poziomPoparciaGraczy.Clear();
        GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy.Clear();
        for (int i = 0; i < CircleBars.Length; i++)
        {
            if (i < 4) lista[i] = Random.Range(5, 24);
            else lista[4] = 100 - (lista[0] + lista[1] + lista[2] + lista[3]);
            GameManager.instance.users.poziomPoparciaGraczy.Add(0);
            GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy.Add(0);
            CircleBars[i].GetComponent<ID>().points = lista[i];
        }
       // int a = (100 - (lista[0] + lista[1] + lista[2] + lista[3]));
       // Debug.Log(lista[0] + " oraz "+ lista[1] + " oraz " + lista[2] + " oraz " + lista[3] + " oraz " + lista[4]);

    }
    public float RemapFloat(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }



    private IEnumerator SamePoints()
    {
        lista = new int[5];
        GameManager.instance.users.poziomPoparciaGraczy.Clear();
        GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy.Clear();
        for (int i = 0; i < CircleBars.Length; i++)
        {
            if (i < 4) lista[i] = 15;
            else lista[i] = 40;
            GameManager.instance.users.poziomPoparciaGraczy.Add(0);
            GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy.Add(0);
            CircleBars[i].GetComponent<ID>().points = lista[i];
        }


            for (int i = 0; i < CircleBars.Length; i++)
        {
            for (float k = 0; k < RemapFloat(lista[i], 4, 24, 0.16f, 1); k += Time.deltaTime)
            {
                if (i < 4)
                {
                    CircleBars[i].GetComponent<Image>().fillAmount = Mathf.Lerp(0.16f, RemapFloat(lista[i], 4, 24, 0.16f, 1), k / RemapFloat(lista[i], 4, 24, 0.16f, 1));
                    poziomPoparcia[i].text = lista[i] + "%";
                    poziomPoparcia[i].GetComponentInParent<ID>().poparcie = lista[i];
                    poziomPoparcia[i].GetComponentInParent<ID>().poczatkowePoparcie = lista[i];
                }
                else
                {
                    CircleBars[i].GetComponent<Image>().fillAmount = Mathf.Lerp(0.16f, RemapFloat(lista[i], 15, 64, 0.16f, 1), k / RemapFloat(lista[i], 4, 24, 0.16f, 1));
                    poziomPoparcia[i].text = lista[i] + "%";
                    poziomPoparcia[i].GetComponentInParent<ID>().poparcie = lista[i];
                    poziomPoparcia[i].GetComponentInParent<ID>().poczatkowePoparcie = lista[i];
                }
                yield return null;

            }
        }
      
        GameManager.instance.users.Suma4Graczy();
        uimanager.Dalej_Button.GetComponent<Button>().interactable = true;
    }
    private IEnumerator DoStuffRepeatedly()
    {
        uimanager.Dalej_Button.GetComponent<Button>().interactable = false;
        // while (true)
        //  {
        // do some stuff here
        //   yield return new WaitForSeconds(2.0f);
        //    CircleBars[i].fillAmount = Time.time / timeAmt;
        // }
        uimanager.dialogueManager.DisplaySpecificSentence(17);
        RandomGen();
        //Debug.Log(CircleBars.Length);
        for (int i=0; i < CircleBars.Length; i++) {
            // Debug.Log(RemapFloat(lista[i], 4, 24, 0, 1));

           
            // Debug.Log(lista[i]);

            for (float k = 0; k < RemapFloat(lista[i], 4, 24, 0.16f, 1); k += Time.deltaTime)
        {

                if (i < 4)
                {
                    CircleBars[i].GetComponent<Image>().fillAmount = Mathf.Lerp(0.16f, RemapFloat(lista[i], 4, 24, 0.16f, 1), k / RemapFloat(lista[i], 4, 24, 0.16f, 1));
                    poziomPoparcia[i].text = lista[i].ToString() +"%";
                    poziomPoparcia[i].GetComponentInParent<ID>().poparcie = lista[i];
                    poziomPoparcia[i].GetComponentInParent<ID>().poczatkowePoparcie= lista[i];

                }
                else
                {
                    CircleBars[4].GetComponent<Image>().fillAmount = Mathf.Lerp(0, RemapFloat(100 - (lista[0] + lista[1] + lista[2] + lista[3]), 15, 64, 0, 1), k);
                    int a = 100 - (lista[0] + lista[1] + lista[2] + lista[3]);
                    poziomPoparcia[4].text = a.ToString() + "%";
                    poziomPoparcia[4].GetComponentInParent<ID>().poparcie = lista[4];
                    poziomPoparcia[4].GetComponentInParent<ID>().poczatkowePoparcie = lista[4];
                }
                GameManager.instance.users.poziomPoparciaGraczy[i] = lista[i];
                GameManager.instance.users.poziomPoczatkowegoPoparciaGraczy[i] = lista[i];
                yield return null;
              
            }
           
        }
        GameManager.instance.users.Suma4Graczy();
        uimanager.Dalej_Button.GetComponent<Button>().interactable = true;
        uimanager.dialogueManager.DisplaySpecificSentence(13);
       // uimanager.Dalej_Button.SetActive(false);
       // uimanager.animacja.ExitButton.SetActive(false);
       // uimanager.resetButton.gameObject.SetActive(false);
       // uimanager.StartRoundButton.SetActive(true);
    }
}
