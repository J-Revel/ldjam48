using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    public string description;

    public Text text;

    public void Start()
    {
        text.text = description;
    }

    public void UpdateDisplay()
    {
        
    }
}