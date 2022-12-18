using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class InventoryScene : BasicScene
{
    public GameObject invContent;
    public GameObject invBoxPrefab;
    public HeaderCtrl header;
    public InvItemInfoBox itemInfoBox;
    public InvEquipmentInfoBox equipmentInfoBox;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        render();
    }

    public void render()
    {
        int noOfBox = Game.inventory.getSize();
        Transform contentTran = invContent.transform;
        GameObject invBox;
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            invBox = (GameObject)Instantiate(invBoxPrefab, contentTran);
            InvBox invBoxCtrl = invBox.GetComponent<InvBox>();
            invBoxCtrl.setStorageSlot(Game.inventory.getSlot(i));
            invBoxCtrl.render();
            invBox.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
        itemInfoBox.hide();
        equipmentInfoBox.hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickItem(int slotId)
    {
        //Debug.Log(Game.inventory.getSlot(slotId).getContainment());
        if (Game.inventory.getSlot(slotId) != null && Game.inventory.getSlot(slotId).getContainment() != null)
        {
            if(Game.inventory.getSlot(slotId).getContainment() is Equipment){
                equipmentInfoBox.setStoageSlot(Game.inventory.getSlot(slotId));
                equipmentInfoBox.show();
            }else{
                itemInfoBox.setStoageSlot(Game.inventory.getSlot(slotId));
                itemInfoBox.show();
            }
        }
    }

}
