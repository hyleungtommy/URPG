using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class StatusScene : BasicScene
{
    BattleMemberListCtrl battleMemberList;
    public Image playerImg;
    public Text textName;
    public Text textStat;
    public Text textUPPT;
    public BarCtrl expbar;
    int selectedId;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void onSelectCharacter(int id, BattleCharacter character)
    {
        this.selectedId = id;
        if (character != null)
        {
            expbar.noAnimationRender(character.expneed, character.currexp);
            playerImg.sprite = character.bodyImg;
            textName.text = character.name + "  Lv." + character.lv + " " + character.job.name;
            BasicStat stat = character.stat.toBasicStat();
            textStat.text = "HP: " + stat.HP + "\nMP: " + stat.MP + "\nATK: " + stat.ATK + "\nDEF: " + stat.DEF + "\nMATK: " + stat.MATK + "\nMDEF: " + stat.MDEF + "\nAGI :" + stat.AGI + "\nDEX: " + stat.DEX +
            "\n\nSkill Pts:" + character.skillPtsSpent + "/" + character.skillPtsEarned;
            textUPPT.text = character.uppt.ToString();
            Game.selectedCharacterInStatusScene = id;
        }

    }

    public void onClickSkill()
    {
        jumpToScene(SceneName.Skill);
    }

    public void onClickEquipment()
    {
        jumpToScene(SceneName.Equipment);
    }

    public void onClickGrowth()
    {

    }

    public void onClickMember()
    {
        jumpToScene(SceneName.PartyManagement);
    }
}
