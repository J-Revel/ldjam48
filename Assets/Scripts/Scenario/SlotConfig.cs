using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SuckedCard
{
    Card card;
    float probability;
}

[CreateAssetMenu]
public class SlotConfig : ScriptableObject
{
    public CardCategory[] acceptedCategories;
    public Card[] acceptedCards;
    public SuckedCard[] failureCards;
}
