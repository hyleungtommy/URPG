using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ExploreSiteScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject itemBoxPrefab;
    public GameObject scrollViewContent;
    public CraftSkillCtrl supportSkillCtrl;
    public Button tabMining;
    public Button tabForging;
    public Button tabHunting;
    public ExploreSiteInfoBox exploreSiteInfoBox;
    public ExploreResultDialog exploreResultDialog;
    public Text availableExploreTeam;
    int selectedSlotId;
    int selectedTabId;
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
            if(DB.exploreSites[i].type == ExploreSite.Type.Mining && (Param.unlockAllRecipe || DB.exploreSites[i].requireLevel <= Game.craftSkillManager.miningSkill.lv)) 
                miningSites.Add(DB.exploreSites[i]);
            else if (DB.exploreSites[i].type == ExploreSite.Type.Forging &&  (Param.unlockAllRecipe || DB.exploreSites[i].requireLevel <= Game.craftSkillManager.gatheringSkill.lv))
                forgingSites.Add(DB.exploreSites[i]);
            else if(DB.exploreSites[i].type == ExploreSite.Type.Hunting &&  (Param.unlockAllRecipe || DB.exploreSites[i].requireLevel <= Game.craftSkillManager.huntingSkill.lv))
                huntingSites.Add(DB.exploreSites[i]);
        }
        renderScrollView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderSupportSkill(){
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
    }

    void renderScrollView(){
        StopAllCoroutines();
        RenderSupportSkill();
            
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
                RenderSupportSkill();
            }
        }else{
            exploreSiteInfoBox.setContent(displayList[slotId]);
            exploreSiteInfoBox.show();
        }
        
    }

    public void onClickExplore(){
        displayList[selectedSlotId].startExplore();
        Game.craftSkillManager.availableExploreTeam --;
        Game.money -= displayList[selectedSlotId].requireMoney;
        exploreSiteInfoBox.hide();
        header.render();
    }


    IEnumerator updateCurrentTaskCtrl(){
        
        while(true){
            foreach(ExploreSiteBox ctrl in boxList){
                ctrl.render();
            }
            availableExploreTeam.text = Game.craftSkillManager.availableExploreTeam + "/" + Param.maximumExploreTeam;
            yield return new WaitForSeconds(1f);
        }
        
    }
}
