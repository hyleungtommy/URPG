using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    public class SkillAttack : Skill
    {

        public SkillAttack(Sprite img) : base(img)
        {


        }

        public override List<BattleMessage> use(Entity user, Entity[] target)
        {
            base.use(user, target);

            List<BattleMessage> msgs = new List<BattleMessage>();
            for (int l = 0; l < target.Length; l++)
            {
                Entity opponent = target[l];
                for (int i = 0; i < turn; i++)
                {
                    BattleMessage atkMsg = new BattleMessage();
                    atkMsg.sender = user;
                    atkMsg.receiver = opponent;
                    atkMsg.SkillAnimationName = animation;
                    atkMsg.AOE = aoe;
                    atkMsg.SkillName = name;
                    int attackPower = (int)((user.stat.ATK * 1 * UnityEngine.Random.Range(0.9f, 1.1f) * mod) - (opponent.isDefensing ? opponent.stat.DEF * opponent.defenseModifier : opponent.stat.DEF));
                    //if (user is EntityPlayer && (user as EntityPlayer).havePassiveSkill(SkillPassive.BattleWill) && (user.CurrHP / user.Stat.HP) <= 1f)
                    //{
                    //   attackPower += (int)(attackPower * (1f - (user.CurrHP / user.Stat.HP)));
                    //    Debug.Log("Battle Will Power:" + (1f - (user.CurrHP / user.Stat.HP)));
                    //}
                    if (attackPower <= 0)
                        attackPower = 1;
                    float hitChance = user.stat.DEX / (opponent.stat.AGI * 1.4f);
                    if (hitChance > 1.0f)
                        hitChance = 1.0f;
                    else if (hitChance <= 0.1f)
                        hitChance = 0.5f;

                    if (UnityEngine.Random.Range(0.0f, 1.0f) > hitChance)
                        atkMsg.type = BattleMessage.Type.Miss;
                    else
                    {
                        bool crititcal = false;
                        float critChance = Mathf.Log((float)user.stat.DEX / (float)opponent.stat.AGI);
                        if (critChance < 0.05f)
                            critChance = 0.05f;
                        if (UnityEngine.Random.Range(0.0f, 1.0f) <= critChance)
                        {
                            crititcal = true;
                            attackPower *= (int)((user.stat.DEX / opponent.stat.DEX) * 2);
                        }
                        opponent.currhp -= attackPower;
                        if (crititcal)
                            atkMsg.type = BattleMessage.Type.Critical;
                        else
                            atkMsg.type = BattleMessage.Type.NormalAttack;
                        if (opponent.currhp < 0)
                            opponent.currhp = 0;

                        atkMsg.value = attackPower;

                        if (opponent.isDefensing && opponent.reflectiveDefense)
                        {
                            user.currhp -= attackPower * 0.1f;
                            BattleMessage refDefMessage = new BattleMessage();
                            refDefMessage.sender = refDefMessage.receiver = user;
                            refDefMessage.value = attackPower * 0.1f;
                            refDefMessage.type = BattleMessage.Type.NormalAttack;
                            msgs.Add(refDefMessage);
                        }
                        /*
                        if (user is EntityPlayer && (user as EntityPlayer).havePassiveSkill("Life Absorpation"))
                        {

                            int healValue = (int)(user.Stat.HP * ((user as EntityPlayer).getPassiveSkill("Life Absorpation").Mod));
                            if (user.CurrHP + healValue > user.Stat.HP)
                                healValue = (int)(user.Stat.HP - user.CurrHP);
                            user.CurrHP += healValue;
                            Debug.Log("Life :" + healValue);
                            if (healValue > 0)
                            {
                                BattleMessage healMsg = new BattleMessage();
                                healMsg.sender = user;
                                healMsg.receiver = user;
                                healMsg.type = BattleMessage.Type.Heal;
                                //healMsg.SkillAnimationName = animation;
                                healMsg.AOE = false;
                                healMsg.SkillName = "";
                                healMsg.value = healValue;
                                msgs.Add(healMsg);
                            }
                        }
                        */

                        //applyBuff (user, opponent);


                    }
                    msgs.Add(atkMsg);
                }
            }
            //			Debug.Log (msgs);
            return msgs;
        }

        public override bool isAttackSkill()
        {
            return true;
        }

    }
}