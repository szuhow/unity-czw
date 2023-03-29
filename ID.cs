using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ID : MonoBehaviour, ISerializationCallbackReceiver
{
    public List<UserValuePoints> usersValues = new List<UserValuePoints>();

    public int Identify;
    public List<GameObject> userNewsDeck = new List<GameObject>();
    public string Name;
    public string kolor;
    public int Wiek;
    public Color color;
    public int PanelGracza;
    public int KartaCelu;
    public bool isActive = false;
    public int points;
    public int poparcie;
    public int poczatkowePoparcie;
    public GameObject useButton;
    public GameObject useToggle;
    public bool usableButtonPanel = true;
    public GameObject KartNewsow;
    public string WiekSzczegolowy;
    public string panelGracza;
    public Text poczatkowePoparcieText;
    public Text Wiarygodnosc;
    public Text Finanse;
    public List<Toggle> options = new List<Toggle>();

    public Dictionary<string,int> categoryCardScore = new Dictionary<string, int>();
    public List<string> _keys = new List<string> ();
    public List<int> _values = new List<int>();
  
    public void UpdateUserValuePoints()
    {
        usersValues.Add(new UserValuePoints(int.Parse(Wiarygodnosc.text), int.Parse(Finanse.text)));
    } //inaczej, bez add to list, lecz update list

    public void TurnToggles(bool flag)
    {


        for(int i=0; i < options.Count; i++)
        {

            options[i].interactable = flag;
        }

    }
    public void TurnTogglesExcept(bool flag, int except)
    {


        for (int i = 0; i < options.Count; i++)
        {

            if(i!=except) options[i].interactable = flag;
        }

    }
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in categoryCardScore)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        categoryCardScore = new Dictionary<string, int>();

        for (int i = 0; i != System.Math.Min(_keys.Count, _values.Count); i++)
            categoryCardScore.Add(_keys[i], _values[i]);
    }

    void Start()
    {



        if (useButton != null)
        {
            useButton.GetComponent<Button>().interactable = true;
            useButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                GameObject.Find("GMManager").GetComponent<MainGameManager>().UzyjPaneluGracza(panelGracza);
                useButton.GetComponent<Button>().interactable = false;
                // GameObject.Find("UIManager").GetComponent<UIManager>().TogglePanel(false);
            });
        }
        categoryCardScore["Fake news"] = 0;
        categoryCardScore["Treść tabloidowa"] = 0;
        categoryCardScore["Treść propagandowa"] = 0;
        categoryCardScore["Zmanipulowana treść"] = 0;
        categoryCardScore["Rzetelne dziennikarstwo"] = 0;
        categoryCardScore["Pogłębiona analiza"] = 0;
        categoryCardScore["Dziennikarstwo śledcze"] = 0;
        categoryCardScore["Rzetelny reportaż"] = 0;
        categoryCardScore["none"] = 0;
    }

    public void IfPanelUsable(bool intercatable_)
    {

       // useButton.transform.Find("UseButton");
        useButton.GetComponent<Button>().interactable =intercatable_;
    }
    public void IfPanelToggleUsable(bool intercatable_)
    {

        // useButton.transform.Find("UseButton");
        useToggle.GetComponent<Toggle>().interactable = intercatable_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
