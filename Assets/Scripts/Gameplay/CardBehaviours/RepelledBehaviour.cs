using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelledBehaviour : MonoBehaviour
{
    public Transform repeller;
    public float repelForce = 10;
    public float maxDistance = 100;
    private Draggable draggable;
    void Start()
    {
        draggable = GetComponent<Draggable>();
    }

    void Update()
    {
        Vector3 direction = transform.position - repeller.position;
        float intensity = Mathf.Clamp(1 - direction.magnitude / maxDistance, 0, 1);
        
        draggable.force = direction.normalized * repelForce * intensity * intensity;
    }
}
