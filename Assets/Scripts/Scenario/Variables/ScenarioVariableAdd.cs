using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioVariableAdd : MonoBehaviour
{
    public ScenarioVariable variable;
    public int delta;

    void Start()
    {
        GetComponent<ScenarioNode>().nodeClosedDelegate += OnNodeClosed;
    }

    void OnNodeClosed()
    {
        variable.value += delta;
    }
}
