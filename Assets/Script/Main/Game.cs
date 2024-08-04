using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
namespace RPG
{
    public static class Game
    {
        public static string playerName = "Tommy";
        public static int money = 99999;
        public static int platinumCoin = 99999;
        public static Map currLoc = null; // testing only
        public static int currentMapMode = 0;
        public static Party party;
        public static bool currInCity = false;
        public static StorageSystem inventory;
        public static int plotPt = 0;
        public static int selectedCharacterInStatusScene = 0;
        public static CraftSkillManager craftSkillManager;
        public static bool rareEnemyAppeared = false;
        public static int difficulty = 1;
        public static bool shouldRefreshTradeList = true;
        public static List<TradeList> currentTradeList = new List<TradeList>();
        public static GlobalBuffManager globalBuffManager = new GlobalBuffManager();
        public static QuestManager questManager = new QuestManager();
        public static Dungeon currentDungeon;

        public static void initialize(){
            party = new Party();
            inventory = new StorageSystem(Param.invSize);
            craftSkillManager = new CraftSkillManager();
            if(DB.maps != null){
                currLoc = DB.maps[0];
            }
        }

        public static void SaveGame()
        {
            List<int> mapAreas = new List<int>();
            foreach (Map m in DB.maps)
            {
                mapAreas.Add(m.currZone);
            }
            string mapAreaSave = string.Join("|", mapAreas);
            string invSave = inventory.onSave();
            string[] exploreSites = DB.exploreSites.Select(site => site.onSave()).ToArray();
            
            PartySaveData partySaveData = party.OnSave();

            SaveData saveData = new SaveData{
                playerName = playerName,
                playerLv = party.GetMainCharacterLv(),
                previewMapLoc = currLoc.currZone + "/" + currLoc.maxZone,
                previewMapName = currLoc.name,
                playerMoney = money,
                currentlyInCity = currInCity,
                currentMapId = currLoc.id,
                plotPts = plotPt,
                difficulty = difficulty,
                mapAreas = mapAreaSave,
                inventory = invSave,
                platinumCoin = platinumCoin,
                globalBuffs = globalBuffManager.onSave(),
                craftSkills = craftSkillManager.OnSave(),
                exploreSites = String.Join(";", exploreSites),
                dailQuests = questManager.OnSave(),
                battleCharacters = partySaveData.battleCharacters,
                equipmentManagers = partySaveData.equipmentManagers,
                noCraftRequirement = Param.noCraftRequirement,
                unlockAllRecipe = Param.unlockAllRecipe,
                skillNoCooldown = Param.skillNoCooldown,
                jobs = partySaveData.jobs
            };

            string json = JsonUtility.ToJson(saveData);
            string path = Application.dataPath + "/Resources/save/save" + SaveManager.getInt(SaveKey.current_save_slot) +".json";
            File.WriteAllText(path, json);
        }

        public static void LoadGame(int slot){
            initialize();

            string path = Application.dataPath + "/Resources/save/save" + slot +".json";
            if (File.Exists(path)){
                string json = File.ReadAllText(path);
                SaveData save = JsonUtility.FromJson<SaveData>(json);
                //Game Info
                playerName = save.playerName;
                money = save.playerMoney;
                currInCity = save.currentlyInCity;
                currLoc = DB.maps[save.currentMapId];
                plotPt = save.plotPts;
                difficulty = save.difficulty;
                platinumCoin = save.platinumCoin;
                //Load map
                string[] mapAreas = save.mapAreas.Split('|');
                int i = 0;
                foreach (Map m in DB.maps)
                {
                    if (i < mapAreas.Length)
                    {
                        m.currZone = int.Parse(mapAreas[i]);
                    }
                    i++;
                }
                //Load party
                PartySaveData partySaveData = new PartySaveData{
                    battleCharacters = save.battleCharacters,
                    equipmentManagers = save.equipmentManagers,
                    jobs = save.jobs
                };
                party.OnLoad(partySaveData);
                //Inventory
                string invSave = save.inventory;
                inventory.onLoad(invSave);
                //Global Buff
                globalBuffManager.onLoad(save.globalBuffs);
                //Cheats
                Param.skillNoCooldown = save.skillNoCooldown;
                Param.noCraftRequirement = save.noCraftRequirement;
                Param.unlockAllRecipe = save.unlockAllRecipe;
                //Craft
                craftSkillManager.OnLoad(save.craftSkills);
                //Explore
                string[] exploreSites = save.exploreSites.Split(';');
                if(exploreSites.Length == DB.exploreSites.Length){
                    for(int j = 0 ; j < DB.exploreSites.Length ;j++){
                        DB.exploreSites[j].onLoad(exploreSites[j]);
                    }
                }
                //Quest
                questManager.OnLoad(save.dailQuests);
                questManager.RenewCompletedDailyQuest();//TODO: make it so that it refresh daily
            }

        }

        public static void resetSave()
        {
            SaveManager.reset();
            for(int i = 0; i < 3; i++){
                string path = Application.dataPath + "/Resources/Save/save" + (i + 1) +".json";
            
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Debug.Log("File deleted: " + path);
                }
            }

        }
    }
}

