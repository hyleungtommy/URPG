using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class RequirementTextGroupCtrl: MonoBehaviour{
    public RequirementTextCtrl[] requirementTexts;
    public void render(CraftRecipe craftRecipe){
        for(int i = 0 ; i < requirementTexts.Length ; i++){
            if(i < craftRecipe.requirements.Count){
                requirementTexts[i].gameObject.SetActive(true);
                requirementTexts[i].render(craftRecipe.requirements[i]);
            }else{
                requirementTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void render(CraftRecipe craftRecipe,int qty){
        for(int i = 0 ; i < requirementTexts.Length ; i++){
            if(i < craftRecipe.requirements.Count){
                requirementTexts[i].gameObject.SetActive(true);
                requirementTexts[i].render(craftRecipe.requirements[i],qty);
            }else{
                requirementTexts[i].gameObject.SetActive(false);
            }
        }
    }


}