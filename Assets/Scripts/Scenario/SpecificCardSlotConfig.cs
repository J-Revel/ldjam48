using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenarioActionSlotConfig))]
public class SpecificCardSlotConfig : MonoBehaviour
{
    private ScenarioActionSlotConfig slotConfig;
    public Card[] cards;

    public void Start()
    {
        slotConfig = GetComponent<ScenarioActionSlotConfig>();
        slotConfig.acceptCardDelegate += DoesAcceptCard;
    }

    public bool DoesAcceptCard(Card card)
    {
        foreach(Card targetCard in cards)
        {
            if(card == targetCard)
                return true;
        }
        return false;
    }
}