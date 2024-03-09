using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillRemoveAllCooldown : SkillSpecial
    {

        public SkillRemoveAllCooldown(Sprite img, string name) : base(img, name)
        {


        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                EntityPlayer targetPlayer = e as EntityPlayer;
                foreach(Skill skill in targetPlayer.skillList){
                    if(!skill.name.Equals(name))
                        skill.currCooldown = 0;
                }
                BattleMessage message = new BattleMessage();
                message.SkillAnimationName = animation;
                message.SkillName = name;
                message.sender = user;
                message.receiver = e;
                message.value = 0;
                message.type = BattleMessage.Type.Heal;
                bundle.Add(message);
                applyBuff(e);
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }

    }
}