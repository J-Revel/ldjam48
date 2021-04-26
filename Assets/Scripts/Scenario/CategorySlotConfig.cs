using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenarioActionSlotConfig))]
public class CategorySlotConfig : MonoBehaviour
{
    private ScenarioActionSlotConfig slotConfig;
    public CardCategory category;
    public void Start()
    {
        slotConfig = GetComponent<ScenarioActionSlotConfig>();
        slotConfig.acceptCardDelegate += DoesAcceptCard;
    }

    public bool DoesAcceptCard(Card card)
    {
        return card.category == category;
    }
}