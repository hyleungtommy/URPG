using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class EnemyTemplate
    {
        public string name;
        public string img;
        public int HP;
        public int MP;
        public int ATK;
        public int DEF;
        public int MATK;
        public int MDEF;
        public int AGI;
        public int DEX;
        public int DropEXP;
        public int id;
        public int DropMoney;

        public EnemyTemplate()
        {

        }

        public override string ToString()
        {
            return "name=" + name;
        }

        public EntityEnemy toEntity()
        {
            BasicStat stat = new BasicStat(HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX);
            stat = stat.multiply(Param.difficultyModifier[Game.difficulty]);
            EntityEnemy entity = new EntityEnemy(name, stat, Resources.Load<Sprite>("Enemy/" + img), DropEXP, DropMoney);
            entity.strengthLv = 2;
            return entity;
        }

        public EntityEnemy toEntity(int strengthLv, float mapAreaStrengthModifier)
        {
            BasicStat stat = new BasicStat(HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX);
            stat = stat.multiply(Constant.enemyStrengthModifier[strengthLv]);
            stat = stat.multiply(1 + mapAreaStrengthModifier);
            stat = stat.multiply(Param.difficultyModifier[Game.difficulty]);           
            //Debug.Log(name + "=" + stat.ToString());
            EntityEnemy entity = new EntityEnemy(name, stat, Resources.Load<Sprite>("Enemy/" + img), DropEXP, DropMoney);
            entity.strengthLv = strengthLv;
            return entity;
        }
    }
}