using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWeight : MonoBehaviour
{
    public float minWeight = 0.1f;
    public float maxWeight = 1;
    public float freq = 1;
    private Draggable draggable;

    void Start()
    {
        draggable = GetComponent<Draggable>();
    }

    void Update()
    {
        draggable.weight = (1 + Mathf.Sin(Time.time * Mathf.PI / freq)) / 2 * ( maxWeight - minWeight) + minWeight;
    }
}
