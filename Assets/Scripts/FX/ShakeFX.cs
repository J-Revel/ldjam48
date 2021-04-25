using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFX : MonoBehaviour
{
    public float value;
    public float maxShake;
    
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)) * value * maxShake;
    }
}
