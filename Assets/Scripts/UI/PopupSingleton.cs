using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSingleton : MonoBehaviour
{
    public string popupId;
    private static Dictionary<string, PopupSingleton> instances = new Dictionary<string, PopupSingleton>();
    void Start()
    {
        if(instances.ContainsKey(popupId) && instances[popupId] != null)
        {
            Destroy(instances[popupId].gameObject);
        }
        instances[popupId] = this;
    }
}
