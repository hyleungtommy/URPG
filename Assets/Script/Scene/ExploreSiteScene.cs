using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ExploreSiteScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject itemBoxPrefab;
    //public SupportCharacterListCtrl supportMemberList;
    public GameObject scrollViewContent;
    public CraftSkillCtrl supportSkillCtrl;
    //public TimedTaskCtrl currentTaskCtrl;
    public Button tabMining;
    public Button tabForging;
    public Button tabHunting;
    public ExploreSiteInfoBox exploreSiteInfoBox;
    public ExploreResultDialog exploreResultDialog;
    int selectedSlotId;
    int selectedTabId;
    //SupportCharacter selectedCharacter;
    List<ExploreSite>miningSites;
    List<ExploreSite>forgingSites;
    List<ExploreSite>huntingSites;
    List<ExploreSite>displayList;
    List<ExploreSiteBox> boxList;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        miningSites = new List<ExploreSite>();
        forgingSites = new List<ExploreSite>();
        huntingSites = new List<ExploreSite>();
        for(int i = 0 ; i < DB.exploreSites.Length;i++){
            if(DB.exploreSites[i].type == ExploreSite.Type.Mining) 
                miningSites.Add(DB.exploreSites[i]);
            else if (DB.exploreSites[i].type == ExploreSite.Type.Forging)
                forgingSites.Add(DB.exploreSites[i]);
            else
                huntingSites.Add(DB.exploreSites[i]);
        }
        renderScrollView();
        //StartCoroutine("updateCurrentTaskCtrl");
    }
    /*
    public override void onSelectSupportCharacter(int id, SupportCharacter character)
    {
        base.onSelectSupportCharacter(id, character);
        selectedCharacter = character;
        btnMining.gameObject.SetActive(false);
        btnForging.gameObject.SetActive(false);
        btnHunting.gameObject.SetActive(false);
        foreach(SkillCraft skill in character.craftSkillSet){
            if(skill.type ==Constant.craftSkillMiningId){
                btnMining.gameObject.SetActive(true);
            }else if(skill.type == Constant.craftSkillForgingId){
                btnForging.gameObject.SetActive(true);
            }else if(skill.type == Constant.craftSkillHuntingId){
                btnHunting.gameObject.SetActive(true);
            }
        }
        renderSupportCharacterStatus(character);

    }
   
    void renderSupportCharacterStatus(SupportCharacter character){
        SkillCraft skillCraft = null;
        foreach(SkillCraft skill in character.craftSkillSet){
            if(selectedTabId == 0 && skill.type == Constant.craftSkillMiningId){
                skillCraft = skill;
            }else if(selectedTabId == 1 && skill.type == Constant.craftSkillForgingId){
                skillCraft = skill;
            }else if(selectedTabId == 2 && skill.type == Constant.craftSkillHuntingId){
                skillCraft = skill;
            }
        }
        supportSkillCtrl.render(skillCraft);
        currentTaskCtrl.render(character.currentTask);
    }
     */

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderScrollView(){
        StopAllCoroutines();
        if(selectedTabId == 0){
            supportSkillCtrl.render(Game.craftSkillManager.miningSkill);
            displayList = miningSites;
        }

        else if (selectedTabId == 1){
            supportSkillCtrl.render(Game.craftSkillManager.gatheringSkill);
            displayList = forgingSites;
        }
        else{
            supportSkillCtrl.render(Game.craftSkillManager.huntingSkill);
            displayList = huntingSites;
        }
            
        int noOfBox = displayList.Count;
        Transform contentTran = scrollViewContent.transform;
        boxList = new List<ExploreSiteBox>();
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;

            box = (GameObject)Instantiate(itemBoxPrefab, contentTran);
            ExploreSiteBox boxCtrl = box.GetComponent<ExploreSiteBox>();
            boxCtrl.setExploreSite(displayList[i]);
            boxCtrl.render();
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
            boxList.Add(boxCtrl);
            
            
        }
        exploreSiteInfoBox.gameObject.SetActive(false);
        exploreResultDialog.gameObject.SetActive(false);
        StartCoroutine("updateCurrentTaskCtrl");
        //craftEquipmentInfoBox.gameObject.SetActive(false);
        //craftItemInfoBox.gameObject.SetActive(false);
    }

    public void onClickTab(int tabId){
        selectedTabId = tabId;
        renderScrollView();
    }

    public void onClickItem(int slotId)
    {
        selectedSlotId = slotId;
        if(displayList[slotId].exploreTask != null){
            if(displayList[slotId].exploreTask.isTaskCompleted()){
                TaskCompleteMsg taskCompleteMsg = displayList[slotId].exploreTask.completeTask();
                exploreResultDialog.setTaskCompleteMsg(taskCompleteMsg);
                exploreResultDialog.show();
                displayList[slotId].exploreTask = null;
            }
        }else{
            exploreSiteInfoBox.setContent(displayList[slotId]);
            exploreSiteInfoBox.show();
        }
        
    }

    public void onClickExplore(){
        //selectedCharacter.assignExploreTask(displayList[selectedSlotId]);
        //renderSupportCharacterStatus(selectedCharacter);
        displayList[selectedSlotId].startExplore();
        Game.craftSkillManager.availableExploreTeam --;
        exploreSiteInfoBox.hide();
    }


    IEnumerator updateCurrentTaskCtrl(){
        
        while(true){
            foreach(ExploreSiteBox ctrl in boxList){
                ctrl.render();
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
}
