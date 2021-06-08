using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;

public enum DragReleaseState
{
    Free,
    Catched,
    Invalid,
}

public struct DragReleaseResult
{
    public DragReleaseState state;
    public Slot targetSlot;
}

public class Draggable : EventTrigger
{
    private RectTransform rectTransform;
    public static System.Action<Draggable> dragStartDelegate;

    public delegate DragReleaseResult DragReleaseDelegate(Draggable draggable);
    public static DragReleaseDelegate dragReleaseDelegate;

    public System.Action<bool> dragChangeDelegate;

    public Vector3 dragStartPos;
    public Transform dragStartParent;
    private Slot dragStartAttachedSlot;
    public Transform defaultParent;

    public Vector3 force;
    public float weight = 1;

    private Vector2 releaseMousePos;
    private bool dragged = false;
    public float dragSpeed = 1;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(defaultParent == null)
            defaultParent = transform.parent;
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        transform.position += force * Time.deltaTime;
        if(dragged)
        {
            
        }
    }

    public override void OnDrag(PointerEventData data)
    {
        Vector3 targetPos = data.pointerCurrentRaycast.worldPosition;
        targetPos.z = 0;
        
        rectTransform.position += new Vector3(data.delta.x, data.delta.y, 0);
        releaseMousePos += data.delta;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if(transform.parent == GameState.instance.cardContainer)
            transform.SetAsLastSibling();
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        // Cursor.visible = false;
        dragStartPos = transform.position;
        releaseMousePos = Mouse.current.position.ReadValue();
        dragStartParent = transform.parent;
        transform.SetParent(GameState.instance.draggedObjectParent, true);
        transform.SetAsLastSibling();
        dragStartDelegate?.Invoke(this);
        GameState.instance.backgroundClicked?.Invoke();
        dragChangeDelegate?.Invoke(true);
        dragged = true;
        MouseCursor.instance.speedScale = dragSpeed;
    }

    public override void OnEndDrag(PointerEventData data)
    {
        dragged = false;
        //SetCursorPos((int)releaseMousePos.x, (int)releaseMousePos.y);
        Cursor.lockState = CursorLockMode.None;
        //Mouse.current.WarpCursorPosition(releaseMousePos);
        // Cursor.visible = true;
        base.OnEndDrag(data);
        dragChangeDelegate?.Invoke(false);
        MouseCursor.instance.speedScale = 1;
        
        if(dragReleaseDelegate != null)
        {

            foreach(DragReleaseDelegate releaseDelegate in dragReleaseDelegate.GetInvocationList())
            {
                DragReleaseResult releaseResult = releaseDelegate(this);
                switch(releaseResult.state)
                {
                    case DragReleaseState.Free:
                        break;
                    case DragReleaseState.Catched:
                        releaseResult.targetSlot.SuckCard(this);
                        return;
                    case DragReleaseState.Invalid:
                        transform.position = dragStartPos;
                        transform.parent = dragStartParent;
                        return;
                }
            }
        }
        transform.SetParent(GameState.instance.cardContainer, true);
        if(dragStartParent != null)
        {
            Slot parentSlot = dragStartParent.GetComponent<Slot>();
            if(parentSlot != null)
                parentSlot.OnCardReleased();
        }
    }
}
