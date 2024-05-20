using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillBuff : Skill
    {

        public SkillBuff(Sprite img) : base(img)
        {


        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();

            foreach (Entity targetEntity in target)
            {
                if (targetEntity.currhp > 0)
                {
                    foreach (Buff b in buffList)
                    {
                        //if ((user as EntityPlayer).havePassiveSkill("Faith of God"))
                        //{
                        //    b.Rounds += (int)(user as EntityPlayer).getPassiveSkill("Faith of God").Mod;
                        //}
                        targetEntity.buffState.addBuff(b);
                        //Debug.Log(targetEntity.name + "buff" + b.type + "round" + b.rounds);
                        BattleMessage message = new BattleMessage();
                        message.SkillAnimationName = animation;
                        message.SkillName = name;
                        message.sender = message.receiver = targetEntity;
                        //message.value = (int)b.type;
                        message.type = BattleMessage.Type.Buff;
                        message.AOE = aoe;
                        bundle.Add(message);
                        //Debug.Log (healAmount);
                    }
                }
            }
            return bundle;
        }

        public override bool isAttackSkill()
        {
            return false;
        }

    }
}