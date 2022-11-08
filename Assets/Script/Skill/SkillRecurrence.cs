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
            Debug.Log(target.Length);
            foreach (Entity e in target)
            {
                if (e.currhp <= 0)
                {

                    //float healAmount = e.Stat.HP * ((user as EntityPlayer).havePassiveSkill("Angel Will") ? 0.25f + (user as EntityPlayer).getPassiveSkill("Angel Will").Mod : 0.25f);
                    float healAmount = e.stat.HP * 0.25f;
                    e.currhp = healAmount;
                    /* 
                    if ((user as EntityPlayer).havePassiveSkill("Angel Will"))
                    {
                        e.CurrMP += healAmount;
                        if (e.CurrMP > e.Stat.MP)
                            e.CurrMP = e.Stat.MP;
                    }*/
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