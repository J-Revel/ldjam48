using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public MouseCursor mouseCursor;
    
    void Update()
    {
        RaycastHit2D[] raycastResults = Physics2D.RaycastAll(mouseCursor.transform.position, Vector2.zero);
        Draggable closestResult = null;
        foreach(RaycastHit2D raycastResult in raycastResults)
        {

            if(raycastResult.collider != null)
            {
                Draggable draggable = raycastResult.collider.GetComponent<Draggable>();
                if(draggable != null)
                {
                    if(closestResult == null || closestResult.sortIndex < draggable.sortIndex)
                    {
                        closestResult = draggable;
                    }
                }
            }
        }
        if(closestResult != null)
            DraggableOrderSorter.instance.SetToFront(closestResult);
    }
}
