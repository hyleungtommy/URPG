using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RPG;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Editor
{
    public class ItemTest{

        ItemTemplate[] itemTemplates;
        BattleCharacter character;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            itemTemplates = JsonHelper.FromJson<ItemTemplate>(Resources.Load<TextAsset>("Data/Mock/mockItem").text);
            Job testJob = new Job("Adventurer",new int[]{25,25,25,12,23},new int[]{2,2,2,2,2});
            Sprite[]sprite = new Sprite[1];
            character = new BattleCharacter("Tommy",sprite,null,testJob,true,1);
            DB.buffs = JsonHelper.FromJson<BuffTemplate>(Resources.Load<TextAsset>("Data/Mock/mockBuff").text);
        }

        [Test]
        public void TestHPPotionShouldRecoverHP(){
            EntityPlayer user = character.toEntity();
            user.currhp = 1;
            
            ItemHPPotion item = itemTemplates[0].toItem() as ItemHPPotion;
            List<BattleMessage> msgs = item.use(user, new Entity[]{user});
            Assert.AreEqual(1,msgs.Count);
            Assert.AreEqual(BattleMessage.Type.Heal,msgs[0].type);
            Assert.Greater(user.currhp, 1);
        }

        [Test]
        public void TestMPPotionShouldRecoverMP(){
            EntityPlayer user = character.toEntity();
            user.currmp = 1;
            
            ItemMPPotion item = itemTemplates[5].toItem() as ItemMPPotion;
            List<BattleMessage> msgs = item.use(user, new Entity[]{user});
            Assert.AreEqual(1,msgs.Count);
            Assert.AreEqual(BattleMessage.Type.MPHeal,msgs[0].type);
            Assert.Greater(user.currmp, 1);
        }

        [Test]
        public void TestBuffPotionShouldAddBuff(){
            EntityPlayer user = character.toEntity();
            
            ItemBuffPotion item = itemTemplates[10].toItem() as ItemBuffPotion;
            List<BattleMessage> msgs = item.use(user, new Entity[]{user});
            Assert.AreEqual(2,msgs.Count);
            Assert.AreEqual(BattleMessage.Type.Buff,msgs[0].type);
            Assert.AreEqual(2,user.buffState.Count);
            Assert.AreEqual("ATK buff",user.buffState.getBuff(0).name);
            Assert.AreEqual(10,user.buffState.getBuff(0).rounds);
        }



        
    }
}