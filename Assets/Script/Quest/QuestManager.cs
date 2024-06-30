using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG
{
    public class QuestManager
    {
        public List<DailyQuest> dailyQuests;

        public QuestManager(){
            
        }

        public void loadQuests(){
            dailyQuests = new List<DailyQuest>();
            for(int i = 0 ; i < DB.dailyQuestTemplates.Length; i++){
                dailyQuests.Add(DB.dailyQuestTemplates[i].createQuest());
            }
        }

        public List<DailyQuest> getAvailableQuests(){
            return dailyQuests.FindAll(quest => quest.accepted == false).ToList();
        }

    }

}