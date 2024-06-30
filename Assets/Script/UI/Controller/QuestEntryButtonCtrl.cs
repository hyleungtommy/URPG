using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;

public class QuestEntryButtonCtrl : MonoBehaviour
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

    public void render(DailyQuest dailyQuest){
        textQuestName.text = dailyQuest.name;
        textRequireLv.text = "Lv." + dailyQuest.requireLv;
    }
    
}
