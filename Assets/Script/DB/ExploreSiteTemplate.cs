using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG{
[Serializable]
public class ExploreSiteTemplate
{
    public int id;
    public string name;
    public string desc;
    public string type;
    public int requireMoney;
    public int requireLevel;
    public int requireTime;
    public string[] obtainableItems;
    public int[] obtainChance;
    public int[] minAmount;
    public int[] maxAmount;

    public ExploreSite toExploreSite(){
        Sprite img;
        ExploreSite.Type estype;
        if(type == "Mining"){
            img = Resources.Load<Sprite>("UI/Icon/icon-gold-mine");
            estype = ExploreSite.Type.Mining;
        }else if(type == "Forging"){
            img = Resources.Load<Sprite>("UI/Icon/icon-forest");
            estype = ExploreSite.Type.Forging;
        }else{
            img = Resources.Load<Sprite>("UI/Icon/icon-plain");
            estype = ExploreSite.Type.Hunting;
        }
        Item[]obtainableItemList = new Item[obtainableItems.Length];
        for(int i = 0 ; i < obtainableItemList.Length ; i++){
            ItemTemplate item = DB.items[Int32.Parse(obtainableItems[i].Substring(1,3))-1];
            obtainableItemList[i] = item.toItem();
        }
        ExploreSite exploreSite = new ExploreSite(img,name);
        exploreSite.id = id;
        exploreSite.desc = desc;
        exploreSite.type = estype;
        exploreSite.obtainableItems = obtainableItemList;
        exploreSite.obtainChance = obtainChance;
        exploreSite.minAmount = minAmount;
        exploreSite.maxAmount = maxAmount;
        exploreSite.requireTime = requireTime;
        exploreSite.requireLevel = requireLevel;
        exploreSite.requireMoney = requireMoney;
        return exploreSite;
    }

}

}
