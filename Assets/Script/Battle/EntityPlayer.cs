using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class EntityPlayer : Entity
    {
        public BattleScene scene { set; get; }
        public List<Skill> skillList { set; get; }
        public EntityPlayer(string name, BasicStat stat, Sprite img, List<Skill> skillList) : base(name, stat, img)
        {
            //Debug.Log(name + "," + skillList.Count);
            this.skillList = skillList;
        }

        public override void takeAction(IFunctionable functionable)
        {
            List<BattleMessage> bundle = functionable.use(this, opponent);
            curratb = 0;
            scene.createFloatingText(bundle);
        }

        public override void passRound()
        {
            base.passRound();
            foreach (Skill s in skillList)
            {
                if (s != null && s.currCooldown > 0) s.currCooldown -= 1;
            }
        }
    }
}