using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardElement : MonoBehaviour
{
    void Start()
    {
        GetComponent<Draggable>().defaultParent = GameState.instance.cardContainer;
    }

    

    public Card card { get { return GetComponent<CardDisplay>().card; } }
}
