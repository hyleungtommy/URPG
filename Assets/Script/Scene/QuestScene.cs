using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class QuestScene : ListScene
{
    public Text textMainQuest;
    public HeaderCtrl header;
    public Button btnCompleteQuest;
    public Button btnAbandonQuest;
    public QuestRewardDialog questRewardDialog;
    List<DailyQuest>dailyQuests;
    // Start is called before the first frame update
    void Start()
    {
        render();
        questRewardDialog.close();
    }

    public void render()
    {
        header.render();
        if (Game.plotPt < DB.mainQuests.Length)
        {
            textMainQuest.text = DB.mainQuests[Game.plotPt].desc + "\n\nNext step: " + DB.mainQuests[Game.plotPt].instruction;
        }
        dailyQuests = Game.questManager.GetAcceptedQuests();
        RenderContentView<QuestEntryButtonCtrl>(dailyQuests.ConvertAll<Displayable>(dailQuest => dailQuest));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickAbondandQuest(){
        int questId = dailyQuests[selectedId].questId;
        Game.questManager.AbondandQuest(questId);
        infoBox.hide();
        render();
    }

    public void onClickCompleteQuest(){
        int questId = dailyQuests[selectedId].questId;
        infoBox.hide();
        Game.questManager.CompleteQuest(questId);
        questRewardDialog.setQuest(Game.questManager.dailyQuests[questId - 1]);
        questRewardDialog.show();
        render();
    }

    public override void OnClickInfoBox(Displayable item)
    {
        base.OnClickInfoBox(item);
        bool questCompleted = (item as DailyQuest).IsCompleted();
        btnCompleteQuest.gameObject.SetActive(questCompleted);
        btnAbandonQuest.gameObject.SetActive(!questCompleted);
    }
}
