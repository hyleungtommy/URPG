using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class BuffTemplate
    {
        public string type;
        public float modifier;
        public int rounds;
        public int chance;

        public Buff toBuff()
        {
            Buff b = new Buff(getType(), modifier, rounds, chance);
            return b;
        }

        public Buff toBuff(float mod, int turn)
        {
            Buff b = new Buff(getType(), mod, turn, chance);
            return b;
        }

        public Buff.Type getType()
        {
            if (type == "ATK") return Buff.Type.ATK;
            else if (type == "AGI") return Buff.Type.AGI;
            else if (type == "DEF") return Buff.Type.DEF;
            else if (type == "DEX") return Buff.Type.DEX;
            else if (type == "HP") return Buff.Type.HP;
            else if (type == "MATK") return Buff.Type.MATK;
            else if (type == "MDEF") return Buff.Type.MDEF;
            else return Buff.Type.MP;
        }




    }
}