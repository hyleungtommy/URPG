using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace RPG
{
    public class Job
    {
        public string name { get; set; }
        public int id { get; set; }
        public int[] startingStat { get; set; }
        public int[] levelUpGain { get; set; }
        public List<GeneralSkill> skills { get; set; }

        public Job(string name, int[] startingStat, int[] levelUpGain)
        {
            this.name = name;
            this.startingStat = startingStat;
            this.levelUpGain = levelUpGain;
            skills = new List<GeneralSkill>();
        }

        public List<Skill> createSkillList()
        {
            List<Skill> slist = new List<Skill>();
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i] != null && skills[i].skillLv > 0)
                {
                    Skill s = skills[i].toSkill();
                    //Debug.Log(name + "," + s);
                    if (s != null) slist.Add(s);
                }
            }
            return slist;
        }

        public List<GeneralSkill> GetGeneralSkills()
        {
            return skills.Where(a => a != null && a.skillLv > 0).ToList();
        }

        public List<GeneralSkill> GetLearnableSkills(BattleCharacter ch)
        {
            return skills.Where(a => a != null && a.reqLv <= ch.lv).ToList();
        }

        public string onSave()
        {
            List<string> save = new List<string>();
            foreach (GeneralSkill s in skills)
            {
                save.Add(s.onSave());
            }
            return string.Join(";", save);
        }

        public void onLoad(string saveStr)
        {
            string[] data = saveStr.Split(';');
            int i = 0;
            foreach (GeneralSkill s in skills)
            {
                if (i < data.Length)
                {
                    s.onLoad(data[i]);
                }
                i++;
            }
        }
    }
}