using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SkillCenterBox : BasicLargeBox
{
    public Text textSkillName;
    public Text textSkillPts;
    public Text textPrice;
    // Start is called before the first frame update
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
        textSkillName.text = item.fullNameSkillCenter;
        textSkillPts.text = "SkillPts: " + item.skillPts;
        textPrice.text = item.price.ToString();
    }

    protected override void boxIsEmpty()
    {
        base.boxIsEmpty();
    }


}
