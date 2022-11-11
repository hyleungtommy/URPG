using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class TaskCompleteMsg
{
    public List<Item>obtainItem{set;get;}
    public List<int>qty{set;get;}

    public TaskCompleteMsg(List<Item>obtainItem,List<int>qty){
        this.obtainItem = obtainItem;
        this.qty = qty;
    }

    public TaskCompleteMsg(Item obtainItem,int qty){
        this.obtainItem = new List<Item>();
        this.qty = new List<int>();
        this.obtainItem.Add(obtainItem);
        this.qty.Add(qty);
    }
}

}
