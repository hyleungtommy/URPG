using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class BrewingScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject itemBoxPrefab;
    public GameObject scrollViewContent;
    public CraftSkillCtrl supportSkillCtrl;
    public CraftItemInfoBox craftItemInfoBox;
    public CraftResultDialog craftResultDialog;
    int selectedSlotId;
    List<CraftRecipe>brewingRecipeList;
    List<CraftBox> boxList;
    int craftQty;
    // Start is called before the first frame update
    void Start()
    {
        craftQty = 1;
        brewingRecipeList = new List<CraftRecipe>();
        for(int i = 0 ; i < DB.craftRecipeItems.Length; i++){
            brewingRecipeList.Add(DB.craftRecipeItems[i]);
        }
        renderScrollView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderScrollView(){
        header.render();
        supportSkillCtrl.render(Game.craftSkillManager.brewingSkill);
        int noOfBox = brewingRecipeList.Count;
        Transform contentTran = scrollViewContent.transform;
        boxList = new List<CraftBox>();
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;

            box = (GameObject)Instantiate(itemBoxPrefab, contentTran);
            CraftBox boxCtrl = box.GetComponent<CraftBox>();
            //Debug.Log(displayList[i].resultItem);
            boxCtrl.render(brewingRecipeList[i].resultItem);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
            boxList.Add(boxCtrl);
        }
        craftItemInfoBox.gameObject.SetActive(false);
        craftResultDialog.gameObject.SetActive(false);
    }

    public void onClickItem(int slotId)
    {
        selectedSlotId = slotId;
        craftItemInfoBox.setContent(brewingRecipeList[slotId]);
        craftItemInfoBox.show();
    }

    public void setCraftQty(int craftQty){
        this.craftQty = craftQty;
    }

    public void onClickCraft(){
        craftItemInfoBox.hide();
        TaskCompleteMsg taskCompleteMsg = brewingRecipeList[selectedSlotId].craftItem(craftQty);
        craftResultDialog.setTaskCompleteMsg(taskCompleteMsg);
        craftResultDialog.show();
    }
}
