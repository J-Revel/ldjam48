using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPopup : MonoBehaviour
{
    public ActionConfig config { get { return GetComponent<ActionConfigSpawnable>().config; }}
    
    public Text description;
    public Slot slotPrefab;
    public Transform slotContainer;
    private List<Slot> spawnedSlots = new List<Slot>();
    public TimerWidget timer;
    private Animator animator;


    public void Start()
    {
        animator = GetComponent<Animator>();
        foreach(SlotConfig slotConfig in config.slots)
        {
            Slot slot = Instantiate(slotPrefab, slotContainer);
            slot.config = slotConfig;
            spawnedSlots.Add(slot);
            slot.slotFilledDelegate += OnSlotFilled;
        }
        description.text = config.text;
        if(config.duration > 0)
        {
            timer.startValue = config.duration;
            timer.timerEndDelegate += OnTimerEnd;
        }
        else
        {
            timer.gameObject.SetActive(false);
        }
    }

    private bool allSlotsFilled { get { 
        foreach(Slot childSlot in spawnedSlots)
        {
            if(childSlot.content == null)
            {
                return false;
            }
        }
        return true;
    }}

    private void OnSlotFilled(Slot slot)
    {
        if(allSlotsFilled)
            animator.SetTrigger("Close");
    }

    public void OnCloseAnimFinished()
    {
        float probabilityVal = Random.Range(0, 1.0f);
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
        }
        Destroy(gameObject);
    }

    public void OnTimerEnd()
    {
        animator.SetTrigger("Close");
    }
}
