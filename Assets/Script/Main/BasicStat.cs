using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    /// <summary>
    /// A set that contains 8 stat that is used during battle, used by Entity
    /// </summary>
    public class BasicStat
    {
        private float hp, mp, atk, def, matk, mdef, agi, dex;
        public float HP { get { return hp; } }
        public float MP { get { return mp; } }
        public float ATK { get { return atk; } }
        public float DEF { get { return def; } }
        public float MATK { get { return matk; } }
        public float MDEF { get { return mdef; } }
        public float AGI { get { return agi; } }
        public float DEX { get { return dex; } }

        public BasicStat(float hp, float mp, float atk, float def, float matk, float mdef, float agi, float dex)
        {
            this.hp = hp;
            this.mp = mp;
            this.atk = atk;
            this.def = def;
            this.matk = matk;
            this.mdef = mdef;
            this.agi = agi;
            this.dex = dex;
        }

        /// <summary>
        /// Do addition with another set
        /// </summary>
        /// <param name="set">Another set to be added.</param>
        /// <returns>The modified set.</returns>
        public BasicStat plus(BasicStat set)
        {
            return new BasicStat(hp + set.hp, mp + set.mp, atk + set.atk, def + set.def, matk + set.matk, mdef + set.mdef, agi + set.agi, dex + set.dex);
        }

        /// <summary>
        /// Do multiplication with another set
        /// </summary>
        /// <param name="set">Another set to be multiply.</param>
        /// <returns>The modified set.</returns>
        public BasicStat multiply(BasicStat set)
        {
            return new BasicStat(hp * set.hp, mp * set.mp, atk * set.atk, def * set.def, matk * set.matk, mdef * set.mdef, agi * set.agi, dex * set.dex);
        }

        /// <summary>
        /// multiply all the stat in the set with a multiplier
        /// </summary>
        /// <param name="multiplier">A multiply to multiply all set values</param>
        /// <returns>The modified set.</returns>
        public BasicStat multiply(float multiplier)
        {
            return new BasicStat(hp * multiplier, mp * multiplier, atk * multiplier, def * multiplier, matk *  multiplier, mdef * multiplier, agi * multiplier, dex * multiplier);
        }

        public override string ToString()
        {
            return string.Format("[HP={0}, MP={1}, ATK={2}, DEF={3}, MATK={4}, MDEF={5}, AGI={6}, DEX={7}]", HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX);
        }
    }
}
