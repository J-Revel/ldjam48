using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraFX : MonoBehaviour
{
    private Image auraFX;
    public float value;
    void Start()
    {
        auraFX = GetComponent<Image>();
    }

    void Update()
    {
        transform.localScale = new Vector3(value, value, value);
        auraFX.color = new Color(0, 0, 0, value);
    }
}
