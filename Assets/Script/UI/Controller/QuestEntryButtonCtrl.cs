using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;

public class QuestEntryButtonCtrl :MonoBehaviour, Renderable
{
    public Text textQuestName;
    public Text textRequireLv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(Displayable item){
        DailyQuest dailyQuest = item as DailyQuest;
        textQuestName.text = dailyQuest.name;
        textRequireLv.text = "Lv." + dailyQuest.requireLv;
    }
    
}
