using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

        // Raycasting
        PointerEventData pointer = new PointerEventData(eventSystem);
        pointer.position = screenPos;
        pointer.delta = (screenPos - lastPos);

        eventSystem.RaycastAll (pointer, m_RaycastResultCache);
        var raycast = FindFirstRaycast (m_RaycastResultCache);
        pointer.pointerCurrentRaycast = raycast;
        m_RaycastResultCache.Clear();

        lastPos = screenPos;
        
        ProcessMove(pointer);
        ProcessMousePress(pointer);
        ProcessDrag(pointer);
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
            Debug.Log("DRAG");
            ExecuteEvents.Execute (pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
        }

        var targetGO = pointerEvent.pointerCurrentRaycast.gameObject;
        if(lastEventData != null)
        {
            pointerEvent.pointerEnter = lastEventData.pointerEnter;
            pointerEvent.pointerPress = lastEventData.pointerPress;
            pointerEvent.pointerDrag = lastEventData.pointerDrag;

        }
        HandlePointerExitAndEnter (pointerEvent, targetGO);
        lastEventData = pointerEvent;
    }

    private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
    {
        if (!useDragThreshold)
            return true;

        return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
    }

    protected void ProcessMousePress(PointerEventData pointerEvent)
    {
        var currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            pointerEvent.eligibleForClick = true;
            pointerEvent.delta = Vector2.zero;
            pointerEvent.dragging = false;
            pointerEvent.pressPosition = pointerEvent.position;
            pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;

            // search for the control that will receive the press
            // if we can't find a press handler set the press 
            // handler to be what would receive a click.
            var newPressed = ExecuteEvents.ExecuteHierarchy (currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler);

            // didnt find a press handler... search for a click handler
            if (newPressed == null)
                newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler> (currentOverGo);

            //Debug.Log("Pressed: " + newPressed);

            float time = Time.unscaledTime;

            if (newPressed == pointerEvent.lastPress)
            {
                var diffTime = time - pointerEvent.clickTime;
                if ( diffTime < 0.3f)
                    ++pointerEvent.clickCount;
                else
                    pointerEvent.clickCount = 1;

                pointerEvent.clickTime = time;
            }
            else
            {
                pointerEvent.clickCount = 1;
            }

            pointerEvent.pointerPress = newPressed;
            pointerEvent.rawPointerPress = currentOverGo;

            pointerEvent.clickTime = time;

            // Save the drag handler as well
            pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler> (currentOverGo);

            if (pointerEvent.pointerDrag != null)
                ExecuteEvents.Execute (pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);

            //	Debug.Log("Setting Drag Handler to: " + pointer.pointerDrag);

            // Selection tracking
            var selectHandlerGO = ExecuteEvents.GetEventHandler<ISelectHandler> (currentOverGo);
            eventSystem.SetSelectedGameObject (selectHandlerGO, pointerEvent);

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
			{
				//Debug.Log("Executing pressup on: " + pointer.pointerPress);
				ExecuteEvents.Execute (pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);

				//Debug.Log("KeyCode: " + pointer.eventData.keyCode);

				// see if we mouse up on the same element that we clicked on...
				var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler> (currentOverGo);

				// PointerClick and Drop events
				if (pointerEvent.pointerPress == pointerUpHandler && pointerEvent.eligibleForClick)
				{
                    Debug.Log("Clicked " + pointerEvent.pointerPress);
					ExecuteEvents.Execute (pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.ExecuteHierarchy (currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
				}

				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				pointerEvent.dragging = false;

				if (pointerEvent.pointerDrag != null)
					ExecuteEvents.Execute (pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);

				pointerEvent.pointerDrag = null;

				// redo pointer enter / exit to refresh state
				// so that if we moused over somethign that ignored it before
				// due to having pressed on something else
				// it now gets it.
				if (currentOverGo != pointerEvent.pointerEnter)
				{
					HandlePointerExitAndEnter (pointerEvent, null);
					HandlePointerExitAndEnter (pointerEvent, currentOverGo);
				}
			}
    }

    protected virtual void ProcessDrag(PointerEventData pointerEvent)
    {
        bool moving = pointerEvent.IsPointerMoving();

        if (moving && pointerEvent.pointerDrag != null
            && !pointerEvent.dragging
            && ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
        {
            ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
            pointerEvent.dragging = true;
        }

        // Drag notification
        if (pointerEvent.dragging && moving && pointerEvent.pointerDrag != null)
        {
            // Before doing drag we should cancel any pointer down state
            // And clear selection!
            if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
            {
                ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);

                pointerEvent.eligibleForClick = false;
                pointerEvent.pointerPress = null;
                pointerEvent.rawPointerPress = null;
            }
            ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
        }
    }

    public override bool IsPointerOverGameObject(int pointerId)
    {
        if (lastEventData != null)
            return lastEventData.pointerEnter != null;
        return false;
    }
}