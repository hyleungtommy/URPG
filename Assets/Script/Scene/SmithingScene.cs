using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SmithingScene : BasicScene{
    // Start is called before the first frame update
    public HeaderCtrl header;
    public GameObject itemBoxPrefab;
    public GameObject scrollViewContent;
    public CraftSkillCtrl supportSkillCtrl;
    public CraftEquipmentInfoBox craftEquipmentInfoBox;
    public CraftResultDialog craftResultDialog;
    int selectedSlotId;
    int selectedTabId;
    List<CraftRecipe>smthingRecipeList;
    List<CraftRecipe>arcaneCraftingRecipeList;
    List<CraftRecipe>jewelCraftingRecipeList;
    List<CraftRecipe>displayList;
    List<CraftEquipmentBox> boxList;
    void Start()
    {
        smthingRecipeList = new List<CraftRecipe>();
        arcaneCraftingRecipeList = new List<CraftRecipe>();
        jewelCraftingRecipeList = new List<CraftRecipe>();
        for(int i = 0 ; i < DB.craftRecipeEquipments.Length; i++){
            string equipType = (DB.craftRecipeEquipments[i].resultItem as GeneralEquipment).type;
            
            if(equipType == "Sword" || equipType == "Axe" || equipType == "Bow" || equipType == "Dagger" || equipType == "Shield" || equipType == "Heavy Armor" || equipType == "Light Armor"){
                if(Param.unlockAllRecipe || DB.craftRecipeEquipments[i].requireLevel <= Game.craftSkillManager.smithingSkill.lv){
                    smthingRecipeList.Add(DB.craftRecipeEquipments[i]);
                }
            }else if(equipType == "Staff" || equipType == "Wand" || equipType == "Spellbook" || equipType == "Robe Armor"){
                if(Param.unlockAllRecipe || DB.craftRecipeEquipments[i].requireLevel <= Game.craftSkillManager.arcaneCraftingSkill.lv){
                    arcaneCraftingRecipeList.Add(DB.craftRecipeEquipments[i]);
                }
            }else if(equipType == "Accessory"){
                if(Param.unlockAllRecipe || DB.craftRecipeEquipments[i].requireLevel <= Game.craftSkillManager.jewelCraftingSkill.lv){
                    jewelCraftingRecipeList.Add(DB.craftRecipeEquipments[i]);
                }
            }
        }
        craftEquipmentInfoBox.gameObject.SetActive(false);
        craftResultDialog.gameObject.SetActive(false);
        renderScrollView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderScrollView(){
        header.render();
        if(selectedTabId == 0){
            supportSkillCtrl.render(Game.craftSkillManager.smithingSkill);
            displayList = smthingRecipeList;
        }

        else if (selectedTabId == 1){
            supportSkillCtrl.render(Game.craftSkillManager.arcaneCraftingSkill);
            displayList = arcaneCraftingRecipeList;
        }
        else{
            supportSkillCtrl.render(Game.craftSkillManager.jewelCraftingSkill);
            displayList = jewelCraftingRecipeList;
        }
            
        int noOfBox = displayList.Count;

        Transform contentTran = scrollViewContent.transform;
        boxList = new List<CraftEquipmentBox>();
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;

            box = (GameObject)Instantiate(itemBoxPrefab, contentTran);
            CraftEquipmentBox boxCtrl = box.GetComponent<CraftEquipmentBox>();
            boxCtrl.render(displayList[i].resultItem);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
            boxList.Add(boxCtrl);
            
            
        }
    }

    public void onClickTab(int tabId){
        selectedTabId = tabId;
        renderScrollView();
    }

    public void onClickItem(int slotId)
    {
        selectedSlotId = slotId;
        craftEquipmentInfoBox.setContent(displayList[slotId]);
        craftEquipmentInfoBox.show();
    }

    public void onClickCraft(){
        craftEquipmentInfoBox.hide();
        TaskCompleteMsg taskCompleteMsg = displayList[selectedSlotId].craftItem(1);
        craftResultDialog.setTaskCompleteMsg(taskCompleteMsg);
        craftResultDialog.show();
        renderScrollView();
    }


}
