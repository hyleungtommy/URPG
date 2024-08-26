using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public abstract class Equipment : Item
    {
        public Constant.EquipmentType equipmentType { get; set; }
        public int reqLv { get; set; }
        public int basePower { get; set; }
        public int baseMagicPower { get; set; }
        public int power { get { return (int)(basePower * Constant.equipmentRarityPowerModifier[rarity] + getReinforcePower()); } }
        public int magicPower { get { return (int)(baseMagicPower * Constant.equipmentRarityPowerModifier[rarity] + getReinforceMagicPower()); } }

        public string fullName { get { return Constant.equipmentRarityPrefix[rarity] + " " + name + GetFirstEnchantmentName() + getReinforceLevelString(); } }
        public ReinforceRecipe reinforceRecipe {get; set;}
        public EnchantmentData enchantment {get; set;}

        public Equipment(Sprite img) : base(img)
        {
        }

        public override int getMaxStack()
        {
            return 1;
        }

        public abstract BasicStat getBasicStat();

        public bool matchJobRestriction(Job job)
        {
            if (equipmentType == Constant.EquipmentType.Accessory)
                return true;
            if (job.name.Equals("Adventurer"))
                return true;
            else if (job.name.Equals("Berserker") || job.name.Equals("Knight"))
            {
                if (equipmentType == Constant.EquipmentType.Sword || equipmentType == Constant.EquipmentType.Axe || equipmentType == Constant.EquipmentType.Shield || equipmentType == Constant.EquipmentType.HeavyArmor)
                    return true;
            }
            else if (job.name.Equals("Mage") || job.name.Equals("Necromancer") || job.name.Equals("Priest"))
            {
                if (equipmentType == Constant.EquipmentType.Wand || equipmentType == Constant.EquipmentType.Staff || equipmentType == Constant.EquipmentType.MagicBook || equipmentType == Constant.EquipmentType.RobeArmor)
                    return true;
            }
            else if (job.name.Equals("Archer") || job.name.Equals("Assassin"))
            {
                if (equipmentType == Constant.EquipmentType.Bow || equipmentType == Constant.EquipmentType.Dagger)
                    return true;
            }
            return false;
        }

        public override string onSave()
        {
            return "E|" + id + "|" + rarity + "|" + (reinforceRecipe == null ? "0" : reinforceRecipe.onSave()) + "|" + (enchantment == null? "" : enchantment.onSave());
        }
        

        public TaskCompleteMsg reinforce(){
            Util.RemoveCraftItem(reinforceRecipe.requirements, reinforceRecipe.requireMoney, 1);
            if(reinforceRecipe.reinforceLv < reinforceRecipe.maxReinforceLv){
                reinforceRecipe.reinforceLv ++;
            }
            Game.craftSkillManager.addExperience(SkillCraft.Type.reinforcing, reqLv/10);
            List<Item>items = new List<Item>();
            items.Add(this);
            List<int>qty = new List<int>();
            qty.Add(1);
            return new TaskCompleteMsg(items,qty);
        }
        //Give equipment a random enchantment with random level
        public TaskCompleteMsg enchant(){
            if(enchantment != null){
                Util.RemoveCraftItem(enchantment.requirements, enchantment.requireMoney, 1);
                int rndLevel = Random.Range(1,5);
                EnchantEffectTemplate[] availableEnchantEffects = FilterEnchantEffect();
                int rndEnchantmentId = Random.Range(0,availableEnchantEffects.Length);
                EnchantmentEffect effect = availableEnchantEffects[rndEnchantmentId].toEnchantmentEffect(rndLevel);
                enchantment.effects.Add(effect);
                Game.craftSkillManager.addExperience(SkillCraft.Type.enchanting, reqLv/10);
            }
            List<Item>items = new List<Item>();
            items.Add(this);
            List<int>qty = new List<int>();
            qty.Add(1);
            return new TaskCompleteMsg(items,qty);
        }

        public bool canReinforce(){
            return reinforceRecipe.reinforceLv <  reinforceRecipe.maxReinforceLv;
        }

        public bool canEnchant(){
            //Debug.Log("enchant=" + enchantment);
            return enchantment != null && enchantment.effects.Count == 0;
        }

        private string getReinforceLevelString(){
            if(reinforceRecipe != null && reinforceRecipe.reinforceLv > 0){
                return " +" + reinforceRecipe.reinforceLv;
            }
            return "";
        }

        private int getReinforcePower(){
            if(reinforceRecipe != null && reinforceRecipe.reinforceLv > 0){
                return reinforceRecipe.reinforceLv * reinforceRecipe.powerIncrementPerLevel;
            }
            return 0;
        }

        private int getReinforceMagicPower(){
            if(reinforceRecipe != null && reinforceRecipe.reinforceLv > 0){
                return reinforceRecipe.reinforceLv * reinforceRecipe.magicPowerIncrementPerLevel;
            }
            return 0;
        }

        private string GetFirstEnchantmentName(){
            if(enchantment != null && enchantment.effects.Count > 0){
                return " of " + enchantment.effects[0].name;
            }
            return "";
        }

        private EnchantEffectTemplate[] FilterEnchantEffect(){
            int equipmentSlot = 0;
            if(this is Shield){
                equipmentSlot = 1;
            }else if(this is Armor){
                equipmentSlot = 2;
            }else if(this is Accessory){
                equipmentSlot = 3;
            }
            return DB.enchantmentEffects.Where(e => e.equipTypeWhiteList[equipmentSlot] == true).ToArray();
        }
        
        public BasicStat getEnchantmentMatrix(){
            if(enchantment != null && enchantment.effects.Count > 0){
                return enchantment.getEnchantmentMatrix();
            }
            return new BasicStat(0f,0f,0f,0f,0f,0f,0f,0f);
        }

        public ElementalTemplate GetElementalDamageMatrix(){
            if(enchantment != null && enchantment.effects.Count > 0){
                return enchantment.GetElementalDamageMatrix();
            }
            return new ElementalTemplate();
        }

        public ElementalTemplate GetElementalResistanceMatrix(){
            if(enchantment != null && enchantment.effects.Count > 0){
                return enchantment.GetElementalResistanceMatrix();
            }
            return new ElementalTemplate();
        }

    }
}