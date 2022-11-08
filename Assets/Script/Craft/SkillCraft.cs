using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class SkillCraft
    {
        public int type { get; set; } //Need To Save
        public int currexp { get; set; } //Need To Save
        public int lv { get; set; } //Need To Save
        public int reqexp { get; set; }
        public string typeName
        {
            get
            {
                return Constant.craftSkillTypes[type];
            }
        }

        public SkillCraft(int type)
        {
            this.type = type;
            this.currexp = 0;
            this.lv = 1;
            this.reqexp = Util.calculateProductionSkillEXPNeed(lv);
        }

        public void onload(string save)
        {


        }

        public string onsave()
        {
            return "";
        }


    }
}