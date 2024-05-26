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
        Util.RemoveCraftItem(requirements, requireMoney, qty);
        if(resultItem is GeneralEquipment){
            Equipment resultEquipment = (resultItem as GeneralEquipment).toEquipment(Random.Range(0,5));
            if(resultEquipment is Accessory){
                resultEquipment.enchant();
            }
            Game.inventory.smartInsert(resultEquipment,qty);
            if(resultEquipment.equipmentType == Constant.EquipmentType.Wand || resultEquipment.equipmentType == Constant.EquipmentType.MagicBook || resultEquipment.equipmentType == Constant.EquipmentType.Staff || resultEquipment.equipmentType == Constant.EquipmentType.RobeArmor){
                Game.craftSkillManager.addExperience(SkillCraft.Type.arcaneCrafting, requireLevel);
            }else if (resultEquipment.equipmentType == Constant.EquipmentType.Accessory){
                Game.craftSkillManager.addExperience(SkillCraft.Type.jewelCrafting, requireLevel);
            }else{
                Game.craftSkillManager.addExperience(SkillCraft.Type.smithing, requireLevel);
            }
            taskCompleteMsg = new TaskCompleteMsg(resultEquipment,qty);
        }else{
            Game.craftSkillManager.addExperience(SkillCraft.Type.brewing, requireLevel);
            Game.inventory.smartInsert(resultItem as Item,qty);
            taskCompleteMsg = new TaskCompleteMsg(resultItem as Item,qty);
        }
        
        return taskCompleteMsg;
    }
}

}
