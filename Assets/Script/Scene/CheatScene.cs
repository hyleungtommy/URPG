using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class CheatScene : MonoBehaviour
{
    public Toggle noCraftRequirement;
    public Toggle unlockAllRecipe;
    public Toggle skillNoCooldown;
    public HeaderCtrl header;
    // Start is called before the first frame update
    void Start()
    {
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickAddMoney(){
        Game.platinumCoin += 10000;
        Game.money += 1000000;
        header.render();
    }

    void Render(){
        noCraftRequirement.isOn = Param.noCraftRequirement;
        unlockAllRecipe.isOn = Param.unlockAllRecipe;
        skillNoCooldown.isOn = Param.skillNoCooldown;
    }

    public void OnValueChange(int i){
        switch (i){
            case 0:
                Param.noCraftRequirement = noCraftRequirement.isOn;
                SaveManager.saveValue(SaveKey.no_craft_requirement, Param.noCraftRequirement);
                break;
            case 1:
                Param.unlockAllRecipe = unlockAllRecipe.isOn;
                SaveManager.saveValue(SaveKey.unlock_all_recipe, Param.unlockAllRecipe);
                break;
            case 2:
                Param.skillNoCooldown = skillNoCooldown.isOn;
                SaveManager.saveValue(SaveKey.skill_no_cooldown, Param.skillNoCooldown);
                break;
        }
        SaveManager.save();
        Render();
    }
}
