using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class ReinforceRecipe:Displayable
{
    public int maxReinforceLv {get;set;}
    public int requireLevel {get;set;}
    public int powerIncrementPerLevel {get;set;}
    public int magicPowerIncrementPerLevel {get;set;}
    public int reinforceLv{get;set;}
    public int requireMoney{get{
        return requireMoneyStart + requireMoneyIncrement * reinforceLv;
    }}
    public List<Requirement>requirements{
        get{
            List<Requirement>reqs = new List<Requirement>();
            int i = 0;
            foreach(Requirement baseReq in baseRequirements){
                reqs.Add(new Requirement(baseReq.requireItem,baseReq.requireQty + requireQtyIncrement[i] * reinforceLv,Requirement.Type.Item));
                i++;
            }
            return reqs;
        }
    }
    private List<Requirement>baseRequirements {get;set;}
    private int requireMoneyStart {get;set;}
    private int[] requireQtyIncrement {get;set;}
    private int requireMoneyIncrement {get;set;}
    
    public ReinforceRecipe(
        Equipment equipment,int[]reqItems,int[]reqQtyStart,int requireMoneyStart,int requireLevel,int[] requireQtyIncrement,int requireMoneyIncrement,int maxReinforceLv,int powerIncrementPerLevel,int magicPowerIncrementPerLevel
    ):base(equipment.img){
        this.maxReinforceLv = maxReinforceLv;
        this.requireLevel = requireLevel;
        this.requireMoneyStart = requireMoneyStart;
        this.requireMoneyIncrement = requireMoneyIncrement;
        this.powerIncrementPerLevel = powerIncrementPerLevel;
        this.magicPowerIncrementPerLevel = magicPowerIncrementPerLevel;
        this.requireQtyIncrement = requireQtyIncrement;
        baseRequirements = new List<Requirement>();
        for(int i = 0 ; i < reqItems.Length;i++){
            ItemTemplate item = DB.items[reqItems[i] - 1];
            Requirement requirement = new Requirement(item.toItem(),reqQtyStart[i],Requirement.Type.Item);
            baseRequirements.Add(requirement);
        }
    }

    public int powerStatAfterReinforce(Equipment e){
        return e.power + powerIncrementPerLevel;
    }

    public int magicPowerStatAfterReinforce(Equipment e){
        return e.magicPower + magicPowerIncrementPerLevel;
    }

    public string onSave(){
        return reinforceLv.ToString();
    }
}
}