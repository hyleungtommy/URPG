using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class DailyQuestTemplate
    {
        public int id;
        public string name;
        public string desc;
        public int requireLv;
        public int maxLv;
        public string[] requirement;
        public int[] requireQty;
        public int rewardEXP;
        public int rewardMoney;

        public DailyQuest createQuest(){
            DailyQuest quest = new DailyQuest();
            quest.questId = id;
            quest.name = name;
            quest.desc = desc;
            quest.requireLv = requireLv;
            quest.maxLv = maxLv;
            quest.rewardEXP = rewardEXP;
            quest.rewardMoney = rewardMoney;
            quest.accepted = false;
            List<Requirement> requirements = new List<Requirement>();
            for(int i = 0 ; i < requirement.Length ; i++){
                if(requirement[i].StartsWith("E")){
                    EnemyTemplate enemy = DB.QueryEnemy(requirement[i].Substring(1,3));
                    requirements.Add(new Requirement(enemy, requireQty[i]));
                }else{
                    Item item = DB.QueryItem(requirement[i].Substring(1,3));
                    requirements.Add(new Requirement(item,requireQty[i],Requirement.Type.Item));
                }
            }

            return quest;
        }
        
    }

}