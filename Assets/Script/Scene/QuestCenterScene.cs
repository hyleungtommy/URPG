using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using UnityEngine.UI;
public class QuestCenterScene : ListScene
{
    public HeaderCtrl header;
    void Start()
    {
        header.render();
        UpdateList();
    }
    void UpdateList(){
        List<DailyQuest> dailyQuests = Game.questManager.getAvailableQuests();
        RenderContentView<QuestEntryButtonCtrl>(dailyQuests.ConvertAll<Displayable>(dailQuest => dailQuest));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickAcceptQuest(){
        Game.questManager.AcceptQuest(selectedId);
        infoBox.hide();
        UpdateList();
    }
}
