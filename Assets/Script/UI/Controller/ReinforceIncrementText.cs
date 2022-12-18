using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class ReinforceIncrementText : MonoBehaviour
{
    public Text textPower;
    public Text textMagicPower;
    public Text textPowerInc;
    public Text textMagicPowerInc;
    public Text textReinLv;
    public GameObject atk;
    public GameObject def;
    public GameObject matk;
    public GameObject mdef;

    public void render(Equipment e)
    {
        if (e is Weapon)
        {
            atk.gameObject.SetActive(true);
            matk.gameObject.SetActive(true);
            def.gameObject.SetActive(false);
            mdef.gameObject.SetActive(false);
            textPower.text = e.power.ToString() + " -> " + (e.reinforceRecipe.powerStatAfterReinforce(e));
            textMagicPower.text = e.magicPower.ToString() + " -> " + (e.reinforceRecipe.magicPowerStatAfterReinforce(e));
            textMagicPowerInc.text = "(+" + e.reinforceRecipe.magicPowerIncrementPerLevel + ")";
            textPowerInc.text = "(+" + e.reinforceRecipe.powerIncrementPerLevel + ")";
            textReinLv.text = "Stat:   +" + e.reinforceRecipe.reinforceLv + " -> +" + (e.reinforceRecipe.reinforceLv + 1) ;
        }
        else if (e is Shield || e is Armor)
        {
            atk.gameObject.SetActive(false);
            matk.gameObject.SetActive(false);
            def.gameObject.SetActive(true);
            mdef.gameObject.SetActive(true);
            textPower.text = e.power.ToString() + " -> " + (e.reinforceRecipe.powerStatAfterReinforce(e));
            textMagicPower.text = e.magicPower.ToString() + " -> " + (e.reinforceRecipe.magicPowerStatAfterReinforce(e));
            textMagicPowerInc.text = "(+" + e.reinforceRecipe.magicPowerIncrementPerLevel + ")";
            textPowerInc.text = "(+" + e.reinforceRecipe.powerIncrementPerLevel + ")";
            textReinLv.text = "Stat:   +" + e.reinforceRecipe.reinforceLv + " -> +" + (e.reinforceRecipe.reinforceLv + 1) ;
        }
    }
}