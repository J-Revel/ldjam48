using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCardBehaviours : MonoBehaviour
{
    public CardCategory blackCardCategory;
    private CardElement cardElement;    
    void Start()
    {
        cardElement = GetComponent<CardElement>();
        if(cardElement.card.category == blackCardCategory)
        {
            gameObject.AddComponent<BlackCardBehaviour>();
        }
    }


    void Update()
    {
        
    }
}
