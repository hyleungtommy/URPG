using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RPG;

namespace Editor
{
    public class PartyTest
    {
        Party party;

        [SetUp]
        public void Setup(){
            Param.unlockAllCharacter = false;
            party = new Party();
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ShouldGetAllInitialBattleCharacter()
        {
            BattleCharacter[] characters = party.getAllBattleCharacter();
            Assert.AreEqual(2, characters.Length);
            Assert.AreEqual("tommy", characters[0].name);
            Assert.AreEqual("Anson", characters[1].name);
        }

        [Test]
        public void ShouldGetAllAvaiableBattleCharacter()
        {
            BattleCharacter[] characters = party.getAllUnlockedCharacter();
            Assert.AreEqual(8, characters.Length);
            Assert.AreEqual("tommy", characters[0].name);
            Assert.AreEqual("Anson", characters[1].name);
            Assert.AreEqual(null, characters[2]);
        }
        
        [Test]
        public void ShouldGetBattleEntityList()
        {
            EntityPlayer[] entityList = party.createBattleParty();
            Assert.AreEqual(2, entityList.Length);
            Assert.AreEqual("tommy", entityList[0].name);
            Assert.AreEqual("Anson", entityList[1].name);
        }

        [Test]
        public void ShouldUnlockCharacter(){
            party.unlockCharacter(2);
            BattleCharacter[] characters = party.getAllBattleCharacter();
            Assert.AreEqual(3, characters.Length);
            Assert.AreEqual("Simon", characters[2].name);
        }
    }
}
