using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace RPG
{
    /// <summary>
    /// Store all public data loaded from json
    /// </summary>
    public static class DB
    {
        public static Map[] maps;
        public static ItemTemplate[] items;
        public static BuffTemplate[] buffs;
        public static Job[] jobs = new Job[]{
            new Job ("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2}, new int[]{1,1,1,1,1}),
            new Job ("Berserker",new int[]{20,45,15,10,20},new int[]{1,5,1,1,2}, new int[]{1,2,0,1,1}),
            new Job("Knight",new int[]{45,20,15,10,20},new int[]{4,2,1,1,2}, new int[]{2,1,0,1,1}),
            new Job("Mage",new int[]{15,10,55,15,25},new int[]{1,1,5,1,2}, new int[]{1,0,2,1,1}),
            new Job("Priest",new int[]{25,10,35,15,15},new int[]{3,1,5,3,3}, new int[]{1,0,2,1,1}),
            new Job("Necromancer",new int[]{10,10,50,15,15},new int[]{1,1,9,2,2}, new int[]{1,0,2,1,1}),
            new Job("Archer",new int[]{15,15,15,25,40},new int[]{2,2,1,5,5}, new int[]{1,1,0,1,2}),
            new Job("Assassin",new int[]{10,20,10,25,45},new int[]{1,2,1,4,7}, new int[]{1,1,0,2,1})
        };

        public static EnemyTemplate[] enemyTemplates;

        public static GeneralEquipment[] equipments;

        public static PlotData[] plots;
        public static MainQuestTemplate[] mainQuests;
        public static ExploreSite[]exploreSites;
        public static CraftRecipe[]craftRecipeItems;
        public static CraftRecipe[]craftRecipeEquipments;
        public static EnchantEffectTemplate[]enchantmentEffects;
        public static EnchantRecipeTemplate[]enchantRecipeTemplates;
        public static TradeListingTemplate[]tradeListingTemplate;
        public static GlobalBuffTemplate[] globalBuffTemplates;
        public static DailyQuestTemplate[] dailyQuestTemplates;
        public static DungeonTemplate[] dunegonTemplates;

        /// <summary>
        /// load data from all jsons from Resources/Data
        /// </summary>
        public static void LoadGameData(){
            for (int i = 0; i < jobs.Length; i++)
            {
                jobs[i].id = i;
            }
            //get enemy data from json
            TextAsset enemyJSON = Resources.Load<TextAsset>("Data/Enemy");
            enemyTemplates = JsonHelper.FromJson<EnemyTemplate>(enemyJSON.text);

            //get map data
            List<Map> mlist = new List<Map>();
            TextAsset mapJSON = Resources.Load<TextAsset>("Data/Map");
            MapTemplate[] templatesMap = JsonHelper.FromJson<MapTemplate>(mapJSON.text);
            for (int i = 0; i < templatesMap.Length; i++)
            {
                templatesMap[i].id = i;
                Map m = templatesMap[i].toMap(enemyTemplates);
                mlist.Add(m);
            }

            maps = mlist.ToArray();

            //get Buff data
            TextAsset buffJSON = Resources.Load<TextAsset>("Data/Buff");
            buffs = JsonHelper.FromJson<BuffTemplate>(buffJSON.text);
            //Debug.Log(Util.printArray<BuffTemplate>(buffs));

            //get Item data
            TextAsset itemJSON = Resources.Load<TextAsset>("Data/Item");
            items = JsonHelper.FromJson<ItemTemplate>(itemJSON.text);

            //get skill data
            TextAsset skillJSON = Resources.Load<TextAsset>("Data/Skill");
            SkillTemplate[] skills = JsonHelper.FromJson<SkillTemplate>(skillJSON.text);
            CreateSkillForJobs(skills);

            //get equipment data
            TextAsset equipmentJSON = Resources.Load<TextAsset>("Data/Equipment");
            EquipmentTemplate[] equipmentTemplates = JsonHelper.FromJson<EquipmentTemplate>(equipmentJSON.text);
            List<GeneralEquipment> elist = new List<GeneralEquipment>();
            for (int i = 0; i < equipmentTemplates.Length; i++)
            {
                elist.Add(equipmentTemplates[i].toGeneralEquipment());
            }
            equipments = elist.ToArray();

            //get craft recipe for item
            List<CraftRecipe> cilist = new List<CraftRecipe>();
            for (int i = 0; i < items.Length; i++)
            {
                //Debug.Log("item " + i);
                if(items[i].craftRecipe.requireItem != null)
                    cilist.Add(items[i].craftRecipe.toCraftRecipe(items[i].toItem()));
            }
            craftRecipeItems = cilist.ToArray();

            //get craft recipe for equipment
            List<CraftRecipe> celist = new List<CraftRecipe>();
            for (int i = 0; i < equipmentTemplates.Length; i++)
            {
                //Debug.Log("Equip " + i);
                if(equipmentTemplates[i].craftRecipe.requireItem != null)
                    celist.Add(equipmentTemplates[i].craftRecipe.toCraftRecipe(equipmentTemplates[i].toGeneralEquipment()));
            }
            craftRecipeEquipments = celist.ToArray();

            //get exploresite
            List<ExploreSite> eslist = new List<ExploreSite>();
            TextAsset exploreSiteJSON = Resources.Load<TextAsset>("Data/ExploreSite");
            ExploreSiteTemplate[]exploreSiteTemplates = JsonHelper.FromJson<ExploreSiteTemplate>(exploreSiteJSON.text);
            for (int i = 0; i < exploreSiteTemplates.Length; i++)
            {
                eslist.Add(exploreSiteTemplates[i].toExploreSite());
            }
            exploreSites = eslist.ToArray();

            //get Plot data
            TextAsset plotJSON = Resources.Load<TextAsset>("Data/Plot");
            PlotTemplate[] plotTemplates = JsonHelper.FromJson<PlotTemplate>(plotJSON.text);
            List<PlotData> pdList = new List<PlotData>();
            for (int i = 0; i < plotTemplates.Length; i++)
            {
                pdList.Add(plotTemplates[i].toPlotData());
            }
            plots = pdList.ToArray();

            //get Main Quest
            TextAsset mainQuestJSON = Resources.Load<TextAsset>("Data/MainQuest");
            mainQuests = JsonHelper.FromJson<MainQuestTemplate>(mainQuestJSON.text);

            //get enchantment effect
            TextAsset enchantmentEffectJSON = Resources.Load<TextAsset>("Data/EnchantEffect");
            enchantmentEffects = JsonHelper.FromJson<EnchantEffectTemplate>(enchantmentEffectJSON.text);

            //get enchantment recipe
            TextAsset enchantmentRecipeJSON = Resources.Load<TextAsset>("Data/EnchantRecipe");
            enchantRecipeTemplates = JsonHelper.FromJson<EnchantRecipeTemplate>(enchantmentRecipeJSON.text);

            //get trade listing data
            TextAsset tradeListingJSON = Resources.Load<TextAsset>("Data/Trade");
            tradeListingTemplate = JsonHelper.FromJson<TradeListingTemplate>(tradeListingJSON.text);

            //get global buff datat
            TextAsset globalBuffJSON = Resources.Load<TextAsset>("Data/GlobalBuff");
            globalBuffTemplates = JsonHelper.FromJson<GlobalBuffTemplate>(globalBuffJSON.text);

            //get daily quests
            TextAsset dailyQuestJSON = Resources.Load<TextAsset>("Data/DailyQuest");
            dailyQuestTemplates = JsonHelper.FromJson<DailyQuestTemplate>(dailyQuestJSON.text);
            Game.questManager.loadQuests();

            //Dunegon
            TextAsset dunegonTemplateJSON = Resources.Load<TextAsset>("Data/Dungeon");
            dunegonTemplates = JsonHelper.FromJson<DungeonTemplate>(dunegonTemplateJSON.text);
            
            DungeonGenerator.GenerateDungeon(0);

        }

        /// <summary>
        /// create Equipment object from Save string
        /// </summary>
        /// <returns>Equipment object loaded from save</returns>
        public static Equipment createEquipmentFormSaveStr(string saveStr)
        {
            string[] data = saveStr.Split('|');
            Equipment e = null;
            if (data.Length == 5)
            {
                try{
                    int id = int.Parse(data[1]);
                    int quality = int.Parse(data[2]);
                    int reinLv = int.Parse(data[3]);
                    string enchatmentText = data[4];
                    e = QueryEquipment(id).toEquipment(quality);
                    if(reinLv > 0 && e.reinforceRecipe != null)
                        e.reinforceRecipe.reinforceLv = reinLv;
                    if(e.enchantment != null)
                        e.enchantment.onLoad(enchatmentText);
                }catch(Exception ex){
                    Debug.Log("error during loading equipment save string :" + saveStr + " exception=" + ex.Message);
                }
            }           
            return e;
        }

        public static void CreateSkillForJobs(SkillTemplate[] skills){
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].id = i;
                for (int j = 0; j < jobs.Length; j++)
                {
                    if (skills[i].jobRestriction.Contains(jobs[j].id))
                    {
                        ///Debug.Log(skills[i].name + "," + jobs[j].name);
                        GeneralSkill s = skills[i].toGeneralSkill();
                        if (Param.learntAllSkill)
                        {
                            s.skillLv = 1;
                        }
                        jobs[j].skills.Add(s);
                    }
                }
            }
        }

        /// <summary>
        /// get a list of available equipment drop for the map
        /// </summary>
        /// <returns>Equipment id list indicate the equipment can be dropped in this map</returns>
        public static List<int>getEquipmentDropList(int mapLv){
            List<int>equipmentIds = DB.equipments.Where(e=>e.reqLv == mapLv).Select(e=>e.id).ToList();
            return equipmentIds;
        }

        public static Item QueryItem(int itemId){
            if(itemId >= items.Length){
                Debug.Log("Failed to query item id=" + itemId);
                return null;
            }
            return items[itemId - 1].toItem();
        }

        public static Item QueryItem(String itemId){
            int parsedId = Int32.Parse(itemId);
            if(parsedId >= items.Length){
                Debug.Log("Failed to query item id=" + parsedId);
                return null;
            }
            return items[parsedId - 1].toItem();
        }

        public static GeneralEquipment QueryEquipment(int id){
            if(id >=  equipments.Length){
                Debug.Log("Failed to query equipment id=" + id);
                return null;
            }
            return equipments[id - 1];
        }

        public static GeneralEquipment QueryEquipment(string id){
            int parsedId = Int32.Parse(id);
            if(parsedId >=  equipments.Length){
                Debug.Log("Failed to query equipment id=" + id);
                return null;
            }
            return equipments[parsedId - 1];
        }

        public static EnemyTemplate QueryEnemy(int id){
            if(id >=  equipments.Length){
                Debug.Log("Failed to query equipment id=" + id);
                return null;
            }
            return enemyTemplates[id - 1];
        }

        public static EnemyTemplate QueryEnemy(string id){
            int parsedId = Int32.Parse(id);
            if(parsedId >=  equipments.Length){
                Debug.Log("Failed to query equipment id=" + id);
                return null;
            }
            return enemyTemplates[parsedId - 1];
        }

    }
}