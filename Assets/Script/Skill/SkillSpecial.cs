using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillSpecial : Skill
    {

        public SkillSpecial(Sprite img, string name) : base(img)
        {
            this.name = name;

        }

        public override bool isAttackSkill()
        {
            return false;
        }

        public SkillSpecial create()
        {
            SkillSpecial s = null;
            if (name.Equals("Decoy"))
            {
                s = new SkillDecoy(img, name);
                s.aoe = true;
            }
            else if (name.Equals("Recurrence"))
            {
                s = new SkillRecurrence(img, name);
            }
            else if (name.Equals("Death"))
            {
                s = new SkillDeath(img, name);
            }
            return s;
        }

    }
}