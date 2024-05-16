using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class Game
    {
        public static string playerName = "Tommy";
        public static int money = 99999;
        public static int platinumCoin = 999;
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
            //Game Info
            SaveManager.saveValue(SaveKey.playerName, playerName);
            SaveManager.saveValue(SaveKey.playerMoney, money);
            SaveManager.saveValue(SaveKey.currInCity, currInCity);
            SaveManager.saveValue(SaveKey.currLocMapId, currLoc.id);
            SaveManager.saveValue(SaveKey.plot_pt, plotPt);
            SaveManager.saveValue(SaveKey.difficulty,difficulty);
            //Map Data save format : map1Area|map2Area|...
            List<int> mapAreas = new List<int>();
            foreach (Map m in DB.maps)
            {
                mapAreas.Add(m.currZone);
            }
            string mapAreaSave = string.Join("|", mapAreas);
            Debug.Log(SaveKey.map_areas + ":" + mapAreaSave);
            SaveManager.saveValue(SaveKey.map_areas, mapAreaSave);
            //Party
            party.save();
            //Inventory
            string invSave = Game.inventory.onSave();
            Debug.Log(SaveKey.inventory + ":" + invSave);
            SaveManager.saveValue(SaveKey.inventory, invSave);
            SaveManager.save();
        }

        public static void loadGame()
        {
            initialize();
            //Game Info
            playerName = SaveManager.getString(SaveKey.playerName);
            money = SaveManager.getInt(SaveKey.playerMoney);
            currInCity = SaveManager.getBool(SaveKey.currInCity);
            currLoc = DB.maps[SaveManager.getInt(SaveKey.currLocMapId)];
            plotPt = SaveManager.getInt(SaveKey.plot_pt);
            difficulty = SaveManager.getInt(SaveKey.difficulty);
            //Load map
            string[] mapAreas = SaveManager.getString(SaveKey.map_areas).Split('|');
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
            party.load();
            //Inventory
            string invSave = SaveManager.getString(SaveKey.inventory);
            inventory.onLoad(invSave);
        }

        public static void resetSave()
        {
            SaveManager.reset();
        }
    }
}

