using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class CraftRecipe:Displayable
{
    public List<Requirement>requirements;
    public int requireMoney{set;get;}
    public int requireLevel{set;get;}
    public Displayable resultItem{set;get;}
    public CraftRecipe(Displayable[] requireItem,int[]requireQty,int requireMoney,int requireLevel,Displayable resultItem):base(resultItem.img){
        requirements = new List<Requirement>();
        for(int i = 0 ; i < requireItem.Length ; i++){
            requirements.Add(new Requirement(requireItem[i],requireQty[i],Requirement.Type.Item));
        }
        this.requireMoney = requireMoney;
        this.requireLevel = requireLevel;
        this.resultItem = resultItem;
    }

    public TaskCompleteMsg craftItem(int qty){
        TaskCompleteMsg taskCompleteMsg;
        if(resultItem is GeneralEquipment){
            Equipment resultEquipment = (resultItem as GeneralEquipment).toEquipment(Random.Range(0,5));
            if(resultEquipment is Accessory){
                resultEquipment.enchant();
            }
            Game.inventory.smartInsert(resultEquipment,qty);
            taskCompleteMsg = new TaskCompleteMsg(resultEquipment,qty);
        }else{
            Game.inventory.smartInsert(resultItem as Item,qty);
            taskCompleteMsg = new TaskCompleteMsg(resultItem as Item,qty);
        }
        
        return taskCompleteMsg;
    }
}

}
