using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPopupSpawner : MonoBehaviour
{
    public CardDisplay cardDisplay;
    public CardDisplay popupPrefab;
    public CardDisplay spawnedPopup;
    
    private void Start()
    {
        GameState.instance.backgroundClicked += OnBackgroundClicked;
    }

    public void SpawnPopup()
    {
        spawnedPopup = Instantiate(popupPrefab, transform.position, Quaternion.identity, GameState.instance.cardContainer);
        spawnedPopup.card = cardDisplay.card;
    }

    public void OnBackgroundClicked()
    {
        if(spawnedPopup != null)
            Destroy(spawnedPopup.gameObject);
    }
}
