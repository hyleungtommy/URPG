using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class SkillBox : BasicLargeBox
{
    // Start is called before the first frame update
    public Text textSkillName;
    public Text textSkillReqMP;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    protected override void boxHaveItem(Displayable obj)
    {
        base.boxHaveItem(obj);
        GeneralSkill item = obj as GeneralSkill;
        textSkillName.text = item.fullName;
        textSkillReqMP.text = "MP: " + item.reqMp.ToString();
    }

    protected override void boxIsEmpty()
    {
        base.boxIsEmpty();
    }

}