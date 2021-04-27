using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkProjectile : MonoBehaviour
{
    public float speed = 0;
    public float acceleration = 1;
    private Slot target;
    public float radius = 5;

    void Start()
    {
        UIParticleSystem.instance.emitters.Add(transform);
    }

    void OnDestroy()
    {
        UIParticleSystem.instance.emitters.Remove(transform);
    }

    void Update()
    {
        if(target != null && target.content != null)
            target = null;
        
            float closestDistance = Mathf.Infinity;
            for(int i=0; i<Slot.activeSlots.Count; i++)
            {
                float slotDistance = (Slot.activeSlots[i].transform.position - transform.position).sqrMagnitude;
                if(Slot.activeSlots[i].content == null && !Slot.activeSlots[i].isLocked && closestDistance > slotDistance)
                {
                    target = Slot.activeSlots[i];
                    closestDistance = slotDistance;
                    break;
                }
            }
        if(target != null)
        {
            float distance = (target.transform.position - transform.position).sqrMagnitude;
            if(distance < radius * radius)
            {
                Destroy(gameObject);
                target.Lock();
            }
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
            speed += Time.deltaTime * acceleration;
        }
        else
        {
        }
    }
}
