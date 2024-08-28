using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class BuildingTemplate
    {
        public int id;
        public string name;
        public string desc;
        public string img;
        public string type;
        public int requireMoneyStart;
        public int requireMoneyInc;
        public int requireWoodStart;
        public int requireStoneStart;
        public int requireWoodInc;
        public int requireStoneInc;
        public int requirePopulation;
        public int requireTime;

        public Building ToBuilding(){
            Building building = new Building(Resources.Load<Sprite>("UI/Icon/" + img));
            building.Id = id;
            building.Name = name;
            building.Desc = desc;
            building.Type = type;
            BuildingRequirement buildingRequirement = new BuildingRequirement(
                building,requireMoneyStart,requireMoneyInc,requireWoodStart,requireWoodInc,requireStoneStart,requireStoneInc,requirePopulation, requireTime
            );
            building.Requirement = buildingRequirement;
            return building;
        }

        public ResourceBuilding ToResourceBuilding(TownResources.Type type){
            ResourceBuilding building = new ResourceBuilding(
                Resources.Load<Sprite>("UI/Icon/" + img),
                type,
                Constant.BasicResourceGenerateRateStart[(int)type],
                Constant.BasicResourceGenerateRateInc[(int)type]
            );
            building.Id = id;
            building.Name = name;
            building.Desc = desc;
            building.Type = this.type;
            BuildingRequirement buildingRequirement = new BuildingRequirement(
                building,requireMoneyStart,requireMoneyInc,requireWoodStart,requireWoodInc,requireStoneStart,requireStoneInc,requirePopulation, requireTime
            );
            building.Requirement = buildingRequirement;
            return building;
        }

        public Warehouse ToWarehouse(){
            Warehouse building = new Warehouse(Resources.Load<Sprite>("UI/Icon/" + img));
            building.Id = id;
            building.Name = name;
            building.Desc = desc;
            building.Type = this.type;
            BuildingRequirement buildingRequirement = new BuildingRequirement(
                building,requireMoneyStart,requireMoneyInc,requireWoodStart,requireWoodInc,requireStoneStart,requireStoneInc,requirePopulation, requireTime
            );
            building.Requirement = buildingRequirement;
            return building;
        }


    }
}