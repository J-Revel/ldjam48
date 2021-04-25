using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPopupSpawner : MonoBehaviour
{
    public ActionConfig config;
    public Transform spawnPosition;

    void SpawnPrefab()
    {
        Vector3 spawnPos = spawnPosition == null ? transform.position : spawnPosition.position;
        GameState.instance.SpawnActionElement(spawnPos, config, GameState.SpawnableVersion.Popup);
    }
}
