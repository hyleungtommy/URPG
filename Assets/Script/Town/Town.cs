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

    }
}