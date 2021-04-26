using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenarioCondition))]
[RequireComponent(typeof(ScenarioActionGiftConfig))]
public class SpawnGiftScenarioAction : MonoBehaviour
{
    private ScenarioCondition cond;

    private ScenarioActionGiftConfig gift;
    public GameState.SpawnableVersion version;

    private void Start()
    {
        gift = GetComponentInChildren<ScenarioActionGiftConfig>();
        cond = GetComponentInParent<ScenarioCondition>();
        cond.onConditionSuccess += SpawnGift;
    }

    public void SpawnGift(Vector3 position)
    {
        GameState.instance.SpawnGiftElement(position, gift, version);
    }

}
