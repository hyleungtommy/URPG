using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace RPG
{
    public class ExploreSite : Displayable
    {
        public int id { get; set; }
        public enum Type
        {
            Mining, Forging, Hunting
        }
        public string name { get; set; }
        public string desc { get; set; }
        public Type type { get; set; }
        public int requireMoney { get; set; }
        public int requireLevel { get; set; }
        public int requireTime { get; set; }
        public Item[] obtainableItems { get; set; }
        public int[] obtainChance { get; set; }
        public int[] minAmount { get; set; }
        public int[] maxAmount { get; set; }
        public ExploreTask exploreTask { get; set; }

        public ExploreSite(Sprite img, string name) : base(img)
        {
            this.name = name;
        }

        public void startExplore()
        {
            exploreTask = new ExploreTask(this);
        }

        public string onSave(){
            if(exploreTask != null){
                return exploreTask.startTime.ToString("yyyy-MM-dd-HH-mm-ss");
            }else{
                return "";
            }
        }

        public void onLoad(string saveStr){
            try{
                if(saveStr.Length > 0){
                    exploreTask = new ExploreTask(this);
                    exploreTask.startTime = DateTime.ParseExact(saveStr,"yyyy-MM-dd-HH-mm-ss",CultureInfo.InvariantCulture);
                }
            }catch(Exception e){
                Debug.Log("date conversion error:" + saveStr);
            }
            
        }
    }

}
