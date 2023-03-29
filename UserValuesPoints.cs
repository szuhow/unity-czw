using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UserValuePoints
{
    public int wiarygodnosc;
    public int finanse;

    public UserValuePoints(int Wiarygodnosc, int Finanse)
    {
        finanse = Finanse;
        wiarygodnosc = Wiarygodnosc;
    }
}