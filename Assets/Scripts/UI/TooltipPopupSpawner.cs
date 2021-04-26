using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipPopupSpawner : MonoBehaviour
{
    public string text;
    public TextPopup popupPrefab;
    public TextPopup spawnedPopup;
    
    private void Start()
    {
        GameState.instance.backgroundClicked += OnBackgroundClicked;
        text = GetComponentInParent<Slot>().config.tooltip;
    }

    public void SpawnPopup()
    {
        spawnedPopup = Instantiate(popupPrefab, transform.position, Quaternion.identity, GameState.instance.cardContainer);
        spawnedPopup.description = text;
    }

    public void ClosePopup()
    {
        Destroy(spawnedPopup.gameObject);
        spawnedPopup = null;
    }

    public void OnBackgroundClicked()
    {
        if(spawnedPopup != null)
            Destroy(spawnedPopup.gameObject);
    }
}
