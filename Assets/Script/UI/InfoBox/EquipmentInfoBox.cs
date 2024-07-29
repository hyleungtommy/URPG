using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class EquipmentInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public Text textDesc2;
    public ItemBox box;
    public EquipmentPowerText powerText;
    public Button btnEquip;
    public Button btnUnequip;
    public Text textError;
    public BattleCharacter character { get; set; }
    public StorageSlot slot { get; set; }
    public bool isEquippedItem { get; set; }
    public int equippedSlotId { get; set; }
    public EquipmentScene scene { get; set; }
    Equipment e;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void showContent()
    {
        if (isEquippedItem) e = base.obj as Equipment;
        else e = slot.getContainment() as Equipment;
        box.render(e);
        textHeader.text = e.fullName;
        textBasicInfo.text = e.getTypeName() + "\n" + "Req Lv.:" + e.reqLv;
        textDesc1.text = e.desc;
        if(e.enchantment != null && e.enchantment.effects.Count > 0){
            string enchantmentText = "";
            foreach(EnchantmentEffect effect in e.enchantment.effects){
                enchantmentText += effect.name + " Lv." + effect.lv + " : " + effect.desc;
            }
            textDesc2.text = enchantmentText;
        }else{
            textDesc2.text = "";
        }
        powerText.render(e);
        if (isEquippedItem)
        {
            btnEquip.gameObject.SetActive(false);
            btnUnequip.gameObject.SetActive(true);
            textError.gameObject.SetActive(false);
        }
        else
        {
            string errorMsg = canEquip();
            //Debug.Log("errorMsg" + errorMsg);
            if (errorMsg != "")
            {
                textError.text = errorMsg;
                btnEquip.gameObject.SetActive(false);
                textError.gameObject.SetActive(true);
            }
            else
            {
                textError.gameObject.SetActive(false);
                btnEquip.gameObject.SetActive(true);
            }
            btnUnequip.gameObject.SetActive(false);
        }

    }

    private string canEquip()
    {
        string errorMsg = "";
        if (!e.matchJobRestriction(character.job)) errorMsg = "Job mismatch";
        if (character.lv < e.reqLv) errorMsg = "Current level is lower than required level";
        return errorMsg;
    }

    public void onClickEquip()
    {
        character.equipmentManager.equip(slot, e);
        hide();
        Game.SaveGame();
        scene.render();
    }

    public void onClickUnequip()
    {
        character.equipmentManager.unequip(equippedSlotId);
        hide();
        Game.SaveGame();
        scene.render();
    }
}
