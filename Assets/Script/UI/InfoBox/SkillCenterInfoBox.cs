using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SkillCenterInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public Text textDesc2;
    public Text textDesc3;
    public BasicBox box;
    public Button btnLearn;
    public SkillCenterScene scene;
    public BattleCharacter character { get; set; }
    public ElementalDisplayPanelCtrl elementalDisplayPanel;
    private GeneralSkill skill;
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
        base.showContent();
        GeneralSkill s = base.obj as GeneralSkill;
        this.skill = s;
        box.render(s);
        textHeader.text = s.name;
        textBasicInfo.text = s.skillType + "\nLv." + (s.skillLv + 1);
        textDesc1.text = s.desc;
        textDesc2.text = s.modifier + "\n" + s.turn + "\n" + s.reqMp + "\n" + s.cooldown;
        textDesc3.text = (s.reqLv > character.lv ? getRedString(s.reqLv.ToString()) : s.reqLv.ToString()) + "\n" +
        (s.skillPts > character.skillPtsAvailable ? getRedString(s.skillPts.ToString()) : s.skillPts.ToString()) + "\n" +
        (s.price > Game.money ? getRedString(s.price.ToString()) : s.price.ToString());
        btnLearn.gameObject.SetActive(canLearn());
        if(s.elementDamage != null){
            elementalDisplayPanel.Render(s.elementDamage);
        }
    }

    public bool canLearn()
    {
        bool canLearn = true;
        if (skill.reqLv > character.lv) canLearn = false;
        if (skill.skillPts > character.skillPtsAvailable) canLearn = false;
        if (skill.price > Game.money) canLearn = false;
        return canLearn;
    }

    public void onClickLearn()
    {
        Game.money -= skill.price;
        character.skillPtsSpent += skill.skillPts;
        scene.onLearn();
        Game.SaveGame();
        hide();
    }


}
