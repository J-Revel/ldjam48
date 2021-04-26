using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public Transform draggedObjectParent;
    public Transform cardContainer;

    public System.Action backgroundClicked;

    public CardElement cardPrefab;
    public ActionPopup actionPopupPrefab;
    public GiftPopup giftPopupPrefab;
    public ActionPopupSpawner actionCardPack;
    public GiftPopupSpawner giftCardPack;
    public Transform darkProjectilePrefab;

    public enum SpawnableVersion
    {
        Pack,
        Popup,
    }

[System.Serializable]
    public struct SpawnablePrefabs
    {
        public SpawnableVersion version;
        public Transform prefab;
    }

    public List<SpawnablePrefabs> giftPrefabs;
    public List<SpawnablePrefabs> actionPrefabs;

    public void OnBackgroundClicked()
    {
        backgroundClicked?.Invoke();
    }

    public void SpawnCard(Vector3 position, Card card)
    {
        Instantiate(cardPrefab, position, Quaternion.identity, cardContainer).GetComponent<CardDisplay>().card = card;
    }

    public void SpawnGiftElement(Vector3 position, ScenarioActionGiftConfig config, SpawnableVersion version)
    {
        foreach(SpawnablePrefabs prefab in this.giftPrefabs)
        {
            if(prefab.version == version)
            {
                Instantiate(prefab.prefab, position, Quaternion.identity, cardContainer).GetComponent<GiftConfigSpawnable>().giftConfig = config;
            }
        }
    }

    public Transform SpawnActionElement(Vector3 position, ScenarioNode config, SpawnableVersion version)
    {
        Transform result = null;
        foreach(SpawnablePrefabs prefab in this.actionPrefabs)
        {
            if(prefab.version == version)
            {
                result = Instantiate(prefab.prefab, position, Quaternion.identity, cardContainer);
                result.GetComponent<ActionConfigSpawnable>().scenarioNode = config;
            }
        }
        return result;
    }

    public void SpawnDarkProjectile(Vector3 position)
    {
        Instantiate(darkProjectilePrefab, position, Quaternion.identity, GameState.instance.cardContainer);
    }

    void Awake()
    {
        instance = this;
    }
}
