using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverColor : MonoBehaviour
{
    private Image image;
    public Color defaultColor = new Color(1, 1, 1, 0);
    public Color hoverColor = new Color(1, 1, 1, 0.5f);
    public float animDuration = 0.2f;
    private float animTime = 0;
    private bool hovered = false;

    public void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnHoverStart()
    {
        hovered = true;
    }

    public void OnHoverEnd()
    {
        hovered = false;
    }

    private void Update()
    {
        if(hovered) animTime += Time.deltaTime;
        else animTime -= Time.deltaTime;
        if(animTime > animDuration) animTime = animDuration;
        if(animTime < 0) animTime = 0;

        image.color = Color.Lerp(defaultColor, hoverColor, animTime / animDuration);
    }
}
