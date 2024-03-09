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
            else if (name.Equals("Life Absorpation"))
            {
                s = new SkillLifeAbsorpation(img, name);
            }
            else if (name.Equals("ExpellBuff"))
            {
                s = new SkillExpellBuff(img, name);
            }
            else if (name.Equals("Wind Song") || name.Equals("Shadow Form"))
            {
                s = new SkillRemoveAllCooldown(img, name);
            }
            return s;
        }

    }
}