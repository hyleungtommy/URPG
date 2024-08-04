using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RPG;
using System;

namespace Editor
{
    public class MapTest
    {
        Map map;
        [OneTimeSetUp]
        public void OneTimeSetUp(){
            TextAsset enemyJSON = Resources.Load<TextAsset>("Data/Mock/mockEnemy");
            EnemyTemplate[]enemyTemplates = JsonHelper.FromJson<EnemyTemplate>(enemyJSON.text);
            EnemyTemplate[]enemyList = new EnemyTemplate[5];
            Array.Copy(enemyTemplates,enemyList,5);
            int[] appearChance = {100,0,0,0,0};
            map = new Map(0,"test map","",null,null,1,10,20,enemyList,appearChance,enemyList[4],enemyList[2],0);
            Game.currLoc = map;
        }

        [Test]
        public void ShouldGenerateEnemy()
        {
            map.currZone = 1;
            EntityEnemy[]generatedEnemy = map.generateEnemy();
            Assert.AreEqual(true,generatedEnemy.Length > 0 && generatedEnemy.Length <=5);
            Assert.AreEqual("Slime",generatedEnemy[0].name);
        }

        [Test]
        public void ShouldProgressZone()
        {
            Game.currentMapMode = Constant.MapModeProgressive;
            map.currZone = 1;
            map.progressZone();
            Assert.AreEqual(2,map.currZone);
        }

        [Test]
        public void ShouldResetAreaStatusWhenInLastZone()
        {
            Game.currentMapMode = Constant.MapModeProgressive;
            map.currZone = 20;
            map.resetZoneStatus();
            Assert.AreEqual(1,map.currZone);
        }

        [Test]
        public void ShouldMoveBackToLast5thAreaWhenNotInLastZone()
        {
            Game.currentMapMode = Constant.MapModeProgressive;
            map.currZone = 17;
            map.resetZoneStatus();
            Assert.AreEqual(15,map.currZone);
        }

        [Test]
        public void ShouldMoveBackToFirstZoneWhenInLessThen5Zone()
        {
            Game.currentMapMode = Constant.MapModeProgressive;
            map.currZone = 4;
            map.resetZoneStatus();
            Assert.AreEqual(1,map.currZone);
        }

        [Test]
        public void ShouldGenerateBossInLastZone()
        {
            Game.currentMapMode = Constant.MapModeProgressive;
            map.currZone = 20;
            EntityEnemy[]enemyList = map.generateEnemy();
            Assert.AreEqual(1,enemyList.Length);
            Assert.AreEqual("Angry Bull",enemyList[0].name);
        }
    }
}
