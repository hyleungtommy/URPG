using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RPG;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor
{
    public class BattleCharacterTest
    {

        [Test]
        public void ShouldCreateEntity(){
            Job testJob = new Job("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2});
            Sprite[]sprite = new Sprite[1];
            BattleCharacter character = new BattleCharacter("Tommy",sprite,null,testJob,true,1);
            EntityPlayer entity = character.toEntity();

            Assert.AreEqual("Tommy",entity.name);
        }

        [Test]
        public void ShouldLevelUpCharacter(){
            Job testJob = new Job("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2});
            Sprite[]sprite = new Sprite[1];
            BattleCharacter character = new BattleCharacter("Tommy",sprite,null,testJob,true,1);
            bool levelUp = character.assignEXP(500);
            Assert.AreEqual(true,levelUp);
        }

        [Test]
        public void ShouldNotLevelUpCharacter(){
            Job testJob = new Job("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2});
            Sprite[]sprite = new Sprite[1];
            BattleCharacter character = new BattleCharacter("Tommy",sprite,null,testJob,true,1);
            bool levelUp = character.assignEXP(10);
            Assert.AreEqual(false,levelUp);
        }

    }
}
