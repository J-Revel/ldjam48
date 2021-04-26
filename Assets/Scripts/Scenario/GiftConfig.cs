using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeighedActionConfig
{
    public ActionConfig action;
    public float weight;
}

[System.Serializable]
public struct ActionConfigPool
{
    public float probability, probabilityOffset;
    public WeighedActionConfig[] possibleActions;

    public bool IsInProbabilityRange(float value)
    {
        return value >= probabilityOffset && value < probabilityOffset + probability;
    }

    public ActionConfig GetRandomAction()
    {
        float weightsSum = 0;
        foreach(WeighedActionConfig action in possibleActions)
        {
            if(action.weight == 0)
            {
                Debug.LogError("ERROR : weight of 0 in action config : " + this);
            }
            weightsSum += action.weight;
        }
        float diceValue = Random.Range(0, weightsSum);
        
        foreach(WeighedActionConfig action in possibleActions)
        {
            diceValue -= action.weight;
            if(diceValue <= 0)
            {
                return action.action;
            }
        }
        return null;
    }
}

[CreateAssetMenu]
public class GiftConfig : ScriptableObject
{
    public string description;
    public Card[] cards;
    public ActionConfigPool[] actions;

    public List<ActionConfig> GetRandomActionSet()
    {
        List<ActionConfig> result = new List<ActionConfig>();
        foreach(ActionConfigPool actionPool in actions)
        {
            float probValue = Random.Range(0, 1.0f);
            if(actionPool.IsInProbabilityRange(probValue))
            {
                result.Add(actionPool.GetRandomAction());
            }
        }
        return result;
    }
}
