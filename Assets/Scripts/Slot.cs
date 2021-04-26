using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public static List<Slot> activeSlots = new List<Slot>();

    public HoverColor hoverOverlay;
    public HoverColor hilightOverlay;
    public HoverColor[] lockOverlays;

    private bool hovered = false;
    public Color defaultHoverColor;
    public Color invalidHoverColor;
    public Color validHoverColor;
    public Draggable content;
    public ScenarioActionSlotConfig config;
    public System.Action<Slot> slotFilledDelegate;
    public System.Action<Slot> slotEmptiedDelegate;
    public bool isLocked;

    public void Start()
    {
        activeSlots.Add(this);
        Draggable.dragReleaseDelegate += OnDragRelease;
        Draggable.dragStartDelegate += OnDragStart;

    }

    public void OnDestroy()
    {
        Draggable.dragReleaseDelegate -= OnDragRelease;
        Draggable.dragStartDelegate -= OnDragStart;
        activeSlots.Remove(this);
    }

    public void OnDragStart(Draggable draggedElement)
    {
        
        if(DoesAcceptDraggable(draggedElement))
        {
            hilightOverlay?.OnHoverStart();
            hoverOverlay.hoverColor = validHoverColor;
        }
        else
        {
            hoverOverlay.hoverColor = invalidHoverColor;
        }
    }

    public void OnHoverStart()
    {
        hovered = true;
    }

    public void OnHoverEnd()
    {
        hovered = false;
    }

    public DragReleaseResult OnDragRelease(Draggable draggable)
    {
        hoverOverlay.hoverColor = defaultHoverColor;
        hilightOverlay.OnHoverEnd();
        if(hovered)
        {
            if(DoesAcceptDraggable(draggable))
            {
                return new DragReleaseResult{
                    state = DragReleaseState.Catched,
                    targetSlot = this,
                };
            }
            else return new DragReleaseResult{
                    state = DragReleaseState.Invalid,
                    targetSlot = this,
                };
        }
        return new DragReleaseResult{
            state = DragReleaseState.Free,
        };
    }
    
    public bool DoesAcceptDraggable(Draggable draggable)
    {
        if(isLocked)
            return false;
        CardElement cardElement = draggable.GetComponent<CardElement>();
        if(cardElement == null) return false;
        Card card = cardElement.card;
        if(card == null)
        {
            return false;
        }
        return config.DoesAcceptCard(card);
    }

    public void SuckCard(Draggable draggable)
    {
        Slot slot = draggable.dragStartParent.GetComponent<Slot>();
        if(content != null)
        {
            content.transform.position = draggable.dragStartPos;
            content.transform.SetParent(draggable.dragStartParent);
            if(slot != null && slot.DoesAcceptDraggable(draggable))
            {
                slot.content = content;
                slot.slotFilledDelegate?.Invoke(this);
            }
            
        }
        else if(slot != null)
        {
            slot.content = null;
            slot.slotEmptiedDelegate?.Invoke(this);
        }
        content = draggable;
        slotFilledDelegate?.Invoke(this);
        draggable.transform.position = transform.position;
        draggable.transform.SetParent(transform, true);
        hovered = false;
    }

    public void OnCardReleased()
    {
        content = null;
    }

    public void Lock()
    {
        isLocked = true;
        foreach(HoverColor hoverColor in lockOverlays)
        {
            hoverColor.OnHoverStart();
        }
    }
}
