using UnityEngine;
using System;
using System.Collections.Generic;
namespace RPG
{
    public class BuffState
    {
        public int Count { get { return buffState.Count; } }
        private List<Buff> buffState;
        public BuffState()
        {
            buffState = new List<Buff>();
        }

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
                if (buff.rounds == 0)
                {
                    buffState.Remove(buff);
                }
                else
                {
                    buff.OnPassingRounds();
                }
            }
        }

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
                foreach (int replace in buff.replace)
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

        public bool isBuffExists(int buffId){
            foreach (Buff buff in buffState.ToArray())
            {
                if(buff.id == buffId) return true;
            }
            return false;
        }

        public bool isStunned(){
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

        public void removeAllDebuff(){
            foreach (Buff buff in buffState.ToArray())
            {
                if(buff.type == "Debuff"){
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