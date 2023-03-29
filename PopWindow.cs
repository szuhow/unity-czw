using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopWindow : MonoBehaviour
{

    public Canvas canvasConfPopUp;

    private Text textTitle;
    private Text textDesc;

    public void ConfirmPopUp(string title, string desc)
    {

        if (canvasConfPopUp == null)
        {
            Debug.LogWarning("No popup canvas");
        }

        textTitle.text = title;
        textDesc.text = desc;

        Button confButton = canvasConfPopUp.GetComponentInChildren<Button>();
        confButton.onClick.RemoveAllListeners();
        confButton.onClick.AddListener(() => canvasConfPopUp.enabled = false);
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(confButton.gameObject);
        }

        canvasConfPopUp.enabled = true;
    }

    // Use this for initialization
    void Awake()
    {

        InitTexts();

    }

    void InitTexts()
    {
        Text[] texts = canvasConfPopUp.GetComponentsInChildren<Text>();
        foreach (var txt in texts)
        {
            switch (txt.name)
            {
                case "HeaderTitle":
                    textTitle = txt;
                    break;
                case "TextBox":
                    textDesc = txt;
                    break;
                default:
                    break;
            }
        }
    }

}
