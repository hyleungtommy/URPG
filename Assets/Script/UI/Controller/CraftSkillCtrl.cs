using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class CraftSkillCtrl : MonoBehaviour
{
    public Text textSkillName;
    public BarCtrl expBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render(SkillCraft skill)
    {
        if(skill != null){
            Debug.Log(skill.reqexp + "," + skill.currexp);
            textSkillName.text = skill.typeName + " Lv." + skill.lv;

            expBar.noAnimationRender(skill.reqexp, skill.currexp);
        }
    }
}
