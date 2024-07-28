using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using UnityEngine.UI;
public class QuestCenterScene : ListScene
{
    public HeaderCtrl header;
    List<DailyQuest> dailyQuests;
    void Start()
    {
        header.render();
        UpdateList();
    }
    void UpdateList(){
        dailyQuests = Game.questManager.getAvailableQuests();
        RenderContentView<QuestEntryButtonCtrl>(dailyQuests.ConvertAll<Displayable>(dailQuest => dailQuest));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickAcceptQuest(){
        int questId = dailyQuests[selectedId].questId;
        Game.questManager.AcceptQuest(questId);
        infoBox.hide();
        UpdateList();
    }
}
