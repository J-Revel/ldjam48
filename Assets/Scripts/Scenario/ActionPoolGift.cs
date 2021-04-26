using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenarioActionGiftConfig))]
public class ActionPoolGift : MonoBehaviour
{
    ScenarioActionGiftConfig giftConfig;
    public ScenarioNode[] possibleNodes;
    public int minNodeCount = 2;
    public int maxNodeCount = 4;

    public ScenarioVariable spawnCountVariable;

    void Start()
    {  
        giftConfig = GetComponent<ScenarioActionGiftConfig>(); 
        giftConfig.listNodesDelegate += AddNodesToGift;
    }

    private List<ScenarioNode> AddNodesToGift()
    {
        List<ScenarioNode> pool = new List<ScenarioNode>();
        List<ScenarioNode> result = new List<ScenarioNode>();
        foreach(ScenarioNode scenarioNode in possibleNodes)
        {
            pool.Add(scenarioNode);
        }

        int nodeCount = Random.Range(minNodeCount, maxNodeCount + 1);
        for(int i=0; i<nodeCount; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }
        spawnCountVariable.value += nodeCount;
        return result;
    }
}
