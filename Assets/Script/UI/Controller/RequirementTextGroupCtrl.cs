using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class RequirementTextGroupCtrl: MonoBehaviour{
    public RequirementTextCtrl[] requirementTexts;
    public bool allRequirementMatches = true;
    public void render(CraftRecipe craftRecipe){
        RenderTexts(craftRecipe.requirements, 1);
        CheckEnoughMoney(craftRecipe.requireMoney);
    }
    //for brewing crafting
    public void render(CraftRecipe craftRecipe,int qty){
        RenderTexts(craftRecipe.requirements, qty);
        CheckEnoughMoney(craftRecipe.requireMoney * qty);
    }
    //for reinforcing
    public void render(ReinforceRecipe reinforceRecipe){
        RenderTexts(reinforceRecipe.requirements, 1);
        CheckEnoughMoney(reinforceRecipe.requireMoney);
    }
    //for enchantment
    public void render(List<Requirement> requirements){
        RenderTexts(requirements, 1);
    }

    void RenderTexts(List<Requirement> requirements, int qty){
        allRequirementMatches = true;
        for(int i = 0 ; i < requirementTexts.Length ; i++){
            if(i < requirements.Count){
                requirementTexts[i].gameObject.SetActive(true);
                requirementTexts[i].Render(requirements[i],qty);
                if(!requirementTexts[i].requirementFulfilled){
                    allRequirementMatches = false;
                }
            }else{
                requirementTexts[i].gameObject.SetActive(false);
            }
        }
    }

    void CheckEnoughMoney(int money){
        if(Game.money < money){
            allRequirementMatches = false;
        }
    }

}