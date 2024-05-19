using System;
using UnityEngine;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
namespace RPG
{
    public class ItemRevival : FunctionalItem
    {
        public ItemRevival(Sprite img)
            : base(img)
        {
        }

        public override int getMaxStack()
        {
            return 1;
        }

        public override string getTypeName()
        {
            return "Revival Item";
        }

        public override System.Collections.Generic.List<BattleMessage> use(Entity user, Entity[] target)
        {
            float healPercentage = 0.2f;

            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Entity e in target)
            {
                float healAmount = healPercentage * e.stat.HP;

                //Debug.Log(healAmount + "," + healPercentage);
                e.currhp = healAmount;

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

        public override bool isUseOnPlayerParty()
        {
            return true;
        }
    }
}
