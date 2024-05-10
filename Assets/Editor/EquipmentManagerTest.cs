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
    public class EquipmentManagerTest{
        EquipmentTemplate[] equipmentTemplates;
        Equipment shield;
        Equipment singleHandWeapon;
        Equipment doubleHandWeapon;
        Equipment armor;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            equipmentTemplates = JsonHelper.FromJson<EquipmentTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEquipment").text);
            DB.buffs = JsonHelper.FromJson<BuffTemplate>(Resources.Load<TextAsset>("Data/Mock/mockBuff").text);
            DB.enchantRecipeTemplates = JsonHelper.FromJson<EnchantRecipeTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEnchantmentRecipe").text);

            singleHandWeapon = equipmentTemplates[0].toGeneralEquipment().toEquipment(0);
            shield = equipmentTemplates[1].toGeneralEquipment().toEquipment(0);
            doubleHandWeapon = equipmentTemplates[2].toGeneralEquipment().toEquipment(0);
            armor = equipmentTemplates[3].toGeneralEquipment().toEquipment(0);
        }

        [SetUp]
        public void SetUp(){
            Game.inventory = new StorageSystem(10);
            Game.inventory.smartInsert(shield,1);
            Game.inventory.smartInsert(singleHandWeapon,1);
            Game.inventory.smartInsert(doubleHandWeapon,1);
            Game.inventory.smartInsert(armor,1);
        }

        [Test]
        public void ShouldEquipSingleHandWeaponAndShield(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(0), shield);
            equipmentManager.equip(Game.inventory.getSlot(1), singleHandWeapon);
            Assert.NotNull(equipmentManager.weaponEquipped);
            Assert.NotNull(equipmentManager.shieldEquipped);
            Assert.AreEqual("Wood Sword", equipmentManager.weaponEquipped.name);
            Assert.AreEqual("Rotten Wood Shield", equipmentManager.shieldEquipped.name);
        }

        [Test]
        public void ShouldReplaceSingleHandWeaponAndShield(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(0), shield);
            equipmentManager.equip(Game.inventory.getSlot(1), singleHandWeapon);
            equipmentManager.equip(Game.inventory.getSlot(2), doubleHandWeapon);
            Assert.NotNull(equipmentManager.weaponEquipped);
            Assert.Null(equipmentManager.shieldEquipped);
            Assert.AreEqual("Lumber Axe", equipmentManager.weaponEquipped.name);
        }

        [Test]
        public void ShouldEquipArmor(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(3), armor);
            Assert.NotNull(equipmentManager.armorEquipped);
            Assert.AreEqual("Simple Armor", equipmentManager.armorEquipped.name);
        }

        [Test]
        public void ShouldUnEquipWeaponAndShield(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(0), shield);
            equipmentManager.equip(Game.inventory.getSlot(1), singleHandWeapon);
            equipmentManager.equip(Game.inventory.getSlot(3), armor);
            equipmentManager.unequip(0);
            equipmentManager.unequip(1);
            Assert.Null(equipmentManager.weaponEquipped);
            Assert.Null(equipmentManager.shieldEquipped);
            Assert.NotNull(equipmentManager.armorEquipped);
        }

        [Test]
        public void ShouldGetWeaponEquipmentStat(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(1), singleHandWeapon);
            BasicStat stat = equipmentManager.getEquipmentStat();
            Assert.AreEqual(25, stat.ATK);
            Assert.AreEqual(10, stat.MATK);
        }

        [Test]
        public void ShouldGetShieldEquipmentStat(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(0), shield);
            BasicStat stat = equipmentManager.getEquipmentStat();
            Assert.AreEqual(10, stat.DEF);
            Assert.AreEqual(10, stat.MDEF);
        }

        [Test]
        public void ShouldGetArmorEquipmentStat(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(3), armor);
            BasicStat stat = equipmentManager.getEquipmentStat();
            Assert.AreEqual(20, stat.DEF);
            Assert.AreEqual(10, stat.MDEF);
        }

        [Test]
        public void ShouldGetCombinedEquipmentStat(){
            EquipmentManager equipmentManager = new EquipmentManager();
            equipmentManager.equip(Game.inventory.getSlot(0), shield);
            equipmentManager.equip(Game.inventory.getSlot(1), singleHandWeapon);
            equipmentManager.equip(Game.inventory.getSlot(3), armor);
            BasicStat stat = equipmentManager.getEquipmentStat();
            Assert.AreEqual(25, stat.ATK);
            Assert.AreEqual(10, stat.MATK);
            Assert.AreEqual(30, stat.DEF);
            Assert.AreEqual(20, stat.MDEF);
        }
    }
}