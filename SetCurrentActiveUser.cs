using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentActiveUser : MonoBehaviour
{
    public UIManager _uimanager;
  //  public int activeUser;
   // public int activeUserOffset;
    public void SetActiveUser(int active)
    {
        _uimanager.activeUser = active;
       
    }
}
