using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingTime : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] ClockController clock;

    private void Start()
    {
        DateTime currentTime = System.DateTime.Now;
        clock.hour = currentTime.Hour;
        clock.minute = currentTime.Minute / 5;
    }

    public void StartGame()
    {
        canvas.gameObject.SetActive(true);
        clock.gameObject.SetActive(true);
    }
}
