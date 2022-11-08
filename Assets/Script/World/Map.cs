using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class Map
    {
        public int id { get; set; }
        public string name { get; }
        public string desc { get; }
        public Sprite bgImg { get; }
        public Sprite battleImg { get; }
        public int reqLv { get; }
        public int maxLv { get; }
        public int maxArea { get; }
        public EnemyTemplate[] enemyList { get; }
        public int[] appearChance { get; }
        public EnemyTemplate boss { get; }
        public int currArea { get; set; }
        public string townName { get; set; }
        public Sprite townbg { get; set; }
        public string[] townFacility { get; set; }
        public bool unlocked { get; set; }
        public Map(int id, string name, string desc, Sprite bgImg, Sprite battleImg, int reqLv, int maxLv, int maxArea, EnemyTemplate[] enemyList, int[] appearChance, EnemyTemplate boss)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.bgImg = bgImg;
            this.maxArea = maxArea;
            this.battleImg = battleImg;
            this.reqLv = reqLv;
            this.maxLv = maxLv;
            this.appearChance = appearChance;
            this.enemyList = enemyList;
            this.boss = boss;
            this.currArea = 1;
        }

        public override string ToString()
        {
            return "id=" + id + "\nname=" + name + "\ndesc=" + desc + "\nbgImg=" + bgImg + "\nbattleImg=" + battleImg + "\nmaxArea=" + maxArea + "\nreqLv=" + reqLv + "\nmaxLv=" + maxLv + "\ncurrArea=" + currArea;
        }

        public EntityEnemy[] generateEnemy()
        {
            List<EntityEnemy> generatedEnemyList = new List<EntityEnemy>();
            if (Game.currLoc.currArea == Game.currLoc.maxArea)
            {
                generatedEnemyList.Add(boss.toEntity());
            }
            else
            {
                int maxEnemy = 5;
                int enemyNum = UnityEngine.Random.Range(1, maxEnemy);
                float mapEnemyModifier = (currArea - 1) * 0.02f;
                if (mapEnemyModifier > Param.maxMapEnemyModifier)
                {
                    mapEnemyModifier = Param.maxMapEnemyModifier;
                }

                for (int i = 0; i < enemyNum; i++)
                {
                    //int enemyStrength = RPGUtil.getRandomIndexFrom(RPGParameter.DIFFICULTY_ENEMY_STRENGTH_CHANCE[RPGSystem.difficulty], 100f);

                    EntityEnemy enemy = enemyList[Util.getRandomIndexFrom(appearChance, 100f)].toEntity(0, mapEnemyModifier);
                    generatedEnemyList.Add(enemy);
                }
            }

            return generatedEnemyList.ToArray();
        }

        public void progressArea()
        {
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                if (currArea < maxArea)
                {
                    currArea = currArea + 1;
                }
            }
        }

        public void resetAreaStatus()
        {
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                if (currArea == maxArea)
                {
                    currArea = 1;
                }
                else if (currArea % 5 != 0) // if current area is not 0,5,10,15,...
                {
                    currArea = Mathf.FloorToInt((float)currArea / 5f) * 5;
                    if (currArea < 1) currArea = 1;
                }
                //Debug.Log("new currArea = " + currArea);
                //RPGSystem.saveGame();
            }
        }


    }
}