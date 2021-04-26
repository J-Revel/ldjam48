using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPopup : MonoBehaviour
{
    public ScenarioNode scenarioNode { get { return GetComponent<ActionConfigSpawnable>().scenarioNode; }}
    
    public Text description;
    public Slot slotPrefab;
    public Transform slotContainer;
    private List<Slot> spawnedSlots = new List<Slot>();
    public TimerWidget timer;
    private Animator animator;


    public void Start()
    {
        animator = GetComponent<Animator>();
        foreach(ScenarioActionSlotConfig slotConfig in scenarioNode.GetSlotConfigs())
        {
            Slot slot = Instantiate(slotPrefab, slotContainer);
            slot.config = slotConfig;
            spawnedSlots.Add(slot);
            slot.slotFilledDelegate += OnSlotFilled;
        }
        description.text = scenarioNode.description;
        if(timer != null)
        {
            if(scenarioNode.duration > 0)
            {
                timer.startValue = scenarioNode.duration;
                timer.timerEndDelegate += OnTimerEnd;
            }
            else
            {
                timer.gameObject.SetActive(false);
            }
        }
    }

    public bool allSlotsFilled { get { 
        foreach(Slot childSlot in spawnedSlots)
        {
            if(childSlot.content == null && childSlot.config.required)
            {
                return false;
            }
        }
        return true;
    }}

    private void OnSlotFilled(Slot slot)
    {
        
    }

    public void OnCloseAnimFinished()
    {
        List<Card> cards = new List<Card>();
        foreach(Slot childSlot in spawnedSlots)
        {
            Card card = null;
            if(childSlot.content != null)
            {
                card = childSlot.content.GetComponent<CardElement>().card;
            }
            cards.Add(card);
        }
        GetComponent<ActionConfigSpawnable>().scenarioNode.OnNodeClosed(transform.position, cards, allSlotsFilled);
        /*float probabilityVal = Random.Range(0, 1.0f);
        if(allSlotsFilled)
        {
            if(config.nextActionSuccess.IsInProbabilityRange(probabilityVal))
            {
                ActionConfig action = config.nextActionSuccess.GetRandomAction();
                Transform element = GameState.instance.SpawnActionElement(transform.position, action, GameState.SpawnableVersion.Popup);
            }
            else if(config.successGift != null)
            {
                GameState.instance.SpawnGiftElement(transform.position, config.successGift, GameState.SpawnableVersion.Pack);
            }
        }
        else
        {
            if(config.nextActionFailure.IsInProbabilityRange(probabilityVal))
            {
                ActionConfig action = config.nextActionFailure.GetRandomAction();
                Transform element = GameState.instance.SpawnActionElement(transform.position, action, GameState.SpawnableVersion.Popup);
            }
            else if(config.failureGift != null)
            {
                GameState.instance.SpawnGiftElement(transform.position, config.failureGift, GameState.SpawnableVersion.Pack);
            }
        }*/
        Destroy(gameObject);
    }

    public void OnTimerEnd()
    {
        animator.SetTrigger("Close");
    }

    public void ActionValidated()
    {
        animator.SetTrigger("Close");
    }
}
