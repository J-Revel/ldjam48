using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioConditionVariableAdd : MonoBehaviour
{
    public ScenarioVariable variable;
    public int toAdd = 1;
    void Start()
    {
        GetComponent<ScenarioCondition>().onConditionSuccess += OnConditionSuccess;
        
    }

    void OnConditionSuccess(Vector3 pos)
    {
        variable.value += toAdd;
    }
}
