using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class QuestScene : BasicScene
{
    public Text textMainQuest;
    public HeaderCtrl header;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        render();
    }

    public void render()
    {
        if (Game.plotPt < DB.mainQuests.Length)
        {
            textMainQuest.text = DB.mainQuests[Game.plotPt].desc + "\n\nNext step: " + DB.mainQuests[Game.plotPt].instruction;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
