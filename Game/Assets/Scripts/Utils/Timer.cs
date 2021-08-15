using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


class TimerData
{

    public string timerTag;

    public float initialTime;
    public float timeStamp;
    public float endTime;

    public bool isActive = true;
    public bool isDone = false;

    public Action timerCallback;
    public bool loopTimer;
}

public class Timer : MonoBehaviour
{

    List<TimerData> timers = new List<TimerData>();

    void Update()
    {
        for (var i = 0; i < timers.Count; i++)
        {
            if (timers[i].isActive)
            {
                timers[i].timeStamp -= Time.deltaTime;

                if (timers[i].timeStamp <= timers[i].endTime)
                {
                    timers[i].timerCallback();
                    if (timers[i].loopTimer)
                    {
                        timers[i].timeStamp = timers[i].initialTime;
                    }
                    else
                    {
                        timers[i].isActive = false;
                    }
                }
            }
        }
    }

    public void CreateTimer(string tag, float start, float end, bool loop, Action callbackMethod)
    {
        timers.Add(new TimerData { timerTag = tag, initialTime = start, timeStamp = start, endTime = end, timerCallback = callbackMethod, loopTimer = loop });
    }

    private TimerData FindTimer(string tag)
    {
        var t = new TimerData();
        foreach (var timer in timers)
        {
            if (timer.timerTag == tag)
            {
                t = timer;
            }
        }
        return t;
    }

    public void PauseTimer(string tag)
    {
        FindTimer(tag).isActive = false;
    }

    public void ResumeTimer(string tag)
    {
        FindTimer(tag).isActive = true;
    }

    public void CancelTimer(string tag)
    {
        timers.Remove(FindTimer(tag));
    }

    public float GetTimeStamp(string tag)
    {
        return FindTimer(tag).timeStamp;
    }
}
