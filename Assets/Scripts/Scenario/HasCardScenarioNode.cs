using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenarioCondition))]
public class HasCardScenarioNode : MonoBehaviour
{
    private ScenarioCondition cond;
    public Card checkedCard;

    private void Start()
    {
        cond = GetComponentInParent<ScenarioCondition>();
        cond.checkConditionDelegate += CheckCondition;
    }

    public bool CheckCondition(List<Card> result)
    {
        foreach(Card card in result)
        {
            if(checkedCard == card)
            {
                return true;
            }
        }
        return false;
    }

}
