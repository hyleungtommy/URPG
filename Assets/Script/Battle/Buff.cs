using UnityEngine;
using System;
namespace RPG
{
    public class Buff
    {
        public enum Type
        {
            HP = 0, MP = 1, ATK = 2, DEF = 3, MATK = 4, MDEF = 5, AGI = 6, DEX = 7
        }
        public Type type { get; set; }
        public float modifier { get; set; }
        public int rounds { get; set; }
        public int chance { get; set; }
        public bool isPositive
        {
            get
            {
                if (type == Type.HP || type == Type.MP)
                    return modifier < 0.0f;
                else
                    return modifier >= 1.0f;
            }
        }
        public Sprite Img
        {
            get
            {
                if (isPositive)
                {
                    //Debug.Log(SpriteManager.buffs[0]);
                    return SpriteManager.buffs[(int)type];
                }
                else
                    return SpriteManager.debuffs[(int)type];
            }
        }

        public Buff(Type type, float modifier, int rounds)
        {
            this.type = type;
            this.modifier = modifier;
            this.rounds = rounds;
            this.chance = -1;
        }

        public Buff(Type type, float modifier, int rounds, int chance)
        {
            this.type = type;
            this.modifier = modifier;
            this.rounds = rounds;
            this.chance = chance;
        }

        public void OnPassingRounds()
        {
            if (rounds > 0)
                rounds--;
        }

        public BasicStat getBuffedSet()
        {
            float percentage = modifier;
            if (type == Type.ATK)
            {
                return new BasicStat(1, 1, percentage, 1, 1, 1, 1, 1);
            }
            else if (type == Type.DEF)
            {
                return new BasicStat(1, 1, 1, percentage, 1, 1, 1, 1);
            }
            else if (type == Type.MATK)
            {
                return new BasicStat(1, 1, 1, 1, percentage, 1, 1, 1);
            }
            else if (type == Type.MDEF)
            {
                return new BasicStat(1, 1, 1, 1, 1, percentage, 1, 1);
            }
            else if (type == Type.AGI)
            {
                return new BasicStat(1, 1, 1, 1, 1, 1, percentage, 1);
            }
            else if (type == Type.DEX)
            {
                return new BasicStat(1, 1, 1, 1, 1, 1, 1, percentage);
            }

            return new BasicStat(1, 1, 1, 1, 1, 1, 1, 1);
        }

        public BasicStat getHPMPChange()
        {
            if (type == Type.HP)
            {
                return new BasicStat(modifier, 1, 1, 1, 1, 1, 1, 1);
            }
            else if (type == Type.MP)
            {
                return new BasicStat(1, modifier, 1, 1, 1, 1, 1, 1);
            }
            return null;
        }

        public float getApplyChance(Entity user, Entity target)
        {
            if (modifier > 0)
            {
                return 100;
            }
            if (chance > 0)
            {
                return chance;
            }
            float applyChance = Convert.ToSingle(Math.Log10((user.stat.MATK * 2.5f) / target.stat.MDEF));
            return applyChance;
        }

        public Buff create()
        {
            return new Buff(type, modifier, rounds);
        }
    }
}