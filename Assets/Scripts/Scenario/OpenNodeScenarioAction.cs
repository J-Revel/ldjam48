using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenarioCondition))]
public class OpenNodeScenarioAction : MonoBehaviour
{
    private ScenarioCondition cond;

    public ScenarioNode node;
    public GameState.SpawnableVersion version;

    private void Start()
    {
        cond = GetComponentInParent<ScenarioCondition>();
        cond.onConditionSuccess += SpawnNode;
    }

    public void SpawnNode(Vector3 position)
    {
        GameState.instance.SpawnActionElement(position, node, version);
    }

}
