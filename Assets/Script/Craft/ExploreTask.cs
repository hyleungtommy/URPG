using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG{
    public class ExploreTask:TimedTask{
        public ExploreSite exploreSite{set;get;}
    public ExploreTask(ExploreSite exploreSite):base(exploreSite.requireTime){
        this.exploreSite = exploreSite;
    }

        public override TaskCompleteMsg completeTask(){
        List<Item>items = new List<Item>();
        List<int>qty = new List<int>();
        for(int i = 0 ; i < exploreSite.obtainableItems.Length;i++){
            int appearChance = Mathf.FloorToInt(Random.Range(0,101));
            if(appearChance < exploreSite.obtainChance[i]){
                int amount = Mathf.FloorToInt(Random.Range(exploreSite.minAmount[i],exploreSite.maxAmount[i] + 1));
                items.Add(exploreSite.obtainableItems[i]);
                qty.Add(amount);
            }
        }
        Game.craftSkillManager.availableExploreTeam ++;
        return base.insertTaskResultItem(items,qty);
    }
    }
}