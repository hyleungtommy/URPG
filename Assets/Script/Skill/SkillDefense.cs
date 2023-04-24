using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillDefense : Skill
    {

        public SkillDefense(Sprite img) : base(img)
        {


        }

        public override bool isAttackSkill()
        {
            return false;
        }

        public string getTypeName()
        {
            return "Defense";
        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            BattleMessage message = new BattleMessage();
            message.sender = message.receiver = user;
            message.AOE = false;
            message.SkillName = name;
            message.type = BattleMessage.Type.Defense;
            message.value = -1;
            user.isDefensing = true;
            user.defenseModifier = mod;
            
            if (name.Equals("Healing Defense"))
            {
                user.currhp += user.stat.HP *0.1f;
                if(user.currhp > user.stat.HP){
                    user.currhp = user.stat.HP;
                }
                BattleMessage healMessage = new BattleMessage();
                healMessage.SkillAnimationName = animation;
                healMessage.SkillName = name;
                healMessage.sender = user;
                healMessage.receiver = user;
                healMessage.value = user.stat.HP *0.1f;
                healMessage.type = BattleMessage.Type.Heal;
                bundle.Add(message);
            }
            else if (name.Equals("Reflective Defense"))
            {
                user.reflectiveDefense = true;
            }
            applyBuff(user);
            
            bundle.Add(message);
            return bundle;
        }
    }
}