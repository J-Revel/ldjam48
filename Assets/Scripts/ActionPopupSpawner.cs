using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPopupSpawner : MonoBehaviour
{
    public Transform spawnPosition;

    void SpawnPrefab()
    {
        Vector3 spawnPos = spawnPosition == null ? transform.position : spawnPosition.position;
        GameState.instance.SpawnActionElement(spawnPos, GetComponentInParent<ActionConfigSpawnable>().scenarioNode, GameState.SpawnableVersion.Popup);
    }
}
