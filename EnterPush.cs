using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EnterPush : MonoBehaviour
{
   
    public Button addUser;
    public GameObject User;
    public InputField nameInput;
    public InputField dateInput;
    // Start is called before the first frame update
    public void Start()
    {
        addUser.onClick.AddListener(() => Clear() );

    }
    public void Clear()
    {
       // addUser.onClick.Invoke();
        dateInput.Select();
        dateInput.text = "";
        nameInput.Select();
        nameInput.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) ){
            addUser.onClick.Invoke();
            dateInput.Select();
            dateInput.text = "";
            nameInput.Select();
            nameInput.text = "";
        }
    }
}
