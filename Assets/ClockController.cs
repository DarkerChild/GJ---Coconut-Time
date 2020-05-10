using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ClockController : MonoBehaviour
{
    [SerializeField] Transform hourHand = null;
    [SerializeField] Transform minuteHand = null;

    [Range(0, 11)] public int hour = 0; //goes between 0 and 11
    [Range(0, 11)] public int minute = 0; // goes between 0 and 11

    String[] hourText = { "12", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11" };
    String[] minuteText = { "00", "05", "10","15","20","25","30","35","40","45","50","55" };

    private void Update()
    {
        UpdateClockHands();
    }

    public void AddHour()
    {
        hour = (hour == 11) ? 0 : hour+1;
    }

    public void RemoveHour()
    {
        hour = (hour == 0) ? 11 : hour-1;
    }

    public void AddMinutes()
    {
        if (minute == 11)
        {
            minute = 0;
            AddHour();
        }
        else
        {
            minute++;
        }
    }

    public void RemoveMinutes()
    {
        if (minute == 0)
        {
            minute = 11;
            RemoveHour();
        }
        else
        {
            minute--;
        }
    }

    public void UpdateClockHands()
    {
        minuteHand.localRotation = Quaternion.Euler(0f, 0f, minute * 30f);
        hourHand.localRotation = Quaternion.Euler(0f, 0f, (hour * 30f) + (minute * 2.5f));
    }
}
