using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class EquipmentScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public BattleMemberListCtrl battleMemberList;
    public ItemBox[] equippedItemBoxes;
    public EquipmentInfoBox infoBox;
    public Image imgCharacter;
    int selectedEquipmentId;
    StorageSlot[] availableEquipments;
    BattleCharacter character;
    // Start is called before the first frame update
    void Start()
    {

        // Game.inventory.smartInsert(DB.equipments[0].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[1].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[3].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[5].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[7].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[9].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[11].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[13].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[15].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[17].toEquipment(0), 1);
        //Game.inventory.smartInsert(DB.equipments[146].toEquipment(0), 1);

        header.render();
        infoBox.scene = this;
        infoBox.hide();
    }

    public override void onSelectCharacter(int id, BattleCharacter character)
    {
        //this.selectedEquipmentId = id;
        this.character = character;
        render();
    }

    public void render()
    {
        availableEquipments = Game.inventory.getOnlyEquipment();
        int noOfBox = availableEquipments.Length;
        Transform contentTran = scrollViewContent.transform;

        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            EquipmentBox boxCtrl = box.GetComponent<EquipmentBox>();
            boxCtrl.slot = availableEquipments[i];
            boxCtrl.render();
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }

        if (character != null)
        {
            equippedItemBoxes[0].render(character.equipmentManager.weaponEquipped);
            equippedItemBoxes[1].render(character.equipmentManager.shieldEquipped);
            equippedItemBoxes[2].render(character.equipmentManager.armorEquipped);
            equippedItemBoxes[3].render(character.equipmentManager.accessoryEquipped);
            imgCharacter.gameObject.SetActive(true);
            imgCharacter.sprite = character.faceImg[0];
        }
        else
        {
            imgCharacter.gameObject.SetActive(false);
            equippedItemBoxes[0].render(null);
            equippedItemBoxes[1].render(null);
            equippedItemBoxes[2].render(null);
            equippedItemBoxes[3].render(null);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickItem(int slotId)
    {
        if (availableEquipments[slotId] != null && availableEquipments[slotId].getContainment() != null)
        {
            this.selectedEquipmentId = availableEquipments[slotId].getContainment().id;
            infoBox.slot = availableEquipments[slotId];
            infoBox.isEquippedItem = false;
            infoBox.character = character;
            infoBox.show();
        }

    }

    public void onClickEquippedItem(int slotId)
    {
        Equipment e = null;
        if (slotId == 0) e = character.equipmentManager.weaponEquipped;
        else if (slotId == 1) e = character.equipmentManager.shieldEquipped;
        else if (slotId == 2) e = character.equipmentManager.armorEquipped;
        else if (slotId == 3) e = character.equipmentManager.accessoryEquipped;
        if (e != null)
        {
            this.selectedEquipmentId = e.id;
            infoBox.slot = null;
            infoBox.equippedSlotId = slotId;
            infoBox.setContent(e);
            infoBox.isEquippedItem = true;
            infoBox.character = character;
            infoBox.show();
        }
    }
}
