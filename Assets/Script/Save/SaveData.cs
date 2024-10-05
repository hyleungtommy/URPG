using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
namespace RPG
{
    [Serializable]
    public class SaveData{
        public string playerName;
        public int playerLv;
        public string previewMapName;
        public string previewMapLoc;
        public int playerMoney;
        public string[] battleCharacters;
        public string[] equipmentManagers;
        public string[] jobs;
        public int currentMapId;
        public bool currentlyInCity;
        public string mapAreas;
        public string inventory;
        public int platinumCoin;
        public string globalBuffs;
        public string craftSkills;
        public string exploreSites;
        public string dailQuests;
        public int plotPts;
        public int difficulty;
        public bool playedBefore;
        public bool unlockAllRecipe;
        public bool noCraftRequirement;
        public bool skillNoCooldown;
        public string township;
        public string warehouseStorage;
        
    }
}