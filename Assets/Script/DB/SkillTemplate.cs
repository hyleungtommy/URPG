using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class SkillTemplate
    {
        public int id;
        public string name;
        public string desc;
        public string skillType;
        public bool isAOE;
        public string useOn;
        public string img;
        public string animation;
        public int maxSkillLv;
        public float modifierStart;
        public float modifierPerLv;
        public int turn;
        public int cooldown;
        public int reqLvStart;
        public int reqLvPerLv;
        public int reqMpStart;
        public int reqMpPerLv;
        public int priceStart;
        public int pricePerLv;
        public int skillPtsStart;
        public int skillPtsPerLv;
        public int[] jobRestriction;
        public ApplyBuffTemplate[] Buffs;
        public ElementalTemplate elementDamage;

        public override string ToString()
        {
            return "name=" + name;
        }

        public GeneralSkill toGeneralSkill()
        {
            GeneralSkill s = new GeneralSkill(Resources.Load<Sprite>("Skill/" + img));
            s.id = id;
            s.name = name;
            s.desc = desc;
            s.skillType = skillType;
            s.animation = animation;
            s.maxSkillLv = maxSkillLv;
            s.modifierStart = modifierStart;
            s.modifierPerLv = modifierPerLv;
            s.priceStart = priceStart;
            s.pricePerLv = pricePerLv;
            s.reqLvStart = reqLvStart;
            s.reqLvPerLv = reqLvPerLv;
            s.reqMpPerLv = reqMpPerLv;
            s.reqMpStart = reqMpStart;
            s.skillPtsPerLv = skillPtsPerLv;
            s.skillPtsStart = skillPtsStart;
            s.turn = turn;
            s.isAOE = isAOE;
            s.cooldown = cooldown;
            s.elementDamage = elementDamage == null ? new ElementalTemplate() : elementDamage;
            

            if(useOn.Equals("partner")){
                s.useOn = GeneralSkill.UseOn.Partner;
            }else if(useOn.Equals("self")){
                s.useOn = GeneralSkill.UseOn.Self;
            }else{
                s.useOn = GeneralSkill.UseOn.Opponent;
            }

            if (Buffs != null)
            {
                s.buffList = new List<ApplyBuffTemplate>(Buffs);
            }
            else
            {
                s.buffList = new List<ApplyBuffTemplate>();
            }

            return s;
        }




    }
}