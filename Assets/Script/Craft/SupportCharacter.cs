using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class SupportCharacter : Displayable
    {
        public string name { get; set; } //Need To Save
        public int lv { get; set; } //Need To Save
        public int faceId { get; set; } //Need To Save
        public int sex { get; set; } //Need To Save
        public int rank { get; set; }
        public int jobId { get; set; }
        public Sprite faceImg { get; set; }
        public Sprite bodyImg { get; set; }
        public List<SkillCraft> craftSkillSet { get; set; }
        public SupportCharacter(string name, Sprite faceImg) : base(faceImg)
        {
            this.name = name;
            lv = 1;
            jobId = 0;
            rank = 1;
            craftSkillSet = new List<SkillCraft>();
            craftSkillSet.Add(new SkillCraft(0));
            craftSkillSet.Add(new SkillCraft(1));
            craftSkillSet.Add(new SkillCraft(2));
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