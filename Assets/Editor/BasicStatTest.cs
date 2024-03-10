using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RPG;

namespace Editor
{
    public class BasicStatTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ShouldDoAdditionCorrectly()
        {
            BasicStat input = new BasicStat(100f,100f,10f,10f,10f,10f,10f,10f);
            BasicStat addition = new BasicStat (50f,50f,0f,0f,0f,0f,0f,0f);
            BasicStat expected = new BasicStat(150f,150f,10f,10f,10f,10f,10f,10f);
            BasicStat result = input.plus(addition);
            Assert.AreEqual(expected.HP,result.HP);
        }

        [Test]
        public void ShouldMultiplyWithSingleMultiplier()
        {
            BasicStat input = new BasicStat(100f,100f,0f,0f,0f,0f,0f,0f);
            BasicStat expected = new BasicStat(200f,200f,0f,0f,0f,0f,0f,0f);
            BasicStat result = input.multiply(2);
            Assert.AreEqual(expected.HP,result.HP);
        }

        [Test]
        public void ShouldMultiplyWithAnotherSet()
        {
            BasicStat input = new BasicStat(100f,100f,0f,0f,0f,0f,0f,0f);
            BasicStat addition = new BasicStat(1.5f,0f,0f,0f,0f,0f,0f,0f);
            BasicStat expected = new BasicStat(150f,200f,0f,0f,0f,0f,0f,0f);
            BasicStat result = input.multiply(addition);
            Assert.AreEqual(expected.HP,result.HP);
        }

    }
}
