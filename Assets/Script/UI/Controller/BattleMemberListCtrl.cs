using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class BattleMemberListCtrl : MonoBehaviour
{
    public BasicBox[] boxes;
    public BasicScene scene;
    BattleCharacter[] list;
    // Start is called before the first frame update
    void Start()
    {
        list = Game.party.getAllUnlockedCharacter();
        //for (int i = 0; i < boxes.Length; i++)
        //{
        //    int j = i;
        //    boxes[i].addClickEvent(() => this.onClickBox(j));
        //}
        if (scene != null && (scene is SkillScene || scene is EquipmentScene))
        {
            onClickBox(Game.selectedCharacterInStatusScene);
        }
        else
        {
            onClickBox(0);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render()
    {

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].render(list[i]);
        }
    }

    public void onClickBox(int id)
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (i == id)
            {
                boxes[i].setSelected(true);
            }
            else
            {
                boxes[i].setSelected(false);
            }
        }
        render();
        scene.onSelectCharacter(id, list[id]);

    }


}
