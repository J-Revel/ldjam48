using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableDuringDrag : MonoBehaviour
{
    Image img;
    
    void Start()
    {
        img = GetComponent<Image>();
        // GetComponentInParent<Draggable>().dragChangeDelegate += OnDragChange;
    }

    void OnDragChange(bool dragging)
    {
        img.raycastTarget = !dragging;
    }
}
