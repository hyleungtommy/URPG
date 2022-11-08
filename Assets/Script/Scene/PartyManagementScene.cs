using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class PartyManagementScene : BasicScene
{
    public BasicBox[] boxes;
    BattleCharacter[] list;
    BattleCharacter selectedCharacter;
    // Start is called before the first frame update
    void Start()
    {

        render();
    }

    void render()
    {
        list = Game.party.getAllUnlockedCharacter();
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].render(list[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickMember(int slotId)
    {
        if (selectedCharacter == null)
        {
            if (list[slotId] != null)
            {
                for (int i = 0; i < boxes.Length; i++)
                {
                    if (i == slotId)
                    {
                        boxes[i].setSelected(true);
                    }
                    else
                    {
                        boxes[i].setSelected(false);
                    }
                }
                selectedCharacter = list[slotId];
            }
        }
        else
        {
            BattleCharacter secondSelectCharacter = list[slotId];
            if (secondSelectCharacter != null)
            {
                int swap = selectedCharacter.listPos;
                selectedCharacter.listPos = secondSelectCharacter.listPos;
                secondSelectCharacter.listPos = swap;
            }
            else
            {
                selectedCharacter.listPos = slotId;
            }

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].setSelected(false);
            }
            selectedCharacter = null;
            Game.saveGame();
        }
        render();
    }
}
