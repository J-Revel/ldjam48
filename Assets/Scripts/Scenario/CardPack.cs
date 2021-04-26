using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardPack : ScriptableObject
{
    public string description;
    public Card[] cards;
    public ActionConfig[] actionsOnClose;
}