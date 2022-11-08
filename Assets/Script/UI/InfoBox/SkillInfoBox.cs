using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public Text textDesc2;
    public BasicBox box;
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
        GeneralSkill s = obj as GeneralSkill;
        box.render(s);
        textHeader.text = s.fullName;
        textBasicInfo.text = s.skillType + "\n" + "Lv.:" + s.skillLv;
        textDesc1.text = s.desc;
        textDesc2.text = s.modifier + "\n" + s.turn + "\n" + s.reqMp + "\n" + s.cooldown;
    }
}
