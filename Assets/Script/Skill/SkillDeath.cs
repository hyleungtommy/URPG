using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillDeath : SkillSpecial
    {

        public SkillDeath(Sprite img, string name) : base(img, name)
        {


        }

        public override bool isAttackSkill()
        {
            return true;
        }

        public string getTypeName()
        {
            return "Death";
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
            float deathChance = (user.stat.MATK / target[0].stat.MDEF) * 0.01f;
            //Debug.Log ("deathChance" + deathChance);
            if (deathChance > 0.1f)
                deathChance = 0.1f;
            //deathChance = 1f;
            float rnd = UnityEngine.Random.Range(0f, 1f);
            if (rnd < deathChance)
            {
                //Debug.Log (target [0].Name + " dead");
                target[0].currhp = -1;
            }
            return bundle;
        }
    }
}