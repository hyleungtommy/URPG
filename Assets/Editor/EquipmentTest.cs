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

    public class EquipmentTest{
        EquipmentTemplate[] equipmentTemplates;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            equipmentTemplates = JsonHelper.FromJson<EquipmentTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEquipment").text);
            DB.buffs = JsonHelper.FromJson<BuffTemplate>(Resources.Load<TextAsset>("Data/Mock/mockBuff").text);
            DB.enchantRecipeTemplates = JsonHelper.FromJson<EnchantRecipeTemplate>(Resources.Load<TextAsset>("Data/Mock/mockEnchantmentRecipe").text);
        }

        [Test]
        public void ShouldCreateGeneralEquipmentFromTemplate(){
            EquipmentTemplate equipmentTemplate = equipmentTemplates[0];
            GeneralEquipment equipment = equipmentTemplate.toGeneralEquipment();
            Assert.AreEqual("Wood Sword", equipment.name);
        }

        [Test]
        public void ShouldCreateEquipmentFromGeneralEquipment(){
            EquipmentTemplate equipmentTemplate = equipmentTemplates[0];
            GeneralEquipment generalEquipment = equipmentTemplate.toGeneralEquipment();
            Equipment equipment = generalEquipment.toEquipment(1);
            Assert.AreEqual("Wood Sword", equipment.name);
            Assert.AreEqual(1, equipment.rarity);
        }
        
    }
}