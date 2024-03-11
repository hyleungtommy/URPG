using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public BattleMemberListCtrl battleMemberList;
    public SkillInfoBox infoBox;
    int selectedMemberId;
    List<GeneralSkill> skillList;
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
        skillList = character.job.GetLearntSkills();
        int noOfBox = skillList.Count;
        Transform contentTran = scrollViewContent.transform;

        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            SkillBox boxCtrl = box.GetComponent<SkillBox>();
            boxCtrl.render(skillList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
    }

    public void onClickItem(int slotId)
    {
        infoBox.setContent(skillList[slotId]);
        infoBox.show();
    }
}
