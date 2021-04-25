using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPopupSpawner : MonoBehaviour
{
    public GiftConfig config { get { return GetComponent<GiftConfigSpawnable>().config; } }
    public Transform spawnPosition;

    void SpawnPrefab()
    {
        Vector3 spawnPos = spawnPosition == null ? transform.position : spawnPosition.position;
        GameState.instance.SpawnGiftElement(spawnPos, config, GameState.SpawnableVersion.Popup);
    }
}
