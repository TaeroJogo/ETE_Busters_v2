using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


class TimerData {

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

  void Update(){
      foreach (var timer in timers)  {
          if(timer.isActive) {
            timer.timeStamp -= Time.deltaTime;

            if (timer.timeStamp <= timer.endTime)
            {
              timer.timerCallback();
              if(timer.loopTimer) {
                timer.timeStamp = timer.initialTime;
              }
              else {
                timer.isActive = false;
              }
            }
          }
      }
    }
    
  public void CreateTimer(string tag, float start, float end, bool loop, Action callbackMethod) {
    timers.Add(new TimerData {timerTag = tag, initialTime = start, timeStamp = start, endTime = end, timerCallback = callbackMethod, loopTimer = loop});
  }

  private TimerData FindTimer(string tag){
    var t = new TimerData();
    foreach (var timer in timers)  {
      if(timer.timerTag == tag) {
        t = timer;
      }
    }
    return t;
}

  public void PauseTimer(string tag){
    FindTimer(tag).isActive = false;
  }

  public void ResumeTimer(string tag){
   FindTimer(tag).isActive = true;
  }

  public void CancelTimer(string tag){
    timers.Remove(FindTimer(tag));
  }

  public float GetTimeStamp(string tag){
    return FindTimer(tag).timeStamp;
  }
}
