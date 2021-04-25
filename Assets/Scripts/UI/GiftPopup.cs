using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPopup : MonoBehaviour
{
    public GiftConfig config { get { return GetComponent<GiftConfigSpawnable>().config; }}
    public CardDisplay cardPrefab;
    public Transform cardContainer;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        foreach(Card card in config.cards)
        {
            CardDisplay newCard = Instantiate(cardPrefab, cardContainer);
            newCard.card = card;
        }
        foreach(ActionConfig action in config.GetRandomActionSet())
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
