using System;
using UnityEngine;
using System.Collections.Generic;


namespace RPG
{
    public class ItemMPPotion : FunctionalItem
    {
        public float healPercentage { get; set; }
        public float minHealAmount { get; set; }
        public ItemMPPotion(Sprite img)
            : base(img)
        {
        }

        public override int getMaxStack()
        {
            return 10;
        }

        public override string getTypeName()
        {
            return "MP Potion";
        }

        public override bool isUseOnPlayerParty()
        {
            return true;
        }

        public override System.Collections.Generic.List<BattleMessage> use(Entity user, Entity[] target)
        {
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                float healAmount = healPercentage * e.stat.HP;

                if (healAmount < minHealAmount)
                    healAmount = minHealAmount;
                if (healAmount > (float)(e.stat.MP - e.currmp))
                {
                    healAmount = (float)(e.stat.MP - e.currmp);
                }
                if (e.currmp > 0)
                    e.currmp += healAmount;

                BattleMessage message = new BattleMessage();
                message.sender = user;
                message.receiver = e;
                message.value = healAmount;
                message.type = BattleMessage.Type.MPHeal;
                bundle.Add(message);
                //Debug.Log (healAmount);
            }
            return bundle;
        }
    }
}

