using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class MapTemplate
    {
        public int id;
        public string name;
        public string desc;
        public string bgImg;
        public string battleImg;
        public int reqLv;
        public int maxLv;
        public int maxArea;
        public int[] enemyList;
        public int[] appearChance;
        public int boss;
        public string townName;
        public string townbg;
        public string[] townFacility;
        public int rareEnemy;
        public MapTemplate()
        {
        }

        public override string ToString()
        {
            return "name=" + name + "desc=" + desc + "reqlv=" + reqLv + "maxLv=" + maxLv + "maxArea=" + maxArea + "enemyList=" + Util.printIntArray(enemyList) + "appearChance=" + Util.printIntArray(appearChance);
        }

        public Map toMap(EnemyTemplate[] enemyTemplates)
        {

            List<EnemyTemplate> list = new List<EnemyTemplate>();
            for (int i = 0; i < enemyList.Length; i++)
            {
                list.Add(enemyTemplates[enemyList[i] - 1]);
            }
            EnemyTemplate boss = enemyTemplates[this.boss - 1];
            EnemyTemplate rareEnemy = (this.rareEnemy == 0 ? null : enemyTemplates[this.rareEnemy - 1]);
            Map m = new Map(id, name, desc, Resources.Load<Sprite>("Background/Map BG/" + bgImg), Resources.Load<Sprite>("Background/Battle BG/" + battleImg), reqLv, maxLv, maxArea, list.ToArray(), appearChance, boss,rareEnemy);
            m.townFacility = townFacility;
            m.townName = townName;
            m.townbg = Resources.Load<Sprite>("Background/VillageBG/" + townbg);
            m.unlocked = true;
            //if (id == 0) m.unlocked = true;
            //else m.unlocked = false;
            return m;
        }
    }
}