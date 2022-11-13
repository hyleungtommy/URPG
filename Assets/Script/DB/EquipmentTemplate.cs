using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class EquipmentTemplate
    {
        public int id;
        public string name;
        public string desc;
        public string type;
        public string img;
        public int reqLv;
        public int power;
        public int magicPower;
        public int price;
        public string buyPlace;
        public CraftRecipeTemplate craftRecipe;
        public ReinforceRecipeTemplate reinforceRecipe; 

        public override string ToString()
        {
            return "name=" + name;
        }

        public GeneralEquipment toGeneralEquipment()
        {
            Sprite img = Resources.Load<Sprite>("Equipment/" + this.img);
            GeneralEquipment e = new GeneralEquipment(img);
            e.id = id;
            e.name = name;
            e.desc = desc;
            e.type = type;
            e.reqLv = reqLv;
            e.power = power;
            e.magicPower = magicPower;
            e.price = price;
            e.buyPlace = buyPlace;
            
            return e;

        }

    }
}