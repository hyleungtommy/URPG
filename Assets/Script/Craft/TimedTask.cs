using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG{
public abstract class TimedTask
{
    public DateTime startTime{set;get;}
    public int taskTime{set;get;}
    public TimedTask(int taskTime){
        startTime = DateTime.Now;
        this.taskTime = taskTime;
    }
    public string getRemainingTimeFormatted(){
        DateTime now = DateTime.Now;
        TimeSpan ts = new TimeSpan(0,0,taskTime);
        TimeSpan remainingTime = startTime.AddSeconds(taskTime).Subtract(now);
        if(remainingTime.Ticks > 0)
            return new DateTime(remainingTime.Ticks).ToString("HH:mm:ss");
        else
            return "00:00:00";
    }

    public bool isTaskCompleted(){
        DateTime now = DateTime.Now;
        TimeSpan ts = new TimeSpan(0,0,taskTime);
        TimeSpan remainingTime = startTime.AddSeconds(taskTime).Subtract(now);
        return (remainingTime.Ticks <= 0);
    }

    public int getRemainingTimeSecond(){
        DateTime now = DateTime.Now;
        TimeSpan ts = new TimeSpan(0,0,taskTime);
        TimeSpan remainingTime = startTime.AddSeconds(taskTime).Subtract(now);
        return (int)remainingTime.TotalSeconds;
    }

    public TaskCompleteMsg insertTaskResultItem(List<Item>items,List<int>qty){
        for(int i = 0 ; i < items.Count; i++){
            Game.inventory.smartInsert(items[i],qty[i]);
        }
        return new TaskCompleteMsg(items,qty);
    }

    public TaskCompleteMsg insertTaskResultItem(Item item,int qty){
        Game.inventory.smartInsert(item,qty);
        return new TaskCompleteMsg(item,qty);
    }

    public abstract TaskCompleteMsg completeTask();

}

}
