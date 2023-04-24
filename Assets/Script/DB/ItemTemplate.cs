using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class ItemTemplate
    {
        public int id;
        public string name;
        public string img;
        public string desc;
        public string type;
        public string buyPlace;
        public int rarity;
        public int price;
        public int sellPrice;
        public float healPercentage;
        public int minHealAmount;
        public ApplyBuffTemplate[] buff;
        public CraftRecipeTemplate craftRecipe;

        public override string ToString()
        {
            return "name=" + name;
        }

        public Item toItem()
        {
            Item item = createItem();
            if (item != null)
            {
                item.id = id;
                item.name = name;
                item.desc = desc;
                item.buyPlace = GetBuyPlace(buyPlace);
                item.rarity = rarity;
                item.price = price;
                item.sellPrice = price;
                item.rarity = rarity;
                if (item is ItemHPPotion)
                {
                    (item as ItemHPPotion).healPercentage = healPercentage;
                    (item as ItemHPPotion).minHealAmount = minHealAmount;
                }
                if (item is ItemMPPotion)
                {
                    (item as ItemMPPotion).healPercentage = healPercentage;
                    (item as ItemMPPotion).minHealAmount = minHealAmount;
                }
                if (item is ItemBuffPotion)
                {
                    (item as ItemBuffPotion).buffEffects = GetBuffs();
                }
            }
            return item;
        }

        private Item createItem()
        {
            Sprite sprite = Resources.Load<Sprite>("Item/" + img);
            if (type.Equals("HP Potion"))
            {
                return new ItemHPPotion(sprite);
            }
            else if (type.Equals("MP Potion"))
            {
                return new ItemMPPotion(sprite);
            }
            else if (type.Equals("Resources") || type.Equals("Special"))
            {
                return new ItemResources(sprite);
            }
            else if (type.Equals("Buff Potion"))
            {
                return new ItemBuffPotion(sprite);
            }
            return null;
        }

        private Constant.buyPlace GetBuyPlace(String buyPlace)
        {
            if (buyPlace != null && buyPlace.Equals("Shop"))
            {
                return Constant.buyPlace.shop;
            }
            else if (buyPlace != null && buyPlace.Equals("None"))
            {
                return Constant.buyPlace.none;
            }
            else if (buyPlace != null && buyPlace.Equals("Blacksmith"))
            {
                return Constant.buyPlace.blacksmith;
            }
            return Constant.buyPlace.none;
        }

        private List<Buff> GetBuffs()
        {
            List<Buff> b = new List<Buff>();
            //Debug.Log("buff" + buff);
            foreach (ApplyBuffTemplate bu in buff)
            {
                //Debug.Log("getbuffs=" + bu.toBuff().ToString());
                b.Add(bu.toBuff());
            }
            return b;
        }

    }
}