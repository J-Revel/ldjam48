using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(defaultParent == null)
            defaultParent = transform.parent;
    }

    public override void OnDrag(PointerEventData data)
    {
        Vector3 targetPos = data.pointerCurrentRaycast.worldPosition;
        targetPos.z = 0;
        rectTransform.position += new Vector3(data.delta.x, data.delta.y, 0);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if(transform.parent == GameState.instance.cardContainer)
            transform.SetAsLastSibling();
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        dragStartPos = transform.position;
        dragStartParent = transform.parent;
        transform.SetParent(GameState.instance.draggedObjectParent, true);
        transform.SetAsLastSibling();
        dragStartDelegate?.Invoke(this);
        GameState.instance.backgroundClicked?.Invoke();
        dragChangeDelegate?.Invoke(true);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);
        dragChangeDelegate?.Invoke(false);
        
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
