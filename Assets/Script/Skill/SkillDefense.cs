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
                user.buffState.addBuff(new Buff(Buff.Type.HP, 10, 1));
            }
            else if (name.Equals("Reflective Defense"))
            {
                user.reflectiveDefense = true;
            }
            bundle.Add(message);
            return bundle;
        }
    }
}