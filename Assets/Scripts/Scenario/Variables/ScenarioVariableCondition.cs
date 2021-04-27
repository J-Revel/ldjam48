using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Comparison
{
    Equals,
    Greater,
    Lower,
    GreaterOrEqual,
    LowerOrEqual,
}
public class ScenarioVariableCondition : MonoBehaviour
{
    public ScenarioVariable variable;
    public int expectedValue;
    public Comparison comparison = Comparison.Equals;
    
    void Start()
    {
        GetComponent<ScenarioCondition>().checkConditionDelegate += CheckCondition;
    }

    private bool CheckCondition(List<Card> cards)
    {
        switch(comparison)
        {
            case Comparison.Equals:
                return variable.value == expectedValue;
            case Comparison.Lower:
                return variable.value < expectedValue;
            case Comparison.LowerOrEqual:
                return variable.value <= expectedValue;
            case Comparison.Greater:
                return variable.value > expectedValue;
            case Comparison.GreaterOrEqual:
                return variable.value >= expectedValue;
        }
        return false;
    }
}
