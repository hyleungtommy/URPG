using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class EnchantScene : MonoBehaviour
{
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public CraftSkillCtrl supportSkillCtrl;
    public EnchantInfoBox enchantInfoBox;
    public EnchantResultDialog enchantResultDialog;
    int selectedSlotId;
    List<StorageSlot>slotsWithEquipmentForEnchant;
    List<EquipmentBox> boxList;
    // Start is called before the first frame update
    void Start()
    {
        renderScrollView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderScrollView(){
        slotsWithEquipmentForEnchant = Game.inventory.searchEquipmentForEnchant();
        header.render();
        supportSkillCtrl.render(Game.craftSkillManager.enchantingSkill);
        int noOfBox = slotsWithEquipmentForEnchant.Count;
        Transform contentTran = scrollViewContent.transform;
        boxList = new List<EquipmentBox>();
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
            //Debug.Log(displayList[i].resultItem);
            boxCtrl.slot = slotsWithEquipmentForEnchant[i];
            boxCtrl.render();
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
            boxList.Add(boxCtrl);
        }
    }

    public void onClickItem(int slotId)
    {
        selectedSlotId = slotId;
        enchantInfoBox.setContent(slotsWithEquipmentForEnchant[slotId].getContainment());
        enchantInfoBox.show();
    }

    public void onClickCraft(){
        enchantInfoBox.hide();
        TaskCompleteMsg taskCompleteMsg = (slotsWithEquipmentForEnchant[selectedSlotId].getContainment() as Equipment).enchant();
        enchantResultDialog.setTaskCompleteMsg(taskCompleteMsg);
        enchantResultDialog.show();
        renderScrollView();
    }
}
