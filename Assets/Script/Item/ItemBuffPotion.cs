using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class ItemBuffPotion : FunctionalItem
    {
        public float healPercentage { get; set; }
        public int minHealAmount { get; set; }
        public List<Buff> buffEffects { get; set; }

        public override string ToString()
        {
            return "name=" + name;
        }

        public ItemBuffPotion(Sprite img) : base(img)
        {

        }

        public override int getMaxStack()
        {
            return 10;
        }

        public override string getTypeName()
        {
            return "Buff Potion";
        }

        public override bool isUseOnPlayerParty()
        {
            return true;
        }

        public override System.Collections.Generic.List<BattleMessage> use(Entity user, Entity[] target)
        {
            List<BattleMessage> bundle = new List<BattleMessage>();
            foreach (Buff b in buffEffects)
            {
                target[0].buffState.addBuff(b);
                Debug.Log(target[0].name + "buff" + b.type + "round" + b.rounds);
                BattleMessage message = new BattleMessage();
                message.sender = message.receiver = target[0];
                //message.value = (int)b.type;
                message.type = BattleMessage.Type.Buff;
                bundle.Add(message);
                //Debug.Log (healAmount);
            }
            return bundle;
        }


    }
}