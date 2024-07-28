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

        public static void initialize(){
            party = new Party();
            inventory = new StorageSystem(Param.invSize);
            craftSkillManager = new CraftSkillManager();
            if(DB.maps != null){
                currLoc = DB.maps[0];
            }
        }

        public static void saveGame()
        {
            // //Game Info
            // SaveManager.saveValue(SaveKey.playerName, playerName);
            // SaveManager.saveValue(SaveKey.playerMoney, money);
            // SaveManager.saveValue(SaveKey.currInCity, currInCity);
            // SaveManager.saveValue(SaveKey.currLocMapId, currLoc.id);
            // SaveManager.saveValue(SaveKey.plot_pt, plotPt);
            // SaveManager.saveValue(SaveKey.difficulty,difficulty);
            // //Map Data save format : map1Area|map2Area|...
            // List<int> mapAreas = new List<int>();
            // foreach (Map m in DB.maps)
            // {
            //     mapAreas.Add(m.currZone);
            // }
            // string mapAreaSave = string.Join("|", mapAreas);
            // SaveManager.saveValue(SaveKey.map_areas, mapAreaSave);
            // //Party
            // party.save();
            // //Inventory
            // string invSave = Game.inventory.onSave();
            // SaveManager.saveValue(SaveKey.inventory, invSave);
            // SaveManager.saveValue(SaveKey.platinum_coin, platinumCoin);
            // //Global Buff
            // SaveManager.saveValue(SaveKey.global_buffs, globalBuffManager.onSave());
            // //Crafting
            // SaveManager.saveValue(SaveKey.craft_skill, craftSkillManager.OnSave());
            // //Explore
            // string[] exploreSites = DB.exploreSites.Select(site => site.onSave()).ToArray();
            // SaveManager.saveValue(SaveKey.explore_site, String.Join(";", exploreSites));
            // SaveManager.saveValue(SaveKey.daily_quest,questManager.OnSave());

            // SaveManager.save();
            SaveGameToJSON();
        }

        public static void SaveGameToJSON(){
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
                jobs = partySaveData.jobs
            };

            string json = JsonUtility.ToJson(saveData);
            string path = Application.dataPath + "/Resources/save/save1.json";
            File.WriteAllText(path, json);
        }

        public static void loadGame()
        {
            // initialize();
            // //Game Info
            // playerName = SaveManager.getString(SaveKey.playerName);
            // money = SaveManager.getInt(SaveKey.playerMoney);
            // currInCity = SaveManager.getBool(SaveKey.currInCity);
            // currLoc = DB.maps[SaveManager.getInt(SaveKey.currLocMapId)];
            // plotPt = SaveManager.getInt(SaveKey.plot_pt);
            // difficulty = SaveManager.getInt(SaveKey.difficulty);
            // platinumCoin = SaveManager.getInt(SaveKey.platinum_coin);
            // //Load map
            // string[] mapAreas = SaveManager.getString(SaveKey.map_areas).Split('|');
            // int i = 0;
            // foreach (Map m in DB.maps)
            // {
            //     if (i < mapAreas.Length)
            //     {
            //         m.currZone = int.Parse(mapAreas[i]);
            //     }
            //     i++;
            // }
            // //Load party
            // party.load();
            // //Inventory
            // string invSave = SaveManager.getString(SaveKey.inventory);
            // inventory.onLoad(invSave);
            // //Global Buff
            // globalBuffManager.onLoad(SaveManager.getString(SaveKey.global_buffs));
            // //Cheats
            // Param.skillNoCooldown = SaveManager.getBool(SaveKey.skill_no_cooldown);
            // Param.noCraftRequirement = SaveManager.getBool(SaveKey.no_craft_requirement);
            // Param.unlockAllRecipe = SaveManager.getBool(SaveKey.unlock_all_recipe);
            // //Craft
            // craftSkillManager.OnLoad(SaveManager.getString(SaveKey.craft_skill));
            // //Explore
            // string[] exploreSites = SaveManager.getString(SaveKey.explore_site).Split(';');
            // //Quest
            // questManager.OnLoad(SaveManager.getString(SaveKey.daily_quest));
            // questManager.RenewCompletedDailyQuest();//TODO: make it so that it refresh daily
            
            // if(exploreSites.Length == DB.exploreSites.Length){
            //     for(int j = 0 ; j < DB.exploreSites.Length ;j++){
            //         DB.exploreSites[j].onLoad(exploreSites[j]);
            //     }
            // }
            LoadGameFromJSON();
        }

        public static void LoadGameFromJSON(){
            initialize();

            string path = Application.dataPath + "/Resources/save/save1.json";
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
            string path = Application.dataPath + "/Resources/Save/save1.json";
        
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("File deleted: " + path);
            }
            else
            {
                Debug.LogError("Cannot delete file. File not found at " + path);
            }
        }
    }
}

