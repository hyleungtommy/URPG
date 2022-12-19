using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace RPG
{
    public static class DB
    {
        public static Map[] maps;
        public static ItemTemplate[] items;

        public static Job[] jobs = new Job[]{
            new Job ("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2}),
            new Job ("Berserker",new int[]{20,45,15,10,20},new int[]{1,5,1,1,2}),
            new Job("Knight",new int[]{45,20,15,10,20},new int[]{4,2,1,1,2}),
            new Job("Mage",new int[]{15,10,55,15,25},new int[]{1,1,5,1,2}),
            new Job("Priest",new int[]{25,10,35,15,15},new int[]{3,1,5,3,3}),
            new Job("Necromancer",new int[]{10,10,50,15,15},new int[]{1,1,9,2,2}),
            new Job("Archer",new int[]{15,15,15,25,40},new int[]{2,2,1,5,5}),
            new Job("Assassin",new int[]{10,20,10,25,45},new int[]{1,2,1,4,7})
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

        static DB()
        {
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

            //get Item data
            TextAsset itemJSON = Resources.Load<TextAsset>("Data/Item");
            items = JsonHelper.FromJson<ItemTemplate>(itemJSON.text);
            for (int i = 0; i < items.Length; i++)
            {
                items[i].id = i;
            }

            //get skill data
            TextAsset skillJSON = Resources.Load<TextAsset>("Data/Skill");
            SkillTemplate[] skills = JsonHelper.FromJson<SkillTemplate>(skillJSON.text);
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

            //get equipment data
            TextAsset equipmentJSON = Resources.Load<TextAsset>("Data/Equipment");
            EquipmentTemplate[] equipmentTemplates = JsonHelper.FromJson<EquipmentTemplate>(equipmentJSON.text);
            List<GeneralEquipment> elist = new List<GeneralEquipment>();
            for (int i = 0; i < equipmentTemplates.Length; i++)
            {
                equipmentTemplates[i].id = i;
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
            
        }

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
                    e = equipments[id].toEquipment(quality);
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

        public static List<int>getEquipmentDropList(int mapLv){
            List<int>equipmentIds = DB.equipments.Where(e=>e.reqLv == mapLv).Select(e=>e.id).ToList();
            return equipmentIds;
        }

    }
}