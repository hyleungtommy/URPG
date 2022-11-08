using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillDecoy : SkillSpecial
    {

        public SkillDecoy(Sprite img, string name) : base(img, name)
        {


        }

        public override bool isAttackSkill()
        {
            return true;
        }

        public string getTypeName()
        {
            return "Decoy";
        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);
            List<BattleMessage> bundle = new List<BattleMessage>();
            BattleMessage b = new BattleMessage();
            b.sender = b.receiver = user;
            b.type = BattleMessage.Type.Special;
            b.SkillAnimationName = animation;
            bundle.Add(b);
            //Debug.Log (target.Length);
            foreach (Entity e in target)
                (e as EntityEnemy).decoy(user as EntityPlayer, (int)mod);// add 10000 to hate meter
            return bundle;
        }
    }
}