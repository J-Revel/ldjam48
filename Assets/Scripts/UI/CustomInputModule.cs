using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInputModule : BaseInputModule
{
    public MouseCursor mouseCursor;
    private Vector3 lastPos;
    private PointerEventData lastEventData;
    private GameObject hovered;

    public override void Process ()
    {
        // Converting the 3D-coords to Canvas-coords (it is giving wrong results, how to do this??)
        Vector3 screenPos = mouseCursor.GetComponent<RectTransform>().position;
        List<RaycastResult> rayResults = new List<RaycastResult>();

        // float scaleFactorX = 1280.0f / Screen.width;
        // float scaleFactorY = 720.0f / Screen.height;
        //float scaleFactorX = canvasScaler.referenceResolution.x / Screen.width;
        //float scaleFactorY = canvasScaler.referenceResolution.y / Screen.height;

        // Raycasting
        PointerEventData pointer = new PointerEventData(eventSystem);
        pointer.position = screenPos;
        pointer.delta = screenPos - lastPos;

        eventSystem.RaycastAll (pointer, m_RaycastResultCache);
        var raycast = FindFirstRaycast (m_RaycastResultCache);
        pointer.pointerCurrentRaycast = raycast;
        m_RaycastResultCache.Clear();

        lastPos = screenPos;
        
        ProcessMove(pointer);
   }

   protected virtual void ProcessMove(PointerEventData pointerEvent)
    {
        bool moving = pointerEvent.IsPointerMoving ();

        // Drag notification
        if (moving && pointerEvent.pointerDrag != null)
        {
            // Before doing drag we should cancel any pointer down state
            // And clear selection!
            if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
            {
                ExecuteEvents.Execute (pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);

                pointerEvent.eligibleForClick = false;
                pointerEvent.pointerPress = null;
                pointerEvent.rawPointerPress = null;
            }
            ExecuteEvents.Execute (pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
        }

        var targetGO = pointerEvent.pointerCurrentRaycast.gameObject;
        if(lastEventData != null)
            pointerEvent.pointerEnter = lastEventData.pointerEnter;
        HandlePointerExitAndEnter (pointerEvent, targetGO);
        lastEventData = pointerEvent;
    }

    public override bool IsPointerOverGameObject(int pointerId)
    {
        if (lastEventData != null)
            return lastEventData.pointerEnter != null;
        return false;
    }
}