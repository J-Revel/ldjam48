using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrderedDraggable
{
    public OrderedDraggable(int orderIndex, Draggable draggable)
    {
        this.orderIndex = orderIndex;
        this.draggable = draggable;
    }

    public int orderIndex;
    public Draggable draggable;
}

public class DraggableOrderSorter : MonoBehaviour
{
    public static DraggableOrderSorter instance;

    public int frontElementOrderIndex {
        get {
            return orderedDraggables.Count > 0 ? orderedDraggables[orderedDraggables.Count - 1].orderIndex : 0;
        }
    }
    public List<OrderedDraggable> orderedDraggables = new List<OrderedDraggable>();
    public Dictionary<Draggable, int> draggableIndex = new Dictionary<Draggable, int>();

    private void Awake()
    {
        instance = this;
    }

    public void AddDraggableFront(Draggable d)
    {
        d.sortIndex = orderedDraggables.Count;
        draggableIndex[d] = orderedDraggables.Count;
        orderedDraggables.Add(new OrderedDraggable(frontElementOrderIndex + 1, d));
    }

    public void SetToFront(Draggable d)
    {
        Debug.Log("--------------");
        Debug.Log("SET TO FRONT " + d);
        if(!draggableIndex.ContainsKey(d))
        {
            AddDraggableFront(d);
        }
        else
        {
            int index = draggableIndex[d];
            
            for(int i=index; i<orderedDraggables.Count - 1; i++)
            {
                OrderedDraggable od = orderedDraggables[i];
                od.draggable = orderedDraggables[i + 1].draggable;
                od.draggable.sortIndex = i;
                od.orderIndex = i;
                orderedDraggables[i] = od;
                draggableIndex[od.draggable] = i;
            }
            OrderedDraggable lastDraggable = orderedDraggables[orderedDraggables.Count - 1];
            lastDraggable.draggable = d;
            d.sortIndex = orderedDraggables.Count - 1;
            lastDraggable.orderIndex = orderedDraggables.Count - 1;
            orderedDraggables[orderedDraggables.Count - 1] = lastDraggable;
            draggableIndex[lastDraggable.draggable] = orderedDraggables.Count - 1;
            Debug.Log("--------------");
            for(int i=0; i<orderedDraggables.Count; i++)
            {
                OrderedDraggable od = orderedDraggables[i];
                Debug.Log(od.draggable + " " + i);
            }
            Debug.Log("===============");
        }
    }
}
