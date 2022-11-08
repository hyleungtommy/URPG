using System;
using UnityEngine;

namespace RPG
{
    public class ItemResources : Item
    {
        public ItemResources(Sprite img)
            : base(img)
        {
        }

        public override int getMaxStack()
        {
            return 99;
        }

        public override string getTypeName()
        {
            return "Resources";
        }
    }
}

