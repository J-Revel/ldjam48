using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioActionRedirection : MonoBehaviour
{
    private ScenarioActionGiftConfig giftConfig;
    private OpenNodeScenarioAction openNodeScenarioAction;
    public ScenarioVariable variable;
    public int value = 3;
    public ScenarioNode[] targets;
    private ScenarioNode[] initialTargets;

    public void Start()
    {
        giftConfig = GetComponent<ScenarioActionGiftConfig>();
        openNodeScenarioAction = GetComponent<OpenNodeScenarioAction>();
        if(giftConfig != null)
            initialTargets = giftConfig.nodes;
        else if(openNodeScenarioAction != null)
        {
            initialTargets = new ScenarioNode[]{openNodeScenarioAction.node};
        }
    }

    void Update()
    {
        if(variable.value > value)
        {
            if(openNodeScenarioAction != null)
                openNodeScenarioAction.node = targets[0];
            if(giftConfig != null)
               giftConfig.nodes = targets;
        }
        else
        {
            if(openNodeScenarioAction != null)
                openNodeScenarioAction.node = initialTargets[0];
            if(giftConfig != null)
               giftConfig.nodes = initialTargets;
        }
    }
}
