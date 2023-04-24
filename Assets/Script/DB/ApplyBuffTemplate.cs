using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class ApplyBuffTemplate{
        public int buffId;
        public int modifier;
        public int rounds;
        public int chance;

        public Buff toBuff()
        {
            //Buff b = new Buff(getType(), modifier, rounds, chance);
            BuffTemplate buffTemplate = DB.buffs[buffId - 1];
            return new Buff(buffId,buffTemplate.type,buffTemplate.name,buffTemplate.effects,modifier,rounds,buffTemplate.replace,buffTemplate.stackable,SpriteManager.buffImgs[buffTemplate.img]);
        }

        public Buff toBuff(int mod, int turn)
        {
            //Buff b = new Buff(getType(), mod, turn, chance);
            BuffTemplate buffTemplate = DB.buffs[buffId - 1];
            return new Buff(buffId,buffTemplate.type,buffTemplate.name,buffTemplate.effects,mod,turn,buffTemplate.replace,buffTemplate.stackable,SpriteManager.buffImgs[buffTemplate.img]);
        }
    }
}