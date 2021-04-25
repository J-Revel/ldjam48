using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionConfig : ScriptableObject
{
    public string text;
    public float duration;
    public SlotConfig[] slots;
    public ActionConfigPool nextActionSuccess;
    public ActionConfigPool nextActionFailure;
    public GiftConfig successGift;
    public GiftConfig failureGift;

}
