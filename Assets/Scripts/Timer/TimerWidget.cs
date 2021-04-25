using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerWidget : MonoBehaviour
{
    public Animator animator;
    public Image radialImg;
    public float value;
    public float startValue = 60;
    public float alertThreshold = 10;
    public bool paused = false;
    public System.Action timerEndDelegate;

    private void Start()
    {
        value = startValue;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!paused)
            value -= Time.deltaTime;
        if(value <= 0)
        {
            timerEndDelegate?.Invoke();
        }
        radialImg.fillAmount = value / startValue;
        animator.SetBool("Alert", value < alertThreshold);
    }
}
