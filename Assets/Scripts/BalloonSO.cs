using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BalloonData")]
public class BalloonSO : ScriptableObject{
    public long firstStart;
    public long totalFallTime;
    public long startTime;
    public long popped;
    public long popTime => startTime + totalFallTime;


    public void DecrementFallTime(){
        if (totalFallTime > 1000){
            totalFallTime -= 1000;
        }
        else{
            totalFallTime -= 100;
        }

        totalFallTime = totalFallTime < 0 ? 0 : totalFallTime;
    }
    public void Load(String stored){
        var items = stored.Split(":");
        firstStart = long.Parse(items[0]);
        totalFallTime = long.Parse(items[1]);
        startTime = long.Parse(items[2]);
        popped = long.Parse(items[3]);
    }

    public String Store(){
        return $"{firstStart}:{totalFallTime}:{startTime}:{popped}";
    }
}
