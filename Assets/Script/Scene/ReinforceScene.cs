using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ReinforceScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public CraftSkillCtrl supportSkillCtrl;
    public ReinforceInfoBox reinforceInfoBox;
    public CraftResultDialog craftResultDialog;
    int selectedSlotId;
    List<StorageSlot>slotsWithEquipmentForReinforce;
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
        slotsWithEquipmentForReinforce = Game.inventory.searchEquipmentForReinforce();
        header.render();
        supportSkillCtrl.render(Game.craftSkillManager.reinforcingSkill);
        int noOfBox = slotsWithEquipmentForReinforce.Count;
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
            boxCtrl.slot = slotsWithEquipmentForReinforce[i];
            boxCtrl.render();
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
            boxList.Add(boxCtrl);
        }
    }

    public void onClickItem(int slotId)
    {
        selectedSlotId = slotId;
        reinforceInfoBox.setContent(slotsWithEquipmentForReinforce[slotId].getContainment());
        reinforceInfoBox.show();
    }

    public void onClickCraft(){
        reinforceInfoBox.hide();
        TaskCompleteMsg taskCompleteMsg = (slotsWithEquipmentForReinforce[selectedSlotId].getContainment() as Equipment).reinforce();
        craftResultDialog.setTaskCompleteMsg(taskCompleteMsg);
        craftResultDialog.show();
        renderScrollView();
    }
}
