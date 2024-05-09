using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using RPG;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor
{
    public class BuffTest
    {

        BuffTemplate[] buffTemplates;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            buffTemplates = JsonHelper.FromJson<BuffTemplate>(Resources.Load<TextAsset>("Data/Mock/mockBuff").text);
        }

        [Test]
        public void shouldCreateNewBuff()
        {
            BuffTemplate buffTemplate = buffTemplates[1];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            Assert.AreEqual(buffTemplate.name, buff.name);
            Assert.AreEqual("Buff", buff.type);
            Assert.AreEqual(10, buff.rounds);
            Assert.AreEqual(10, buff.modifier);
        }

        [Test]
        public void shouldCreateNewDebuff()
        {
            BuffTemplate buffTemplate = buffTemplates[15];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            Assert.AreEqual(buffTemplate.name, buff.name);
            Assert.AreEqual("Debuff", buff.type);
            Assert.AreEqual(10, buff.rounds);
            Assert.AreEqual(10, buff.modifier);
        }

        [Test]
        public void shouldGetCorrectBuffStat()
        {
            BuffTemplate buffTemplate = buffTemplates[15];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            BasicStat stat = buff.getBuffedSet();
            Assert.AreEqual(1, stat.HP);
            Assert.AreEqual(0.9, Math.Ceiling(stat.DEX * 10) / 10); // precision loss
        }

        [Test]
        public void shouldGetPositiveHPMPChangeForBuff()
        {
            BuffTemplate buffTemplate = buffTemplates[23];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            float[] hpmpChange = buff.getHPMPChange();
            Assert.AreEqual(Math.Round(1.1 * 10) / 10, Math.Round(hpmpChange[0] * 10) / 10);
        }

        [Test]
        public void shouldGetNegativeHPMPChangeForDebuff()
        {
            BuffTemplate buffTemplate = buffTemplates[14];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            float[] hpmpChange = buff.getHPMPChange();
            Assert.AreEqual(Math.Round(0.9 * 10) / 10, Math.Round(hpmpChange[0] * 10) / 10);
        }

        [Test]
        public void shouldAddNewBuff()
        {
            BuffTemplate buffTemplate = buffTemplates[15];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            BuffState buffState = new BuffState();
            buffState.addBuff(buff);
            Assert.AreEqual(1, buffState.Count);
        }

        [Test]
        public void shouldRemoveBuffWhenBuffEnds()
        {
            Job testJob = new Job("Adventurer", new int[] { 25, 25, 25, 12, 23 }, new int[] { 2, 2, 2, 2, 2 });
            Sprite[] sprite = new Sprite[1];
            BattleCharacter character = new BattleCharacter("Tommy", sprite, null, testJob, true, 1);
            EntityPlayer entityPlayer = character.toEntity();

            BuffTemplate buffTemplate = buffTemplates[15];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 1, buffTemplate.replace, buffTemplate.stackable, null);
            BuffState buffState = new BuffState();
            buffState.addBuff(buff);
            buffState.passRound(entityPlayer);
            Assert.AreEqual(0, buffState.Count);
        }

        [Test]
        public void shouldReplaceBuffWhenAddNewBuff()
        {
            BuffTemplate oldBuffTemplate = buffTemplates[23]; // Regen HP
            Buff oldBuff = new Buff(oldBuffTemplate.id, oldBuffTemplate.type, oldBuffTemplate.name, oldBuffTemplate.effects, 10, 10, oldBuffTemplate.replace, oldBuffTemplate.stackable, null);

            BuffTemplate newBuffTemplate = buffTemplates[14]; // Bleed
            Buff newBuff = new Buff(newBuffTemplate.id, newBuffTemplate.type, newBuffTemplate.name, newBuffTemplate.effects, 10, 10, newBuffTemplate.replace, newBuffTemplate.stackable, null);
            BuffState buffState = new BuffState();
            buffState.addBuff(oldBuff);
            buffState.addBuff(newBuff);
            Assert.AreEqual(1, buffState.Count);
            Assert.AreEqual("Bleeding", buffState.getBuff(0).name);
        }

        [Test]
        public void shouldBeStunIfStunBuffExist(){
            BuffTemplate buffTemplate = buffTemplates[0];
            Buff buff = new Buff(buffTemplate.id, buffTemplate.type, buffTemplate.name, buffTemplate.effects, 10, 10, buffTemplate.replace, buffTemplate.stackable, null);
            BuffState buffState = new BuffState();
            buffState.addBuff(buff);
            Assert.AreEqual(true, buffState.isStunned());
        }

    }
}