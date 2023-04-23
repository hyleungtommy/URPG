using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public abstract class Skill : Displayable, IFunctionable
    {
        public int id { get; set; }
        public string name { get; set; }
        public float mod { get; set; }
        public int turn { get; set; }
        public int cooldown { get; set; }
        public int currCooldown { get; set; }
        public string animation { get; set; }
        public int reqMp { get; set; }
        public bool aoe { get; set; }
        public bool useOnSelf { get; set; }
        public GeneralSkill.UseOn useOn {get; set;}
        public Skill(Sprite img) : base(img)
        {
            aoe = false;
            useOnSelf = false;
            currCooldown = 0;
        }

        public virtual List<BattleMessage> use(Entity user, Entity[] target)
        {
            //if (user is EntityPlayer && (user as EntityPlayer).havePassiveSkill(SkillPassive.Mana_Reservation))
            //{
            //    user.CurrMP -= (float)mp * (user as EntityPlayer).getPassiveSkill(SkillPassive.Mana_Reservation).Mod;
            //}
            //else

            user.currmp -= reqMp;
            currCooldown = cooldown;
            //Debug.Log("use skill currCooldown=" + currCooldown);
            return null;
        }

        public abstract bool isAttackSkill();

    }
}