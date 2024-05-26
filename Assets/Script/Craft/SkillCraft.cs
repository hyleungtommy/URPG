using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class SkillCraft
    {
        public enum Type{
            mining=0, gathering=1, hunting=2, smithing=3, arcaneCrafting=4, jewelCrafting=5, reinforcing=6, enchanting=7, brewing=8
        }
        public Type type {get; set;}
        public int currexp { get; set; } //Need To Save
        public int lv { get; set; } //Need To Save
        public int reqexp { get; set; }
        public string typeName
        {
            get
            {
                return type.ToString();
            }
        }

        public SkillCraft(Type type)
        {
            this.type = type;
            this.currexp = 0;
            this.lv = 1;
            this.reqexp = Util.calculateCraftSkillEXPNeed(this.type, lv);
        }

        public void addExperience(int taskLv){
            if(lv < 10 && taskLv >= lv){
                currexp += 1;
                if(currexp >= reqexp){
                    lv ++;
                    currexp = 0;
                    reqexp = Util.calculateCraftSkillEXPNeed(type, lv);
                }
            }
        }

        public void OnLoad(string save)
        {
            string[]split = save.Split(',');
            if(split.Length == 2){
                this.lv = Int32.Parse(split[0]);
                this.currexp = Int32.Parse(split[1]);
                this.reqexp = Util.calculateCraftSkillEXPNeed(this.type, lv);
            }
        }

        public string OnSave()
        {
            return lv + "," + currexp;
        }


    }
}