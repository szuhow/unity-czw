using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetValue : MonoBehaviour
{
    public Slider mainSlider;
    public Text text;
   public void SetPoint()
    {
        text.text = mainSlider.value.ToString();
    }
}
