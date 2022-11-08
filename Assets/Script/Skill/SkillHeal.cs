using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillHeal : Skill
    {

        public SkillHeal(Sprite img) : base(img)
        {


        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                float healAmount = mod * user.stat.MATK;
                Debug.Log("Heal :" + healAmount + "Mod:" + mod + "user MATK:" + user.stat.MATK);
                if (healAmount > (float)(e.stat.HP - e.currhp))
                {
                    healAmount = (float)(e.stat.HP - e.currhp);
                }
                if (e.currhp > 0)
                    e.currhp += healAmount;
                Debug.Log("Heal :" + healAmount + "");
                BattleMessage message = new BattleMessage();
                message.SkillAnimationName = animation;
                message.SkillName = name;
                message.sender = user;
                message.receiver = e;
                message.value = healAmount;
                message.type = BattleMessage.Type.Heal;
                bundle.Add(message);
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }

    }
}