using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class ResourceBuilding:Building
    {
        private int resourceGenerateRateStart;
        private int resourceGenerateRateInc;
        public int ResourceGenerateRate {get{
            return resourceGenerateRateStart + Lv * resourceGenerateRateInc;
        }}
        public int ResourceGenerateRateNextLv {get{
            return resourceGenerateRateStart + (Lv + 1) * resourceGenerateRateInc;
        }}
        public TownResources.Type ResourceType{get; set;}
        public ResourceBuilding(Sprite img, TownResources.Type type, int resourceGenerateRateStart, int resourceGenerateRateInc):base(img){
            this.ResourceType = type;
            this.resourceGenerateRateStart = resourceGenerateRateStart;
            this.resourceGenerateRateInc = resourceGenerateRateInc;
        }

    }
}