using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class Constant
    {
        public const int MapModeProgressive = 0;
        public const int MapModeExplore = 1;
        public const string topBarSelectAnEnemy = "Select an enemy";
        public const string topBarSelectAnItem = "Select an item to use";
        public const string topBarSelectASkill = "Select a skill to use";
        public const string topBarSelectPlayer = "Select a player";
        public enum buyPlace
        {
            shop, none, blacksmith
        }
        public static Color32[] itemRarityColor = {
            new Color32(0,0,0,255),
            new Color32(72,209,55,255),
            new Color32(0,168,255,255),
            new Color32(142,68,173,255),
            new Color32(230,126,34,255)
        };

        public static string[] equipmentRarityPrefix = {
            "",
            "Good",
            "Elite",
            "Legendary",
            "Heroic"
        };

        public static float[] equipmentRarityPowerModifier = {
            1.0f,
            1.2f,
            1.5f,
            2f,
            3f
        };

        public const string attackSkill = "Attack";
        public const string attackSkillAOE = "AttackAOE";
        public const string defenseSkill = "Defense";
        public const string magicSkill = "Magic";
        public const string magicSkillAOE = "MagicAOE";
        public const string healSkill = "Heal";
        public const string healSkillAOE = "HealAOE";
        public const string buffSkill = "Buff";
        public const string buffSkillAOE = "BuffAOE";
        public const string deuffSkill = "Debuff";
        public const string debuffSkillAOE = "DebuffAOE";
        public const string SpecialSkill = "Special";
        public const string PassiveSkill = "Passive";
        public const string buffSelfSkill = "BuffSelf";

        public enum WeaponHand
        {
            SingleHand, DoubleHand, Shield, Armor
        }

        public enum EquipmentType
        {
            Sword, Axe, Shield, Wand, Staff, MagicBook, Bow, Dagger, HeavyArmor, LightArmor, RobeArmor, Accessory
        }

        public static int[] memberUnlockAt = { 0, 0, 8, 17, 24, 32, 45 };
        public static int[] mapUnlockAt = { 0, 6, 15, 23, 30, 35, 42 };

        public static readonly string[] craftSkillTypes = {
            "Mining","Gathering","Hunting","Smithing","Arcane Crafting","Jewel Crafting","Reinforcing","Enchanting","Brewing"
        };
        public const int craftSkillMiningId = 0;
        public const int craftSkillForgingId = 1;
        public const int craftSkillHuntingId = 2;
        public const int craftSkillSmithingId = 3;
        public const int craftSkillReinforcingId = 4;
        public const int craftSkillArcaneCraftingId = 5;
        public const int craftSkillBrewingId = 6;
        public const int craftSkillEnchantingId = 7;
        public const int craftSkillCraftingId = 8;
        public static readonly string[] supportCharacterJobs = {
            "Gatherer","Crafter","Enchanter"
        };
        public const int jobGathererId = 0;
        public const int jobCrafterId = 1;
        public const int jobEnchanterId = 2;
        public static readonly string[] supportCharacterFacesMale = {

        };

        public static readonly string[] supportCharacterBodiesMale = {
            "Body_Adventurer","Body_Berserker","Body_Knight"
        };

        public static readonly string[] supportCharacterFacesFemale = {

        };

        public static readonly string[] supportCharacterBodiesFemale = {
            "Body_Adventurer","Body_Berserker","Body_Knight"
        };

        public static readonly string[] supportCharacterNamesMale = {
            "Thomas","Harry","Dickson","Andrew","Jack"
        };

        public static readonly string[] supportCharacterNamesFemale = {
            "Kelly","Jane","Angel","Sue","Mary"
        };
    }
}