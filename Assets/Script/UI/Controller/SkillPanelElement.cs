using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using System;
public class SkillPanelElement : MonoBehaviour
{
    // Start is called before the first frame update
    public BasicBox box;
    public Text textSkillInfo;
    public int skillId { get; set; }
    public Skill skill { get; set; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render(Skill item, EntityPlayer character)
    {
        this.skill = item;
        textSkillInfo.gameObject.SetActive(true);
        box.render(item);
        int requreMP = (int)(item.reqMp * ModifierFromBuffHelper.getMPUseModifierFromBuff(character));
        textSkillInfo.text = item.name + "\n" + (character.currmp < requreMP ? "<color=#ff0000ff>" : "") + "MP:" + requreMP + (character.currmp < requreMP ? "</color>" : "") + (item.currCooldown > 0 ? "\nCooldown:" + item.currCooldown + " round" : "");
        GetComponent<Button>().enabled = (item.currCooldown <= 0 && character.currmp >= requreMP);

        //For Sacrifice Summon skill, only allow when user has summons
        if(item.name.Contains("Sacrifice Summon")){
            if(!(character.buffState.isBuffExists(32) || character.buffState.isBuffExists(33) || character.buffState.isBuffExists(34))){
                GetComponent<Button>().enabled = false;
                textSkillInfo.text = textSkillInfo.text + "\nNo Summon available";
            }
        }
    }


    public void renderEmpty()
    {
        this.skill = null;
        box.render(null);
        textSkillInfo.gameObject.SetActive(false);
    }

}
