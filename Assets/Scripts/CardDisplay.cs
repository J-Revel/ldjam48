using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Image iconImage;
    public Card card;
    public GameObject timerContainer;
    public TimerWidget timerWidget;
    public Image bgImage;
    public bool useCategoryColors = true;

    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if(card != null)
        {
            if(useCategoryColors && card.category != null)
            {
                if(titleText != null)
                    titleText.color = card.category.textColor;
                if(bgImage != null)
                    bgImage.color = card.category.cardColor;
            }
            if(titleText != null)
            {
                titleText.text = card.title;
            }
            if(descriptionText != null)
            {
                descriptionText.text = card.description;

            }
            if(iconImage != null)
                iconImage.sprite = card.icon;
            if(card.duration > 0)
            {
                if(timerWidget != null)
                    timerWidget.startValue = card.duration;
                if(timerContainer != null)
                    timerContainer.SetActive(true);
            }
        }
    }

    public void SelectCard()
    {

    }

    public void DestroyCard()
    {
        if(card.depletionCard != null)
        {
            GameState.instance.SpawnCard(transform.position, card.depletionCard);
        }
        Destroy(gameObject);
    }
}
