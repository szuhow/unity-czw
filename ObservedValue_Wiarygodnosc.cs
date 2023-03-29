using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObservedValue_Wiarygodnosc : MonoBehaviour
{
    public Text value;

    public event Action OnValueIncreased;
    public event Action OnValueDecreased;

    public void GetChanged(int value)
    {




    }
}
