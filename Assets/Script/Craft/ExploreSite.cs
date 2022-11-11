using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class ExploreSite:Displayable
{
    public int id {get;set;}
    public enum Type{
        Mining,Forging,Hunting
    }
    public string name {get;set;}
    public string desc {get;set;}
    public Type type {get;set;}
    public int requireMoney {get;set;}
    public int requireLevel {get;set;}
    public int requireTime {get;set;}
    public Item[] obtainableItems {get;set;}
    public int[] obtainChance{get;set;}
    public int[] minAmount{get;set;}
    public int[] maxAmount{get;set;}
    public ExploreTask exploreTask {get;set;}

    public ExploreSite(Sprite img,string name):base(img){
        this.name = name;
    }

    public void startExplore(){
        exploreTask = new ExploreTask(this);
    }
}

}
