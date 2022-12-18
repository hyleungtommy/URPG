using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public abstract class Item : Displayable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public Constant.buyPlace buyPlace { get; set; }
        public int rarity { get; set; }
        public int price { get; set; }
        public int sellPrice { get; set; }


        public override string ToString()
        {
            return "name=" + name;
        }

        public Item(Sprite img) : base(img)
        {

        }

        public abstract int getMaxStack();

        public abstract String getTypeName();

        public virtual void onLoad(string save) { }

        public virtual string onSave()
        {
            return "I|" + id.ToString();
        }


    }
}