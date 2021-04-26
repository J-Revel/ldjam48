using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionMergeMode
{
    AND, OR
};
public class ScenarioActionSlotConfig : MonoBehaviour
{
    public string tooltip;
    public bool required = true;
    
    
    public ConditionMergeMode conditionMergeMode = ConditionMergeMode.AND;
    public delegate bool AcceptCardDelegate(Card card);

    public AcceptCardDelegate acceptCardDelegate;
    
    public bool DoesAcceptCard(Card card)
    {
        foreach(AcceptCardDelegate cardDelegate in acceptCardDelegate?.GetInvocationList())
        {
            if(!cardDelegate(card) && conditionMergeMode == ConditionMergeMode.AND)
            {
                return false;
            }

            if(cardDelegate(card) && conditionMergeMode == ConditionMergeMode.OR)
            {
                return true;
            }
        }
        return conditionMergeMode == ConditionMergeMode.AND;
    }

}
