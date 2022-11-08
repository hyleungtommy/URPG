using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SupportStatusScene : BasicScene
{
    public HeaderCtrl header;
    public SupportSkillCtrl[] skillset;
    public SupportCharacterListCtrl characterListCtrl;
    public RankCtrl rank;
    public Image bodyImg;
    public Text textName;
    SupportCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void render()
    {
        textName.text = character.name + " Lv." + character.lv + " " + Constant.supportCharacterJobs[character.jobId];
        rank.render(character.rank);
        int i = 0;
        foreach (SupportSkillCtrl skill in skillset)
        {
            if (character.craftSkillSet.Count > i)
            {
                skill.gameObject.SetActive(true);
                skill.render(character.craftSkillSet[i]);
            }
            else
            {
                skill.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public override void onSelectSupportCharacter(int id, SupportCharacter character)
    {
        if (character != null)
        {
            this.character = character;
            render();
        }

    }
}
