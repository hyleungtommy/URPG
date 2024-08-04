using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    /// <summary>
    /// Represent a map
    /// </summary>
    public class Map
    {
        public int id { get; set; }
        public string name { get; }
        public string desc { get; }
        public Sprite bgImg { get; }
        public Sprite battleImg { get; }
        public int reqLv { get; }
        public int maxLv { get; }
        public int maxZone { get; }
        public EnemyTemplate[] enemyList { get; }
        public int[] appearChance { get; }
        public EnemyTemplate boss { get; }
        public EnemyTemplate rareEnemy { get; }
        public int currZone { get; set; }
        public string townName { get; set; }
        public Sprite townbg { get; set; }
        public string[] townFacility { get; set; }
        public bool unlocked { get; set; }
        public int platinumCoinGain {get; set;}
        public int dungeonId {get; set;}
        public Map(int id, string name, string desc, Sprite bgImg, Sprite battleImg, int reqLv, int maxLv, int maxZone, EnemyTemplate[] enemyList, int[] appearChance, EnemyTemplate boss, EnemyTemplate rareEnemy, int dungeonId)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.bgImg = bgImg;
            this.maxZone = maxZone;
            this.battleImg = battleImg;
            this.reqLv = reqLv;
            this.maxLv = maxLv;
            this.appearChance = appearChance;
            this.enemyList = enemyList;
            this.boss = boss;
            this.currZone = 1;
            this.rareEnemy = rareEnemy;
            this.dungeonId = dungeonId;
        }

        public override string ToString()
        {
            return "id=" + id + "\nname=" + name + "\ndesc=" + desc + "\nbgImg=" + bgImg + "\nbattleImg=" + battleImg + "\nmaxArea=" + maxZone + "\nreqLv=" + reqLv + "\nmaxLv=" + maxLv + "\ncurrArea=" + currZone;
        }

        /// <summary>
        /// Generate enemies for next zone, according to which zone player is in
        /// </summary>
        /// <returns>a list of up to 5 Entity Enemy</returns>
        public EntityEnemy[] generateEnemy()
        {
            List<EntityEnemy> generatedEnemyList = new List<EntityEnemy>();
            //if player is in last zone, generate boss enemy
            if (Game.currLoc.currZone == Game.currLoc.maxZone)
            {
                generatedEnemyList.Add(boss.toEntity());
            }
            else
            {
                //determine if a rare enemy will be generated
                int rndRareEnemy = UnityEngine.Random.Range(0, 100);
                if (rareEnemy != null && rndRareEnemy <= Param.rareEnemyAppearChance)
                {
                    Game.rareEnemyAppeared = true;
                    generatedEnemyList.Add(rareEnemy.toEntity());
                }
                else
                {
                    int maxEnemy = 5;
                    int enemyNum = UnityEngine.Random.Range(1, maxEnemy);
                    float mapEnemyModifier = (currZone - 1) * 0.02f;
                    if (mapEnemyModifier > Param.maxMapEnemyModifier)
                    {
                        mapEnemyModifier = Param.maxMapEnemyModifier;
                    }

                    for (int i = 0; i < enemyNum; i++)
                    {
                        int monsterCodexValue = Game.globalBuffManager.GetMonsterCodexValue();
                        int rndEnemyStrength = 0;
                        if(monsterCodexValue == 1){
                            rndEnemyStrength = UnityEngine.Random.Range(3, 5);
                        }else if(monsterCodexValue == -1){
                            rndEnemyStrength = UnityEngine.Random.Range(0, 3);
                        }else{
                            rndEnemyStrength = UnityEngine.Random.Range(0, 5);
                        }
                        EntityEnemy enemy = enemyList[Util.getRandomIndexFrom(appearChance, 100f)].toEntity(rndEnemyStrength, mapEnemyModifier);
                        generatedEnemyList.Add(enemy);
                    }
                }

            }

            return generatedEnemyList.ToArray();
        }

        /// <summary>
        /// Move to the next zone
        /// </summary>
        public void progressZone()
        {
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                if (currZone < maxZone)
                {
                    currZone = currZone + 1;
                }
            }
        }

        /// <summary>
        /// When player reached last zone or leave during progression, move them back to first zone or the last 5th zone
        /// </summary>
        public void resetZoneStatus()
        {
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                if (currZone == maxZone)
                {
                    currZone = 1;
                }
                else if (currZone % 5 != 0) // if current area is not 0,5,10,15,... Move back to the last 5th zone
                {
                    currZone = Mathf.FloorToInt((float)currZone / 5f) * 5;
                    if (currZone < 1) currZone = 1;
                }
            }
        }


    }
}