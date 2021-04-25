using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOnTimerEnd : MonoBehaviour
{
    public TimerWidget timer;
    private Animator animator;

    public string animTrigger;

    void Start()
    {
        animator = GetComponent<Animator>();    
        timer.timerEndDelegate += OnTimerEnd;
    }

    void OnTimerEnd()
    {
        animator.SetTrigger(animTrigger);
    }
}
