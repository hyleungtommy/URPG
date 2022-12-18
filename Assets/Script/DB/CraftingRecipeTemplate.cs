using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG{
[Serializable]
public class CraftRecipeTemplate
{
    public string[]requireItem;
    public int[]requireQty;
    public int requireMoney;
    public int requireLevel;
    public CraftRecipe toCraftRecipe(Displayable resultItem){
        Displayable[] requireDisplayable = new Displayable[requireItem.Length];
        for(int i = 0 ; i < requireItem.Length;i++){
            if(requireItem[i].StartsWith("I")){
                ItemTemplate item = DB.items[Int32.Parse(requireItem[i].Substring(1,3))-1];
                requireDisplayable[i] = item.toItem();
            }else{
                GeneralEquipment equipment = DB.equipments[Int32.Parse(requireItem[i].Substring(1,3))-1];
                requireDisplayable[i] = equipment;
            }
        }
        //Debug.Log("create recipe:" + resultItem);
        return new CraftRecipe(requireDisplayable,requireQty,requireMoney,requireLevel,resultItem);
    }
}

}
