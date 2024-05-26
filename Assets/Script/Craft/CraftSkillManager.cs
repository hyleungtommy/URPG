using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            miningSkill = new SkillCraft(SkillCraft.Type.mining);
            gatheringSkill = new SkillCraft(SkillCraft.Type.gathering);
            huntingSkill = new SkillCraft(SkillCraft.Type.hunting);
            smithingSkill = new SkillCraft(SkillCraft.Type.smithing);
            arcaneCraftingSkill = new SkillCraft(SkillCraft.Type.arcaneCrafting);
            jewelCraftingSkill = new SkillCraft(SkillCraft.Type.jewelCrafting);
            reinforcingSkill = new SkillCraft(SkillCraft.Type.reinforcing);
            enchantingSkill = new SkillCraft(SkillCraft.Type.enchanting);
            brewingSkill = new SkillCraft(SkillCraft.Type.brewing);
        }

        public void OnLoad(string saveStr){
            string[] split = saveStr.Split(';');
            if(split.Length == 10){
                availableExploreTeam = Int32.Parse(split[0]);
                miningSkill.OnLoad(split[1]);
                gatheringSkill.OnLoad(split[2]);
                huntingSkill.OnLoad(split[3]);
                smithingSkill.OnLoad(split[4]);
                arcaneCraftingSkill.OnLoad(split[5]);
                jewelCraftingSkill.OnLoad(split[6]);
                reinforcingSkill.OnLoad(split[7]);
                enchantingSkill.OnLoad(split[8]);
                brewingSkill.OnLoad(split[9]);
            }
        }

        public string OnSave(){
            string[] saveStrs = new string[10];
            saveStrs[0] = availableExploreTeam.ToString();
            saveStrs[1] = miningSkill.OnSave();
            saveStrs[2] = gatheringSkill.OnSave();
            saveStrs[3] = huntingSkill.OnSave();
            saveStrs[4] = smithingSkill.OnSave();
            saveStrs[5] = arcaneCraftingSkill.OnSave();
            saveStrs[6] = jewelCraftingSkill.OnSave();
            saveStrs[7] = reinforcingSkill.OnSave();
            saveStrs[8] = enchantingSkill.OnSave();
            saveStrs[9] = brewingSkill.OnSave();
            Debug.Log(String.Join(";",saveStrs));
            return String.Join(";",saveStrs);
        }

        public void addExperience(SkillCraft.Type type, int taskLv){
            switch (type){
                case SkillCraft.Type.mining:
                    miningSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.gathering:
                    gatheringSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.hunting:
                    huntingSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.smithing:
                    smithingSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.arcaneCrafting:
                    arcaneCraftingSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.jewelCrafting:
                    jewelCraftingSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.reinforcing:
                    reinforcingSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.enchanting:
                    enchantingSkill.addExperience(taskLv);
                    break;
                case SkillCraft.Type.brewing:
                    brewingSkill.addExperience(taskLv);
                    break;
            }
        }
    }
}