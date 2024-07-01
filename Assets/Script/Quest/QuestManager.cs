using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG
{
    public class QuestManager
    {
        public List<DailyQuest> dailyQuests { set; get; }

        public QuestManager()
        {

        }

        public void loadQuests()
        {
            dailyQuests = new List<DailyQuest>();
            for (int i = 0; i < DB.dailyQuestTemplates.Length; i++)
            {
                dailyQuests.Add(DB.dailyQuestTemplates[i].createQuest());
            }
        }

        public List<DailyQuest> getAvailableQuests()
        {
            return dailyQuests.FindAll(quest => quest.accepted == false).ToList();
        }

        public List<DailyQuest> GetAcceptedQuests()
        {
            return dailyQuests.FindAll(quest => quest.accepted == true).ToList();
        }

        public void UpdateEnemyCount(EntityEnemy[] entityEnemy)
        {
            //count enemies
            Dictionary<string, int> enemyCount = new Dictionary<string, int>();
            foreach (EntityEnemy enemy in entityEnemy)
            {
                if (enemyCount.ContainsKey(enemy.name))
                {
                    enemyCount[enemy.name] = enemyCount[enemy.name] + 1;
                }
                else
                {
                    enemyCount[enemy.name] = 1;
                }
            }
            foreach (DailyQuest dailyQuest in dailyQuests)
            {
                if (dailyQuest.accepted)
                {
                    foreach(KeyValuePair<string,int> pair in enemyCount){
                        dailyQuest.UpdateEnemyCount(pair.Key, pair.Value);
                    }
                }
            }
        }

        public void AcceptQuest(int id)
        {
            dailyQuests[id - 1].accepted = true;
        }

        public void AbondandQuest(int id)
        {
            dailyQuests[id - 1].accepted = false;
        }

        public void CompleteQuest(int id){
            DailyQuest dailyQuest = dailyQuests[id - 1];
            dailyQuest.accepted = false;
            foreach(Requirement requirement in dailyQuest.requirements){
                if(requirement.type == Requirement.Type.Item){
                    Item item = requirement.requireItem as Item;
                    Game.inventory.smartDelete(item, requirement.requireQty);
                }
            }
            Game.money += dailyQuest.reward.money;
            foreach(BattleCharacter battleCharacter in Game.party.getAllUnlockedCharacter()){
                battleCharacter.assignEXP(dailyQuest.reward.exp);
            }
        }

        public string OnSave(){
            string saveStr = String.Join(";",dailyQuests.Select(quest => quest.OnSave()).ToArray());
            Debug.Log("daily_quest saveStr=" +saveStr);
            return saveStr;
        }

        public void OnLoad(string saveStr){
            string[]saveStrList = saveStr.Split(';');
            int i = 0;
            foreach (DailyQuest dailyQuest in dailyQuests){
                if(i < saveStrList.Length){
                    dailyQuest.OnLoad(saveStrList[i]);
                    i++;
                }else{
                    Debug.Log("daily_quest saveStr OutOfBound, str length=" + saveStrList.Length + " daily quest list=" + dailyQuests.Count + " saveStr=" + saveStr);
                    break;
                }
                
            }
        }

    }

}