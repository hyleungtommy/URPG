using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SupportCharacterListCtrl : MonoBehaviour
{
    public SupportCharacterBox[] boxes;
    public BasicScene scene;
    public int skillTypeFilter = -1;
    SupportCharacter[] characters;

    // Start is called before the first frame update
    void Start()
    {
        if (skillTypeFilter >= 0)
        {
            characters = Game.party.getSupportCharacterList(skillTypeFilter);
        }
        else
        {
            characters = Game.party.getSupportCharacterList();
        }
        onClickBox(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].render(characters[i]);
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
        scene.onSelectSupportCharacter(id, characters[id]);
    }
}
