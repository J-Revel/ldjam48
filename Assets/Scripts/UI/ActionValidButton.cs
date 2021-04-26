using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionValidButton : MonoBehaviour
{
    ActionPopup popup;
    
    public Sprite validSprite;
    public Sprite invalidSprite;
    private Image image;

    void Start()
    {
        popup = GetComponentInParent<ActionPopup>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = popup.allSlotsFilled ? validSprite : invalidSprite;
    }
}
