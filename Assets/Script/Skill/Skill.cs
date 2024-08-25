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
        public GeneralSkill.UseOn useOn { get; set; }
        public List<Buff> buffList { get; set; }
        public ElementalTemplate elementDamage {get; set;}
        public Skill(Sprite img) : base(img)
        {
            aoe = false;
            useOnSelf = false;
            currCooldown = 0;
        }

        public virtual List<BattleMessage> use(Entity user, Entity[] target)
        {
            user.currmp -= reqMp * ModifierFromBuffHelper.getMPUseModifierFromBuff(user) * (user is EntityPlayer ? ModifierFromBuffHelper.getMPModifierFromPassiveSkill(user as EntityPlayer) : 1f);
            currCooldown = cooldown - ModifierFromBuffHelper.getCooldownModifier(user);
            if(currCooldown < 0) currCooldown = 0;
            //Debug.Log("use skill currCooldown=" + currCooldown);
            return null;
        }

        protected void applyDebuff(Entity user, Entity target)
        {
            if (buffList != null)
            {
                foreach (Buff buff in buffList)
                {
                    float applyChance = ((float)user.stat.MATK / (float)target.stat.MDEF * 2f);
                    int rnd = UnityEngine.Random.Range(0, (int)applyChance);
                    if (rnd < applyChance)
                        target.buffState.addBuff(buff);
                }
            }

        }

        protected void applyBuff(Entity target)
        {
            if (buffList != null)
            {
                foreach (Buff buff in buffList)
                {
                    target.buffState.addBuff(buff);
                }
            }

        }

        public abstract bool isAttackSkill();

    }
}