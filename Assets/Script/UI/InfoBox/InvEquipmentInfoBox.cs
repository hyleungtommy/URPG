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
    public Button sendToWarehouse;
    public Button sendToInventory;
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
        sendToInventory.gameObject.SetActive(Game.inventorySceneType.Equals("warehouse"));
        sendToWarehouse.gameObject.SetActive(Game.inventorySceneType.Equals("inventory"));
    }

    public void onClickDestroyItem()
    {
        slot.clear();
        Game.SaveGame();
        scene.render();
        hide();
    }

    public void OnClickTransferItem(){
        if(Game.inventorySceneType.Equals("inventory")){
            Game.inventory.transferTo(Game.town.Warehouse.ItemStorage, slot.getId());
        }else{
            Game.town.Warehouse.ItemStorage.transferTo(Game.inventory, slot.getId());
        }
        scene.render();
        hide();
    }
}
