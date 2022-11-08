using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SkillCenterScene : BasicScene
{

    // Start is called before the first frame update
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public BattleMemberListCtrl battleMemberList;
    public SkillCenterInfoBox infoBox;
    public Text textCharacterName;
    public Text textCharacterSkillPts;
    int selectedMemberId;
    List<GeneralSkill> skillList;
    int selectSkillSlotId;
    BattleCharacter selectCharacter;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        infoBox.hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void onSelectCharacter(int id, BattleCharacter character)
    {
        this.selectedMemberId = id;
        this.selectCharacter = character;
        render();
    }

    public void render()
    {
        skillList = selectCharacter.job.GetLearnableSkills(selectCharacter);
        int noOfBox = skillList.Count;
        textCharacterName.text = selectCharacter.name;
        textCharacterSkillPts.text = "SkillPts:" + selectCharacter.skillPtsSpent + "/" + selectCharacter.skillPtsEarned;
        Transform contentTran = scrollViewContent.transform;
        //Debug.Log("len:" + skillList.Count);
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            SkillCenterBox boxCtrl = box.GetComponent<SkillCenterBox>();
            boxCtrl.render(skillList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
    }

    public void onClickItem(int slotId)
    {
        selectSkillSlotId = slotId;
        infoBox.character = Game.party.getAllUnlockedCharacter()[selectedMemberId];
        infoBox.setContent(skillList[slotId]);
        infoBox.show();
    }

    public void onLearn()
    {
        skillList[selectSkillSlotId].learn();

        //TODO: update scene after learn
        header.render();
        render();
    }
}
