using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG{
    public class CraftSkillManager{
        public SkillCraft miningSkill {get;set;}
        public SkillCraft gatheringSkill {get;set;}
        public SkillCraft huntingSkill {get;set;}
        public SkillCraft smithingSkill {get;set;}
        public SkillCraft arcaneCraftingSkill {get;set;}
        public SkillCraft jewelCraftingSkill {get;set;}
        public SkillCraft reinforcingSkill {get;set;}
        public SkillCraft enchantingSkill {get;set;}
        public SkillCraft brewingSkill {get;set;}
        public int availableExploreTeam {get;set;}
        public CraftSkillManager(){
            availableExploreTeam = 3;
            miningSkill = new SkillCraft(0);
            gatheringSkill = new SkillCraft(1);
            huntingSkill = new SkillCraft(2);
        }
    }
}