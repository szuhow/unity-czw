using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDBControl : MonoBehaviour
{
    public bool locked = false;
    public UIManager uimanager;
    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        if (locked)
        {
            uimanager.animacja.PDBPanelON();
            uimanager.animacja.UserIndicatorPDBBarOn();
            uimanager.animacja.InstructionButtonON();
            if(uimanager.gameManager.koniecGryTag == false) uimanager.animacja.NoChosenCardButtonON();
            uimanager.animacja.ResetPDBButtonON();
        }
    }
    private void OnMouseExit()
    {
        if (locked)
        {
            uimanager.animacja.PDBPanelOFF();
            uimanager.animacja.UserIndicatorPDBBarOff();
            uimanager.animacja.InstructionButtonOff();
            uimanager.animacja.NoChosenCardButtonOFF();
            uimanager.animacja.ResetPDBButtonOFF();
        }
    }
}
