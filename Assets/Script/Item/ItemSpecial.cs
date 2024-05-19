using System;
using UnityEngine;

namespace RPG
{
    public abstract class ItemSpecial : Item
    {
        public ItemSpecial(Sprite img)
            : base(img)
        {
        }

        public override int getMaxStack()
        {
            return 1;
        }

        public override string getTypeName()
        {
            return "Specials";
        }

        public abstract void OnUse();
    }
}
