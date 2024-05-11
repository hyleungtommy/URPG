using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RPG;
using System.Linq;

namespace Editor
{
    public class SkillTest{
        SkillTemplate[] skillTemplates;
        BattleCharacter character;
        EnemyTemplate[] enemyTemplates;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            skillTemplates = JsonHelper.FromJson<SkillTemplate>(Resources.Load<TextAsset>("Data/Mock/mockSkill").text);
            enemyTemplates = JsonHelper.FromJson<EnemyTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEnemy").text);
            Sprite[]sprite = new Sprite[1];
            character = new BattleCharacter("Tommy",sprite,null,DB.jobs[0],true,1);
            DB.buffs = JsonHelper.FromJson<BuffTemplate>(Resources.Load<TextAsset>("Data/Mock/mockBuff").text);
        }

        [Test]
        public void ShouldUseAttackSkill(){
            SkillTemplate skillTemplate = skillTemplates[0];
            GeneralSkill generalSkill = skillTemplate.toGeneralSkill();
            EntityPlayer player = character.toEntity();
            EntityEnemy[]target = new EntityEnemy[]{enemyTemplates[0].toEntity()};
            Skill skill = generalSkill.toSkill();
            List<BattleMessage> msgs = skill.use(player, target);
            Assert.AreEqual(1, msgs.Count);
            Assert.Greater(msgs[0].value, 1);
        }

        [Test]
        public void ShouldUseAOEAttackSkill(){
            SkillTemplate skillTemplate = skillTemplates[4];
            GeneralSkill generalSkill = skillTemplate.toGeneralSkill();
            EntityPlayer player = character.toEntity();
            EntityEnemy[]target = new EntityEnemy[]{enemyTemplates[0].toEntity(), enemyTemplates[0].toEntity(), enemyTemplates[0].toEntity()};
            Skill skill = generalSkill.toSkill();
            List<BattleMessage> msgs = skill.use(player, target);
            Assert.AreEqual(3, msgs.Count);
            Assert.Greater(msgs[0].value, 1);
        }

        [Test]
        public void ShouldUseHealSkill(){
            SkillTemplate skillTemplate = skillTemplates.First((skilltemp)=> skilltemp.name.Equals("Heal"));
            GeneralSkill generalSkill = skillTemplate.toGeneralSkill();
            EntityPlayer player = character.toEntity();
            player.currhp = 1;
            Skill skill = generalSkill.toSkill();
            List<BattleMessage> msgs = skill.use(player, new Entity[]{player});
            Assert.AreEqual(1, msgs.Count);
            Assert.AreEqual(msgs[0].type, BattleMessage.Type.Heal);
            Assert.Greater(msgs[0].value, 1);
        }

        [Test]
        public void ShouldUseBuffSkill(){
            SkillTemplate skillTemplate = skillTemplates.First((skilltemp)=> skilltemp.name.Equals("HotBlooded"));
            GeneralSkill generalSkill = skillTemplate.toGeneralSkill();
            EntityPlayer player = character.toEntity();
            Skill skill = generalSkill.toSkill();
            List<BattleMessage> msgs = skill.use(player, new Entity[]{player});
            Assert.AreEqual(1, msgs.Count);
            Assert.AreEqual(msgs[0].type, BattleMessage.Type.Buff);
            Assert.AreEqual(1, player.buffState.Count);
        }

        [Test]
        public void ShouldUseDebuffSkill(){
            SkillTemplate skillTemplate = skillTemplates.First((skilltemp)=> skilltemp.name.Equals("Deshell"));
            GeneralSkill generalSkill = skillTemplate.toGeneralSkill();
            EntityPlayer player = character.toEntity();
            EntityEnemy[]target = new EntityEnemy[]{enemyTemplates[0].toEntity()};
            Skill skill = generalSkill.toSkill();
            List<BattleMessage> msgs = skill.use(player, target);
            Assert.AreEqual(1, msgs.Count);
            Assert.AreEqual(msgs[0].type, BattleMessage.Type.Debuff);
            Assert.AreEqual(1, target[0].buffState.Count);
        }

        [Test]
        public void ShouldUseMagicSkill(){
            SkillTemplate skillTemplate = skillTemplates.First((skilltemp)=> skilltemp.name.Equals("Fireball"));
            GeneralSkill generalSkill = skillTemplate.toGeneralSkill();
            EntityPlayer player = character.toEntity();
            EntityEnemy[]target = new EntityEnemy[]{enemyTemplates[0].toEntity()};
            Skill skill = generalSkill.toSkill();
            List<BattleMessage> msgs = skill.use(player, target);
            Assert.AreEqual(1, msgs.Count);
            Assert.Greater(msgs[0].value, 1);
        }


    }
}