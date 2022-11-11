using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class CraftRecipe:Displayable
{
    public List<Requirement>requirements;
    public int requireMoney{set;get;}
    public int requireLevel{set;get;}
    public int craftTime{set;get;}
    public Displayable resultItem{set;get;}
    public CraftRecipe(Displayable[] requireItem,int[]requireQty,int requireMoney,int requireLevel,Displayable resultItem,int craftTime):base(resultItem.img){
        requirements = new List<Requirement>();
        for(int i = 0 ; i < requireItem.Length ; i++){
            requirements.Add(new Requirement(requireItem[i],requireQty[i],Requirement.Type.Item));
        }
        this.requireMoney = requireMoney;
        this.requireLevel = requireLevel;
        this.resultItem = resultItem;
        this.craftTime = craftTime;
    }
}

}
