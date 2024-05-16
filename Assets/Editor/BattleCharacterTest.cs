using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using RPG;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor
{
    public class BattleCharacterTest
    {
        BattleCharacter character;
        
        [SetUp]
        public void Setup(){
            Job testJob = new Job("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2}, new int[]{});
            Sprite[]sprite = new Sprite[1];
            character = new BattleCharacter("Tommy",sprite,null,testJob,true,1);
        }

        [Test]
        public void ShouldCreateEntity(){
            EntityPlayer entity = character.toEntity();
            Assert.AreEqual("Tommy",entity.name);
        }

        [Test]
        public void ShouldLevelUpCharacter(){
            bool levelUp = character.assignEXP(500);
            Assert.AreEqual(true,levelUp);
        }

        [Test]
        public void ShouldNotLevelUpCharacter(){
            bool levelUp = character.assignEXP(10);
            Assert.AreEqual(false,levelUp);
        }

        [Test]
        public void ShouldIncreaseCharacterStatAfterAssignUpgradePoint(){
            int staminaBeforeAssign = character.stat.stamina;
            int[]upptAllocation = {5,1,1,1,1};
            character.assignUPPT(upptAllocation);
            Assert.AreEqual(9,character.upptSpend);
            Assert.AreEqual(true, staminaBeforeAssign < character.stat.stamina);
        }

    }
}
