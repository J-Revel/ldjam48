using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftPopup : MonoBehaviour
{
    public ScenarioActionGiftConfig config { get { return GetComponent<GiftConfigSpawnable>().giftConfig; }}
    public CardDisplay cardPrefab;
    public Transform cardContainer;
    private Animator animator;
    public Text text;

    void Start()
    {
        animator = GetComponent<Animator>();
        text.text = config.description;
        foreach(Card card in config.GetCards())
        {
            CardDisplay newCard = Instantiate(cardPrefab, cardContainer);
            newCard.card = card;
        }
        foreach(ScenarioNode action in config.GetNodes())
        {
            Transform element = GameState.instance.SpawnActionElement(transform.position, action, GameState.SpawnableVersion.Pack);
            element.SetParent(cardContainer);
        }
    }

    void Update()
    {
        if(cardContainer.childCount == 0)
        {
            animator.SetTrigger("Close");
        }
    }

    public void OnPopupClosed()
    {
        Destroy(gameObject);
    }
}
