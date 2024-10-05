using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class Town
    {
        public string Name{get; set;}
        public TownResources Resources{get; set;}
        public ResourceBuilding[] ResourceBuildings{get; set;}
        public Warehouse Warehouse{get; set;}
        public Building Townhall{get; set;}
        public Building House {get; set;}
        public int MaxPopulation {get{
            return Constant.PopulationStart + House.Lv * Constant.PopulationPerHouseLv;
        }}
        public int Population {get{
            int pop = 0;
            for(int i = 0 ; i < 4 ; i++){
                pop += ResourceBuildings[i].Requirement.RequirePopulation * ResourceBuildings[i].Lv;
            }
            return pop;
        }}
        public Building[] MainBuildingList {get{
            return new Building[]{Townhall, House, Warehouse};
        }}
        public Building[] ResourceBuildingList {get{
            return ResourceBuildings;
        }}

        public Town(){
            Resources = new TownResources();
            Townhall = DB.buildingTemplates[0].ToBuilding();
            House = DB.buildingTemplates[1].ToBuilding();
            Warehouse = DB.buildingTemplates[2].ToWarehouse();
            ResourceBuildings = new ResourceBuilding[4];
            for(int i = 0 ; i < 4 ; i++){
                ResourceBuildings[i] = DB.buildingTemplates[i + 3].ToResourceBuilding((TownResources.Type)i);
            }
        }

        public void OfflineProgress(){
            string lastTownTimestamp = SaveManager.getString(SaveKey.last_town_timestamp);
            if(lastTownTimestamp.Length > 0){
                DateTime dateTime = Util.GetDateTimeFromLoadSave(lastTownTimestamp);
                TimeSpan elasped = DateTime.Now - dateTime;
                double elaspedSecond = elasped.TotalSeconds;
                for(int i = 0 ; i < 4 ; i++){
                    int resouceGain = (int)Math.Floor((double)ResourceBuildings[i].ResourceGenerateRate / 3600 * elaspedSecond);
                    Resources.AllResources[i] += resouceGain;
                    if(Resources.AllResources[i] > Warehouse.MaxCapacity) 
                        Resources.AllResources[i]= Warehouse.MaxCapacity;
                }
            }
            SaveManager.saveValue(SaveKey.last_town_timestamp, Util.ToDateTimeSaveString(DateTime.Now));
        }

        public string OnSave(){
            List<String>saveStr = new List<string>();
            saveStr.Add(Resources.OnSave());
            for(int i = 0 ; i < ResourceBuildings.Length ; i++){
                saveStr.Add(ResourceBuildings[i].OnSave());
            }
            saveStr.Add(Warehouse.OnSave());
            saveStr.Add(Townhall.OnSave());
            saveStr.Add(House.OnSave());
            return String.Join(";",saveStr.ToArray());
        }

        public void OnLoad(string saveStr){
            int saveStrLengthCheck = 7;// change this when adding new building
            if(saveStr == null){
                return;
            }
            string[] saveStrSplit = saveStr.Split(';');
            if(saveStrSplit.Length == saveStrLengthCheck){
                Resources.OnLoad(saveStrSplit[0]);
                for(int i = 0 ; i < ResourceBuildings.Length ; i++){
                    ResourceBuildings[i].OnLoad(saveStrSplit[i + 1]);
                }
                Warehouse.OnLoad(saveStrSplit[5]);
                Townhall.OnLoad(saveStrSplit[6]);
                House.OnLoad(saveStrSplit[7]);
            }
        }

    }
}