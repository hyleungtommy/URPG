using UnityEngine;
using System;
namespace RPG
{
    public class BuffState
    {

        private Buff[] buffState;
        public BuffState()
        {
            buffState = new Buff[8];
        }

        public void passRound(Entity user)
        {
            if (buffState[0] != null)
            {
                user.currhp += user.currhp * (buffState[0].modifier);
                if (user.currhp > user.stat.HP)
                    user.currhp = user.stat.HP;
            }
            if (buffState[1] != null)
            {
                user.currmp += user.currmp * (buffState[1].modifier);
                if (user.currmp > user.stat.MP)
                    user.currmp = user.stat.MP;
            }


            //Debug.Log ("pass");
            for (int i = 0; i < 8; i++)
            {
                if (buffState[i] != null)
                {
                    if (buffState[i].rounds == 0)
                    {
                        //Debug.Log ("remove " + i);
                        buffState[i] = null;
                    }
                    if (buffState[i] != null)
                        buffState[i].OnPassingRounds();
                }
            }



        }

        public BasicStat getBasicStat()
        {
            BasicStat set = new BasicStat(1, 1, 1, 1, 1, 1, 1, 1);
            for (int i = 0; i < 8; i++)
            {
                if (buffState[i] != null)
                {
                    set = set.multiply(buffState[i].getBuffedSet());
                }
            }
            //Debug.Log ("Buffed set:" + set.ToString());
            return set;
        }

        public void addBuff(Buff buff)
        {
            buffState[(int)buff.type] = buff.create();
        }

        public Buff getBuff(int i)
        {
            return buffState[i];
        }

    }
}