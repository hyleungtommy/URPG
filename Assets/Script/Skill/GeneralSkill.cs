using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class GeneralSkill : Displayable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string skillType { get; set; }
        public bool isAOE {get; set;}
        public UseOn useOn {get;set;}
        public string animation { get; set; }
        public int maxSkillLv { get; set; }
        public float modifierStart { get; set; }
        public float modifierPerLv { get; set; }
        public int turn { get; set; }
        public int cooldown { get; set; }
        public int reqLvStart { get; set; }
        public int reqLvPerLv { get; set; }
        public int reqMpStart { get; set; }
        public int reqMpPerLv { get; set; }
        public int priceStart { get; set; }
        public int pricePerLv { get; set; }
        public int skillPtsStart { get; set; }
        public int skillPtsPerLv { get; set; }
        public int skillLv { get; set; }//need to save
        public List<ApplyBuffTemplate> buffList { get; set; }
        public int reqMp
        {
            get
            {
                return reqMpStart + (reqMpPerLv * (skillLv - 1));
            }
        }
        public float modifier
        {
            get
            {
                return modifierStart + (modifierPerLv * (skillLv));
            }
        }
        public int price
        {
            get
            {
                return priceStart + (pricePerLv * (skillLv));
            }
        }
        public int skillPts
        {
            get
            {
                return skillPtsStart + (skillPtsPerLv * (skillLv));
            }
        }
        public string fullName
        {
            get
            {
                return name + " " + Util.ToRomanNum(skillLv);
            }
        }
        public string fullNameSkillCenter
        {
            get
            {
                return name + " " + Util.ToRomanNum(skillLv + 1);
            }
        }
        public int reqLv
        {
            get
            {
                return reqLvStart + (reqLvPerLv * (skillLv));
            }
        }
        public enum UseOn{
            Opponent,Partner,Self
        }
        public GeneralSkill(Sprite img) : base(img)
        {

        }

        public Skill toSkill()
        {
            Skill s = getSkillType();
            //Debug.Log(skillType);
            if (s != null)
            {
                s.cooldown = cooldown;
                s.id = id;
                s.mod = modifier;
                s.turn = turn;
                s.name = fullName;
                s.animation = animation;
                s.reqMp = reqMp;
                s.aoe = isAOE;
            }
            return s;
        }

        private Skill getSkillType()
        {
            Skill s = null;
            if (skillType == Constant.attackSkill)
            {
                s = new SkillAttack(img);
            }
            else if (skillType == Constant.magicSkill)
            {
                s = new SkillMagic(img);
            }
            else if (skillType == Constant.healSkill)
            {
                s = new SkillHeal(img);
            }
            else if (skillType == Constant.buffSkill)
            {
                s = new SkillBuff(img);
            }
            else if (skillType == Constant.deuffSkill)
            {
                s = new SkillDebuff(img);
            }
            else if (skillType == Constant.defenseSkill)
            {
                s = new SkillDefense(img);
            }
            else if (skillType == Constant.SpecialSkill)
            {
                s = new SkillSpecial(img, name).create();
            }
            if(s != null){
                s.aoe = isAOE;
                s.useOn = useOn;
                s.buffList = getBuffList();
            }
            return s;
        }

        public void learn()
        {
            skillLv++;
        }
        //when it is a buff or debuff skill, 
        private List<Buff> getBuffList()
        {
            List<Buff> newbuffList = new List<Buff>();
            if (buffList != null)
            {
                foreach (ApplyBuffTemplate b in buffList)
                {
                    newbuffList.Add(b.toBuff((int)((modifier - 1) * 100), turn));
                }
            }
            return newbuffList;
        }

        public string onSave()
        {
            return "" + skillLv;
        }

        public void onLoad(string saveStr)
        {
            if (saveStr.Length > 0)
            {
                skillLv = int.Parse(saveStr);
            }
        }

    }
}