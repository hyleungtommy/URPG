using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class BuildingRequirement
    {
        private int RequireMoneyStart;
        private int RequireMoneyInc;
        private int RequireWoodStart;
        private int RequireStoneStart;
        private int RequireWoodInc;
        private int RequireStoneInc;
        public int RequirePopulation {set; get;}
        public int RequireTime {set; get;}
        private Building Building;
        public int RequireMoney {get{
            return RequireMoneyStart + Building.Lv * RequireMoneyInc;
        }}
        public int RequireWood {get{
            return RequireWoodStart + Building.Lv * RequireWoodInc;
        }}
        public int RequireStone{get{
            return RequireStoneStart + Building.Lv * RequireStoneInc;
        }}
        public BuildingRequirement(Building building,int reqMoneyStart,int reqMoneyInc,int reqWoodStart,int reqWoodInc, int reqStoneStart, int reqStoneInc, int reqPopulation, int reqTime){
            RequireMoneyStart = reqMoneyStart;
            RequireMoneyInc = reqMoneyInc;
            RequireWoodStart = reqWoodStart;
            RequireWoodInc = reqWoodInc;
            RequireStoneStart = reqStoneStart;
            RequireStoneInc = reqStoneInc;
            RequirePopulation = reqPopulation;
            RequireTime = reqTime;
            Building = building;
        }
    }
}