using UnityEngine;
using System;
namespace RPG
{
    /// <summary>
    /// Represent a buff instance, initalize by ApplyBuffTemplate
    /// </summary>
    public class Buff
    {
        public enum Type
        {
            HP = 0, MP = 1, ATK = 2, DEF = 3, MATK = 4, MDEF = 5, AGI = 6, DEX = 7, Stun, Summon_Zombie, Summon_Skeleton, Summon_Dark_Spirit
        }
        public int id {get;set;}
        public string type { get; set; }
        public string name { get; set; }
        public Type[] effects { get; set; }
        public int modifier { get; set; }
        public int rounds { get; set; }
        public int chance { get; set; }
        public int[] replace { get; set; }
        public bool stackable { get; set; }
        public Sprite Img { set; get; }

        public Buff(int id,string type, string name, Type[]effects, int modifier, int rounds, int[] replace, bool stackable, Sprite img)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.effects = effects;
            this.modifier = modifier;
            this.rounds = rounds;
            this.replace = replace;
            this.stackable = stackable;
            this.Img = img;
            this.chance = -1;
        }
        public Buff(int id,string type, string name, string[]effects, int modifier, int rounds, int[] replace, bool stackable, Sprite img)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.effects = getEffects(effects);
            this.modifier = modifier;
            this.rounds = rounds;
            this.replace = replace;
            this.stackable = stackable;
            this.Img = img;
            this.chance = -1;
        }

        public Buff(int id,string type, string name, string[]effects, int modifier, int rounds, int[] replace, bool stackable, Sprite img, int chance)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.effects = getEffects(effects);
            this.modifier = modifier;
            this.rounds = rounds;
            this.replace = replace;
            this.stackable = stackable;
            this.Img = img;
            this.chance = chance;
        }

        /// <summary>
        /// Reduce the buff round by one
        /// </summary>
        public void OnPassingRounds()
        {
            if (rounds > 0)
                rounds--;
        }

        /// <summary>
        /// calculate the final stat for all effects exists in this buff
        /// </summary>
        /// <returns>The final stat object from this buff</returns>
        public BasicStat getBuffedSet()
        {
            BasicStat stat = new BasicStat(1, 1, 1, 1, 1, 1, 1, 1);
            foreach (Type effect in effects)
            {
                float percentage = getPercentageByModifier(type,modifier);
                if (effect == Type.ATK)
                {
                    stat = stat.multiply(new BasicStat(1, 1, percentage, 1, 1, 1, 1, 1));
                }
                else if (effect == Type.DEF)
                {
                    stat = stat.multiply(new BasicStat(1, 1, 1, percentage, 1, 1, 1, 1));
                }
                else if (effect == Type.MATK)
                {
                    stat = stat.multiply(new BasicStat(1, 1, 1, 1, percentage, 1, 1, 1));
                }
                else if (effect == Type.MDEF)
                {
                    stat = stat.multiply(new BasicStat(1, 1, 1, 1, 1, percentage, 1, 1));
                }
                else if (effect == Type.AGI)
                {
                    stat = stat.multiply(new BasicStat(1, 1, 1, 1, 1, 1, percentage, 1));
                }
                else if (effect == Type.DEX)
                {
                    stat = stat.multiply(new BasicStat(1, 1, 1, 1, 1, 1, 1, percentage));
                }
            }
            return stat;
        }

        private float getPercentageByModifier(string type, int modifier)
        {
            if (type.Equals("Buff"))
            {
                return 1f + modifier / 100f;
            }
            else
            {
                return 1f - modifier / 100f;
            }
        }

        private Type[] getEffects(string[] effects){
            Type[] effs = new Type[effects.Length];
            for(int i = 0 ; i < effects.Length ; i++){
                if(effects[i].Equals("Stun")){
                    effs[i] = Type.Stun;
                }else if(effects[i].Equals("HP")){
                    effs[i] = Type.HP;
                }else if(effects[i].Equals("MP")){
                    effs[i] = Type.MP;
                }else if(effects[i].Equals("ATK")){
                    effs[i] = Type.ATK;
                }else if(effects[i].Equals("MATK")){
                    effs[i] = Type.MATK;
                }else if(effects[i].Equals("DEF")){
                    effs[i] = Type.DEF;
                }else if(effects[i].Equals("MDEF")){
                    effs[i] = Type.MDEF;
                }else if(effects[i].Equals("AGI")){
                    effs[i] = Type.AGI;
                }else if(effects[i].Equals("DEX")){
                    effs[i] = Type.DEX;
                }
            }
            return effs;
        }

        /// <summary>
        /// calculate the increase/decrease of HP MP percentage from all effects exists in this buff
        /// </summary>
        /// <returns>A float array of hp,mp change by percentage</returns>
        public float[] getHPMPChange()
        {
            float[]hpmpChange = {1f,1f};
            foreach (Type effect in effects)
            {
                float percentage = getPercentageByModifier(type,modifier);
                if (effect == Type.HP)
                {
                    hpmpChange[0] = percentage;
                }
                else if (effect == Type.MP)
                {
                    hpmpChange[1] = percentage;
                }
            }

            return hpmpChange;
        }

        /// <summary>
        /// calculate the chance of applying this buff using user's MATK and opponenet's MDEF
        /// </summary>
        /// <returns>chance of applying this buff in percentage</returns>
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
            return new Buff(id,type, name, effects, modifier, rounds, replace,stackable,Img);
        }

        public override string ToString()
        {
            return "buff id=" + id + " name=" + name + " modifer=" + modifier + " rounds=" + rounds;
        }
    }
}