using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Entity
    {
        public int id { get; set; }
        public string name { get; set; }
        public Sprite img { get; set; }
        public float currhp { get; set; }
        public float currmp { get; set; }
        public float curratb { get; set; }
        private BasicStat _stat;
        public BasicStat stat
        {
            get
            {
                return _stat.multiply(buffState.getBasicStat());
            }
            set
            {
                _stat = value;
            }
        }
        public bool isDefensing { get; set; }
        float atbSpeed;
        protected Entity[] opponent;
        public bool reflectiveDefense { get; set; }
        public float defenseModifier { get; set; }
        public BuffState buffState { get; set; }
        public Entity(string name, BasicStat stat, Sprite img)
        {
            this.name = name;
            this._stat = stat;
            this.img = img;
            currhp = stat.HP;
            currmp = stat.MP;
            curratb = 0f;
            atbSpeed = 0f;
            isDefensing = false;
            defenseModifier = 1.0f;
            buffState = new BuffState();
        }

        public void setOpponent(Entity entity)
        {
            opponent = new Entity[1];
            opponent[0] = entity;
        }

        public void setOpponent(Entity[] entity)
        {
            opponent = entity;
        }

        public bool tick(float opponentAvgAGI)
        {
            if (currhp > 0)
            {

                atbSpeed = UnityEngine.Random.Range((stat.AGI / opponentAvgAGI) * 2f, (stat.AGI / opponentAvgAGI) * 4.0f) * (isDefensing ? 0.5f : 1f);
                if (atbSpeed > 6.0f)
                    atbSpeed = 6.0f;
                if (atbSpeed < 1.0f)
                    atbSpeed = 1.0f;
                curratb += atbSpeed;
                //Debug.Log (name + "" + currATB);
                return (curratb >= 100.0f);
            }
            return false;
        }

        public virtual void takeAction(IFunctionable functionable)
        {

        }

        public virtual void onReceiveDamage(Entity attacker, float damage)
        {

        }

        public virtual void passRound()
        {
            buffState.passRound(this);
            //Debug.Log(name + " pass round");
        }

        public List<BattleMessage> useNormalAttack()
        {

            List<BattleMessage> msgs = new List<BattleMessage>();
            for (int j = 0; j < opponent.Length; j++)
            {

                BattleMessage atkMsg = new BattleMessage();
                atkMsg.sender = this;
                atkMsg.receiver = opponent[j];
                atkMsg.SkillAnimationName = "NormalAttack";
                float attackPower = (this.stat.ATK * 1 * UnityEngine.Random.Range(0.9f, 1.1f)) - (opponent[j].isDefensing ? opponent[j].stat.DEF * opponent[j].defenseModifier : opponent[j].stat.DEF);

                /*
				if (this is EntityPlayer && (this as EntityPlayer).havePassiveSkill (SkillPassive.BattleWill) && (CurrHP / Stat.HP) <= 1f) {
					attackPower += (int)(attackPower * (this as EntityPlayer).getPassiveSkill (SkillPassive.BattleWill).Mod * (1f - (CurrHP / Stat.HP)));
					Debug.Log ("Battle Will Power:" + (attackPower * (this as EntityPlayer).getPassiveSkill (SkillPassive.BattleWill).Mod * (1f - (CurrHP / Stat.HP))));
				}
				*/

                //				Debug.Log (name + " att power " + attackPower + " " + opponent [j].stat.DEF + " " + opponent [j].name);
                if (attackPower <= 0f) attackPower = 1f;
                float hitChance = this.stat.DEX / (opponent[j].stat.AGI * 2f);
                if (hitChance > 1.0f) hitChance = 1.0f;
                else if (hitChance <= 0.1f) hitChance = 0.1f;

                if (UnityEngine.Random.Range(0.0f, 1.0f) > hitChance)
                    atkMsg.type = BattleMessage.Type.Miss;
                else
                {
                    bool crititcal = false;
                    float critChance = Mathf.Log(this.stat.DEX / opponent[j].stat.AGI);
                    if (critChance < 0.05f)
                        critChance = 0.05f;
                    if (UnityEngine.Random.Range(0.0f, 1.0f) <= critChance)
                    {
                        crititcal = true;
                        attackPower *= (this.stat.DEX / opponent[j].stat.DEX) * 2;
                    }
                    opponent[j].currhp -= attackPower;
                    opponent[j].onReceiveDamage(this, attackPower);
                    if (crititcal)
                        atkMsg.type = BattleMessage.Type.Critical;
                    else
                        atkMsg.type = BattleMessage.Type.NormalAttack;
                    if (opponent[j].currhp < 0)
                        opponent[j].currhp = 0;

                    atkMsg.value = attackPower;

                    if (opponent[j].isDefensing && opponent[j].reflectiveDefense)
                    {
                        currhp -= attackPower * 0.1f;
                        BattleMessage refDefMessage = new BattleMessage();
                        refDefMessage.sender = refDefMessage.receiver = this;
                        refDefMessage.value = attackPower * 0.1f;
                        refDefMessage.type = BattleMessage.Type.NormalAttack;
                        msgs.Add(refDefMessage);
                    }

                }
                msgs.Add(atkMsg);
            }
            curratb = 0;


            //Debug.Log (msgs.Count);

            return msgs;
        }

    }
}