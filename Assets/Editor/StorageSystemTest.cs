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
    public class StorageSystemTest
    {
        StorageSystem storageSystem;
        ItemTemplate[] itemTemplates;
        GeneralEquipment[]equipmentList;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            itemTemplates = JsonHelper.FromJson<ItemTemplate>(Resources.Load<TextAsset>("Data/Mock/mockItem").text);
            EquipmentTemplate[] equipmentTemplates = JsonHelper.FromJson<EquipmentTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEquipment").text);
            EnchantRecipeTemplate[] enchantRecipeTemplates = JsonHelper.FromJson<EnchantRecipeTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEnchantRecipe").text);
            equipmentList = equipmentTemplates.Select((equipment)=> equipment.toGeneralEquipment()).ToArray();
            DB.enchantRecipeTemplates = enchantRecipeTemplates;
        }

        [SetUp]
        public void SetUp(){
            storageSystem = new StorageSystem(50);
            storageSystem.smartInsert(itemTemplates[0].toItem(),10);// Insert 10 potion
            storageSystem.smartInsert(itemTemplates[26].toItem(),20);// Insert 20 crafting materials
            storageSystem.smartInsert(equipmentList[0].toEquipment(1),1);// Insert an equipment
        }

        [Test]
        public void ShouldOnlyGetEquipment()
        {
            StorageSlot[] slots = storageSystem.getOnlyEquipment();
            Assert.AreEqual(1,slots.Length);
            Assert.AreEqual(true, slots[0].getContainment() is Equipment);
        }

        [Test]
        public void ShouldInsertIntoStorageSystem()
        {
            int remaining = storageSystem.smartInsert(itemTemplates[0].toItem(),10);
            Assert.AreEqual(0,remaining);
        }

        [Test]
        public void ShouldHaveRemainingIfStorageSystemIsFull()
        {
            storageSystem = new StorageSystem(2);
            storageSystem.smartInsert(equipmentList[0].toEquipment(1),1);// Insert an equipment
            int remaining = storageSystem.smartInsert(itemTemplates[0].toItem(),15);
            Assert.AreEqual(5,remaining);
        }

        [Test]
        public void ShouldDeleteItemInStorage()
        {
            storageSystem.smartDelete(itemTemplates[0].toItem(),5);
            int total = storageSystem.searchTotalQtyOfItemInInventory(1);
            Assert.AreEqual(5,total);
        }

        [Test]
        public void ShouldGetAllSlotIfItemExist()
        {
            storageSystem.smartInsert(itemTemplates[0].toItem(),11);
            ArrayList allSlot = storageSystem.searchInInventory(itemTemplates[0].toItem());
            Assert.AreEqual(3,allSlot.Count);
        }

        [Test]
        public void ShouldNotFindSlotIfItemDoNotExist()
        {
            ArrayList allSlot = storageSystem.searchInInventory(itemTemplates[1].toItem());
            Assert.AreEqual(0,allSlot.Count);
        }

        [Test]
        public void ShouldCreateVirtualWithOnlyBattleItem()
        {
            storageSystem.smartInsert(itemTemplates[1].toItem(),11);
            List<ItemAndQty> list = storageSystem.CreateVirtualItemInv();
            Assert.AreEqual(2,list.Count);
            Assert.AreEqual(1,list[0].item.id);
            Assert.AreEqual(10,list[0].qty);
            Assert.AreEqual(2,list[1].item.id);
            Assert.AreEqual(11,list[1].qty);
        }

        [Test]
        public void ShouldGetAllSlotWithEquipment()
        {
            int total = storageSystem.searchTotalQtyOfEquipmentInInventory(1);
            Assert.AreEqual(1,total);
        }


    }
}
