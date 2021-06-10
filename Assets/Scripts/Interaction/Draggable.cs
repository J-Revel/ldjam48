using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DraggableChildRenderer
{
    public DraggableChildRenderer(int indexOffset, Renderer renderer)
    {
        this.indexOffset = indexOffset;
        this.renderer = renderer;
    }

    public int indexOffset;
    public Renderer renderer;
}

[RequireComponent(typeof(BoxCollider2D))]
public class Draggable : MonoBehaviour
{
    private int _sortIndex;
    public int sortIndex
    {
        get
        {
            return _sortIndex;
        }

        set
        {
            _sortIndex = value;
            foreach(DraggableChildRenderer childRenderer in childRenderers)
            {
                childRenderer.renderer.sortingOrder = _sortIndex * 100 + childRenderer.indexOffset;
            }
        }
    }
    
    public new BoxCollider2D collider;
    private List<DraggableChildRenderer> childRenderers = new List<DraggableChildRenderer>();

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            childRenderers.Add(new DraggableChildRenderer(0, renderer));
        }

        foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderers.Add(new DraggableChildRenderer(childRenderer.sortingOrder, childRenderer));
        }

        DraggableOrderSorter.instance.AddDraggableFront(this);
    }
}
