using UnityEngine;
using System;
using System.Collections.Generic;
namespace RPG
{
    /// <summary>
    /// Manage all buffs currently exist in an entity
    /// </summary>
    public class BuffState
    {
        public int Count { get { return buffState.Count; } }
        private List<Buff> buffState;
        public BuffState()
        {
            buffState = new List<Buff>();
        }

        /// <summary>
        /// cause all buff to take effect on an entity
        /// </summary>
        public void passRound(Entity user)
        {
            //handle hp/mp change
            float[] hpmpChange = getTotalHPMPChange();
            user.currhp = user.currhp * hpmpChange[0];
            if (user.currhp > user.stat.HP)
                user.currhp = user.stat.HP;
            user.currmp = user.currmp * hpmpChange[1];
            if (user.currmp > user.stat.MP)
                user.currmp = user.stat.MP;

            //reduce round by 1 and remove buff when it is expired
            foreach (Buff buff in buffState.ToArray())
            {
                if (buff.rounds <= 1)
                {
                    buffState.Remove(buff);
                }
                else
                {
                    buff.OnPassingRounds();
                }
            }
        }
        /// <summary>
        /// get the final stat modifer from all buff that exists in the entity
        /// </summary>
        /// <returns>A stat object represent the stat modifier that will apply to the entity</returns>
        public BasicStat getBasicStat()
        {
            BasicStat set = new BasicStat(1, 1, 1, 1, 1, 1, 1, 1);
            foreach (Buff buff in buffState)
            {
                set = set.multiply(buff.getBuffedSet());
            }
            //Debug.Log ("Buffed set:" + set.ToString());
            return set;
        }

        /// <summary>
        /// add a buff to the eneity, also replace opposite buff
        /// </summary>
        public void addBuff(Buff newBuff)
        {

            foreach (Buff buff in buffState.ToArray())
            {
                //if buff exists, remove old buff and add new buff to refresh the rounds
                if (buff.id == newBuff.id)
                {
                    buffState.Remove(buff);
                }
                //replace buff as stated in buff.replace
                foreach (int replace in newBuff.replace)
                {
                    if (buff.id == replace)
                    {
                        buffState.Remove(buff);
                    }
                }
            }
            buffState.Add(newBuff.create());
        }

        public Buff getBuff(int i)
        {
            return buffState[i];
        }

        /// <summary>
        /// Check if a buff exist
        /// </summary>
        /// <returns>Where a buff with buff id exist</returns>
        public bool isBuffExists(int buffId)
        {
            foreach (Buff buff in buffState.ToArray())
            {
                if (buff.id == buffId) return true;
            }
            return false;
        }

        /// <summary>
        /// Check if entity is currently stunned
        /// </summary>
        /// <returns>Where entity is stunned</returns>
        public bool isStunned()
        {
            foreach (Buff buff in buffState.ToArray())
            {
                foreach (Buff.Type effect in buff.effects)
                {
                    if (effect == Buff.Type.Stun)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private float[] getTotalHPMPChange()
        {
            float[] hpmpChange = { 1f, 1f };
            foreach (Buff buff in buffState)
            {
                float[] hpmpChangePerBuff = buff.getHPMPChange();
                hpmpChange[0] = hpmpChange[0] * hpmpChangePerBuff[0];
                hpmpChange[1] = hpmpChange[1] * hpmpChangePerBuff[1];
            }
            return hpmpChange;
        }

        public void removeAllDebuff()
        {
            foreach (Buff buff in buffState.ToArray())
            {
                if (buff.type == "Debuff")
                {
                    buffState.Remove(buff);
                }
            }
        }

        /// <summary>
        /// remove all buff that is classified as Summon, use for Sacrifice Summon skill
        /// </summary>
        public void removeSummons()
        {
            foreach (Buff buff in buffState.ToArray())
            {
                if (buff.id == 32 || buff.id == 33 || buff.id == 34)
                {
                    buffState.Remove(buff);
                }
            }
        }

        public override string ToString()
        {
            return Util.printList<Buff>(buffState);
        }

    }
}