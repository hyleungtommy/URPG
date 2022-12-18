using UnityEngine;

namespace RPG
{
    public class GeneralEquipment : Displayable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string type { get; set; }
        public int reqLv { get; set; }
        public int power { get; set; }
        public int magicPower { get; set; }
        public int price { get; set; }
        public string buyPlace { get; set; }
        public ReinforceRecipeTemplate reinforceRecipeTemplate {get; set;}

        public GeneralEquipment(Sprite img) : base(img)
        {
        }

        public Equipment toEquipment(int quality)
        {
            Equipment e = getEquipmentType();
            e.id = id;
            e.name = name;
            e.desc = desc;
            e.reqLv = reqLv;
            e.basePower = power;
            e.baseMagicPower = magicPower;
            e.price = price;
            if (buyPlace == "Blacksmith") e.buyPlace = Constant.buyPlace.blacksmith;
            else e.buyPlace = Constant.buyPlace.none;
            e.quality = quality;
            if(reinforceRecipeTemplate != null && reinforceRecipeTemplate.requireItem != null){
                e.reinforceRecipe = new ReinforceRecipe(e,
                reinforceRecipeTemplate.requireItem,
                reinforceRecipeTemplate.requireQtyStart,
                reinforceRecipeTemplate.requireMoneyStart,
                reinforceRecipeTemplate.requireLevel,
                reinforceRecipeTemplate.requireQtyIncrement,
                reinforceRecipeTemplate.requireMoneyIncrement,
                reinforceRecipeTemplate.maxReinforceLv,
                reinforceRecipeTemplate.powerIncrementPerLevel,
                reinforceRecipeTemplate.magicPowerIncrementPerLevel);
            }

            //create enchantment data
            EnchantmentData ed = null;
            foreach(EnchantRecipeTemplate t in DB.enchantRecipeTemplates){
                if(t.equipmentLv == reqLv){
                    ed = t.toEnchantmentData();
                }
            }

            e.enchantment = ed;
            
            return e;
        }

        private Equipment getEquipmentType()
        {
            Equipment e = null;
            if (type == "Sword" || type == "Axe" || type == "Staff" || type == "Wand" || type == "Bow" || type == "Dagger")
            {
                e = new Weapon(img);
                if (type == "Sword") e.equipmentType = Constant.EquipmentType.Sword;
                if (type == "Axe") e.equipmentType = Constant.EquipmentType.Axe;
                if (type == "Staff") e.equipmentType = Constant.EquipmentType.Staff;
                if (type == "Wand") e.equipmentType = Constant.EquipmentType.Wand;
                if (type == "Bow") e.equipmentType = Constant.EquipmentType.Bow;
                if (type == "Dagger") e.equipmentType = Constant.EquipmentType.Dagger;
            }
            else if (type == "Shield" || type == "Spellbook")
            {
                e = new Shield(img);
                if (type == "Shield") e.equipmentType = Constant.EquipmentType.Shield;
                if (type == "Spellbook") e.equipmentType = Constant.EquipmentType.MagicBook;
            }
            else if (type == "Heavy Armor" || type == "Light Armor" || type == "Robe Armor")
            {
                e = new Armor(img);
                if (type == "Heavy Armor") e.equipmentType = Constant.EquipmentType.HeavyArmor;
                if (type == "Light Armor") e.equipmentType = Constant.EquipmentType.LightArmor;
                if (type == "Robe Armor") e.equipmentType = Constant.EquipmentType.RobeArmor;
            }
            return e;
        }

        public override string ToString()
        {
            return "name=" + name;
        }

    }
}