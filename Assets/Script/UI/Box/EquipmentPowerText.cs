using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class EquipmentPowerText : MonoBehaviour
{
    public Text textATK;
    public Text textDEF;
    public Text textMATK;
    public Text textMDEF;
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
            textATK.text = e.power.ToString();
            textMATK.text = e.magicPower.ToString();
        }
        else if (e is Shield || e is Armor)
        {
            atk.gameObject.SetActive(false);
            matk.gameObject.SetActive(false);
            def.gameObject.SetActive(true);
            mdef.gameObject.SetActive(true);
            textDEF.text = e.power.ToString();
            textMDEF.text = e.magicPower.ToString();
        }else{
            atk.gameObject.SetActive(false);
            matk.gameObject.SetActive(false);
            def.gameObject.SetActive(false);
            mdef.gameObject.SetActive(false);
        }
    }

    public void render(GeneralEquipment e)
    {
        string type = e.type;
        if (type == "Sword" || type == "Axe" || type == "Staff" || type == "Wand" || type == "Bow" || type == "Dagger")
        {
            atk.gameObject.SetActive(true);
            matk.gameObject.SetActive(true);
            def.gameObject.SetActive(false);
            mdef.gameObject.SetActive(false);
            textATK.text = e.power.ToString();
            textMATK.text = e.magicPower.ToString();
        }
        else if (type == "Shield" || type == "Spellbook" || type == "Heavy Armor" || type == "Light Armor" || type == "Robe Armor")
        {
            atk.gameObject.SetActive(false);
            matk.gameObject.SetActive(false);
            def.gameObject.SetActive(true);
            mdef.gameObject.SetActive(true);
            textDEF.text = e.power.ToString();
            textMDEF.text = e.magicPower.ToString();
        }else{
            atk.gameObject.SetActive(false);
            matk.gameObject.SetActive(false);
            def.gameObject.SetActive(false);
            mdef.gameObject.SetActive(false);
        }
    }
}