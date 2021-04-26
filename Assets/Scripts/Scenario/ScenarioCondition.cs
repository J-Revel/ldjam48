using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScenarioCondition : MonoBehaviour
{
    public bool checkSuccess = true;
    private ScenarioNode node;

    public delegate bool CheckConditionDelegate(List<Card> cards);
    public CheckConditionDelegate checkConditionDelegate;
    public System.Action<Vector3> onConditionSuccess;

    private void Start()
    {
        
    }

    public bool HandleNodeClosed(Vector3 position, List<Card> result)
    {
        if(checkConditionDelegate != null)
        {
            foreach(CheckConditionDelegate conditionDelegate in checkConditionDelegate.GetInvocationList())
            {
                if(!conditionDelegate(result))
                {
                    return false;
                }
            }
        }
        onConditionSuccess?.Invoke(position);
        return true;
    }
}
