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
    //for brewing crafting
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
    //for reinforcing
    public void render(ReinforceRecipe reinforceRecipe){
        for(int i = 0 ; i < requirementTexts.Length ; i++){
            if(i < reinforceRecipe.requirements.Count){
                requirementTexts[i].gameObject.SetActive(true);
                requirementTexts[i].render(reinforceRecipe.requirements[i]);
            }else{
                requirementTexts[i].gameObject.SetActive(false);
            }
        }
    }
    //for enchantment
    public void render(List<Requirement> requirements){
        for(int i = 0 ; i < requirementTexts.Length ; i++){
            if(i < requirements.Count){
                requirementTexts[i].gameObject.SetActive(true);
                requirementTexts[i].render(requirements[i]);
            }else{
                requirementTexts[i].gameObject.SetActive(false);
            }
        }
    }

}