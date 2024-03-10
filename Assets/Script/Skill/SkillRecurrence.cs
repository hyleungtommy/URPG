using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillRecurrence : SkillSpecial
    {

        public SkillRecurrence(Sprite img, string name) : base(img, name)
        {


        }

        public override bool isAttackSkill()
        {
            return false;
        }

        public string getTypeName()
        {
            return "Recurrence";
        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                if (e.currhp <= 0)
                {
                    float healAmount = e.stat.HP * 0.25f;
                    
                    if (user is EntityPlayer && (user as EntityPlayer).hasPassiveSkill("Angel Will"))
                    {
                        healAmount = e.stat.HP - e.currhp;
                        e.currmp = e.stat.MP;
                    }
                    
                    e.currhp = healAmount;
                    BattleMessage message = new BattleMessage();
                    message.SkillAnimationName = animation;
                    message.sender = user;
                    message.receiver = e;
                    message.value = healAmount;
                    message.type = BattleMessage.Type.Heal;
                    bundle.Add(message);
                }

            }
            return bundle;
        }
    }
}