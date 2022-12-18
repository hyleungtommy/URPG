using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG
{
    public abstract class Equipment : Item
    {
        public Constant.EquipmentType equipmentType { get; set; }
        public int reqLv { get; set; }
        public int basePower { get; set; }
        public int baseMagicPower { get; set; }
        public int power { get { return basePower + getReinforcePower(); } }
        public int magicPower { get { return baseMagicPower + getReinforceMagicPower(); } }

        public string fullName { get { return name + getReinforceLevelString(); } }
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
            return "E|" + id + "|" + quality + "|" + reinforceRecipe.onSave() + "|" + enchantment.onSave();
        }
        

        public TaskCompleteMsg reinforce(){
            
            if(reinforceRecipe.reinforceLv < reinforceRecipe.maxReinforceLv){
                reinforceRecipe.reinforceLv ++;
            }
            List<Item>items = new List<Item>();
            items.Add(this);
            List<int>qty = new List<int>();
            qty.Add(1);
            return new TaskCompleteMsg(items,qty);
        }

        public TaskCompleteMsg enchant(){
            if(enchantment != null){
                int rndLevel = Random.Range(1,5);
                int rndEnchantmentId = Random.Range(0,DB.enchantmentEffects.Length);
                EnchantmentEffect effect = DB.enchantmentEffects[rndEnchantmentId].toEnchantmentEffect(rndLevel);
                enchantment.effects.Add(effect);
            }
            List<Item>items = new List<Item>();
            items.Add(this);
            List<int>qty = new List<int>();
            qty.Add(1);
            return new TaskCompleteMsg(items,qty);
        }

        public bool canReinforce(){
            return (reinforceRecipe.reinforceLv <  reinforceRecipe.maxReinforceLv);
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
        
        public BasicStat getEnchantmentMatrix(){
            if(enchantment != null && enchantment.effects.Count > 0){
                return enchantment.getEnchantmentMatrix();
            }
            return new BasicStat(0f,0f,0f,0f,0f,0f,0f,0f);
        }

    }
}