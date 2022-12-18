using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using UnityEngine.UI;

public class InvEquipmentInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public Text textDesc2;
    public ItemBox box;
    public EquipmentPowerText powerText;
    public InventoryScene scene;
    StorageSlot slot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStoageSlot(StorageSlot slot)
    {
        this.slot = slot;
    }

    protected override void showContent()
    {
        Equipment e = slot.getContainment() as Equipment;
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
    }

    public void onClickDestroyItem()
    {
        slot.clear();
        Game.saveGame();
        scene.render();
        hide();
    }
}
