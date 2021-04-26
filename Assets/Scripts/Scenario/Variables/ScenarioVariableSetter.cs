using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioVariableSetter : MonoBehaviour
    {
     public ScenarioVariable variable;
    public int newValue;

    void Start()
    {
        GetComponent<ScenarioNode>().nodeClosedDelegate += OnNodeClosed;
    }

    void OnNodeClosed()
    {
        variable.value = newValue;
    }
}
