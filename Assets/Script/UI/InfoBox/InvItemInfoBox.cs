using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class InvItemInfoBox : BasicInfoBox
{
    // Start is called before the first frame update
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public ItemBox box;
    public InventoryScene scene;
    public Button buttonUse;
    StorageSlot slot;
    private Item item;
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
        item = slot.getContainment();
        textHeader.text = item.name;
        textBasicInfo.text = item.getTypeName() + "\n" + "Qty:" + slot.getQty();
        textDesc1.text = item.desc;
        box.render(item);
        buttonUse.gameObject.SetActive(item is ItemSpecial);
    }

    public void onClickDestroyItem()
    {
        slot.clear();
        Game.saveGame();
        scene.render();
        hide();
    }

    public void onClickUse(){
        slot.clear();
        (item as ItemSpecial).OnUse();
        Game.saveGame();
        scene.render();
        hide();
    }

}