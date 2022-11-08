using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class ItemHPPotion : FunctionalItem
    {
        public float healPercentage { get; set; }
        public int minHealAmount { get; set; }

        public override string ToString()
        {
            return "name=" + name;
        }

        public ItemHPPotion(Sprite img) : base(img)
        {

        }

        public override int getMaxStack()
        {
            return 10;
        }

        public override string getTypeName()
        {
            return "HP Potion";
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
                if (healAmount > (float)(e.stat.HP - e.currhp))
                {
                    healAmount = (float)(e.stat.HP - e.currhp);
                }
                //Debug.Log(healAmount + "," + healPercentage);
                if (e.currhp > 0)
                    e.currhp += healAmount;

                BattleMessage message = new BattleMessage();
                message.sender = user;
                message.receiver = e;
                message.value = healAmount;
                message.type = BattleMessage.Type.Heal;
                bundle.Add(message);
                //Debug.Log (healAmount);
            }
            return bundle;
        }


    }
}