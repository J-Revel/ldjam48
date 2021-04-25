using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string title;
    public string description;
    public Sprite icon;
    public CardCategory category;
    public float duration;
    public Card depletionCard;
}
