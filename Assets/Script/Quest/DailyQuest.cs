using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class DailyQuest:Displayable
{
    public bool accepted {get; set;}
    public int questId {get; set;}
    public string name {get; set;}
    public string desc {get; set;}
    public int requireLv {get; set;}
    public int maxLv {get; set;}
    public List<Requirement> requirements {get; set;}
    public QuestReward reward {get; set;}
    
    public DailyQuest():base(null){

    }

    public bool IsCompleted(){
        bool isCompleted = true;
        foreach(Requirement requirement in requirements){
            if(requirement.type == Requirement.Type.Enemy){
                if(requirement.currentQty < requirement.requireQty){
                    isCompleted = false;
                }
            }else{
                int qty = 0;
                if(requirement.requireItem is GeneralEquipment){
                    GeneralEquipment equip = requirement.requireItem as GeneralEquipment;
                    qty = Game.inventory.searchTotalQtyOfEquipmentInInventory(equip.id);
                }else{
                    Item item = requirement.requireItem as Item;
                    qty = Game.inventory.searchTotalQtyOfItemInInventory(item.id);
                }
                if(qty < requirement.requireQty){
                    isCompleted = false;
                }
            }
        }
        return isCompleted;
    }

    public void UpdateEnemyCount(string enemyName, int noOfEnemy){
        foreach(Requirement requirement in requirements){
            if(requirement.type == Requirement.Type.Enemy){
                if(requirement.requireEnemy.name.Equals(enemyName)){
                    if(requirement.currentQty + noOfEnemy > requirement.requireQty){
                        requirement.currentQty = noOfEnemy;
                    }else{
                        requirement.currentQty += noOfEnemy;
                    }
                }
            }
        }
    }


}

}