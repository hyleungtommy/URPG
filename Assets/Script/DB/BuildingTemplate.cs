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

        public Building toBuilding(){
            Building building = new Building(Resources.Load<Sprite>("UI/Icon/" + img));
            building.Id = id;
            building.Name = name;
            building.Desc = desc;
            building.Type = type;
            building.RequireMoneyStart = requireMoneyStart;
            building.RequireMoneyInc = requireMoneyInc;
            building.RequireStoneStart = requireStoneStart;
            building.RequireStoneInc = requireStoneInc;
            building.RequireWoodStart = requireStoneStart;
            building.RequireWoodInc = requireWoodInc;
            building.RequireTime = requireTime;
            building.RequirePopulation = requirePopulation;
            return building;
        }


    }
}