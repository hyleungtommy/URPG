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
                requireDisplayable[i] = DB.QueryItem(requireItem[i].Substring(1,3));
            }else{
                requireDisplayable[i] = DB.QueryEquipment(requireItem[i].Substring(1,3));
            }
        }
        //Debug.Log("create recipe:" + resultItem);
        return new CraftRecipe(requireDisplayable,requireQty,requireMoney,requireLevel,resultItem);
    }
}

}
