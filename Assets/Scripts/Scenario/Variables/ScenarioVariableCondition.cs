using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioVariableCondition : MonoBehaviour
{
    public ScenarioVariable variable;
    public int expectedValue;
    
    void Start()
    {
        GetComponent<ScenarioCondition>().checkConditionDelegate += CheckCondition;
    }

    private bool CheckCondition(List<Card> cards)
    {
        return variable.value == expectedValue;
    }
}
