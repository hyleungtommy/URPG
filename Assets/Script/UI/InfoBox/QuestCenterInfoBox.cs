using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class QuestCenterInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc;
    public RequirementTextGroupCtrl requirementTextGroupCtrl;
    public Text rewardMoney;
    public Text rewardEXP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void showContent()
    {
        DailyQuest quest = base.obj as DailyQuest;
        textHeader.text = quest.name;
        textBasicInfo.text = "Require Lv." + quest.requireLv;
        textDesc.text = quest.desc;
        rewardMoney.text = quest.rewardMoney.ToString();
        rewardEXP.text = quest.rewardEXP.ToString();
        requirementTextGroupCtrl.render(quest.requirements);
    }
}
