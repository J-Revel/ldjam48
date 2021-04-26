using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScenarioNode : MonoBehaviour
{
    [TextArea(3, 10)]
    public string description;
    public float duration;
    public System.Action nodeClosedDelegate;

    private Transform slotConfigContainer;
    private Transform resultsConfigContainer;

    private void Awake()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "Slots")
                slotConfigContainer = transform.GetChild(i);
            if(transform.GetChild(i).name == "Results")
                resultsConfigContainer = transform.GetChild(i);

        }
    }

    public void OnNodeClosed(Vector3 position, List<Card> result, bool success)
    {
        nodeClosedDelegate?.Invoke();
        for(int i=0; i<resultsConfigContainer.transform.childCount; i++)
        {
            ScenarioCondition scenarioCondition = resultsConfigContainer.transform.GetChild(i).GetComponent<ScenarioCondition>();
            if(scenarioCondition != null && success == scenarioCondition.checkSuccess && scenarioCondition.HandleNodeClosed(position, result))
            {
                return;
            }
        }
    }

    public List<ScenarioActionSlotConfig> GetSlotConfigs()
    {
        List<ScenarioActionSlotConfig> result = new List<ScenarioActionSlotConfig>();
        for(int i=0; i<slotConfigContainer.transform.childCount; i++)
        {
            ScenarioActionSlotConfig slotConfig = slotConfigContainer.transform.GetChild(i).GetComponent<ScenarioActionSlotConfig>();
            result.Add(slotConfig);
        }
        return result;
    }
}
