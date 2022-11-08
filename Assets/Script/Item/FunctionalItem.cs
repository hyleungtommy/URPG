using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public abstract class FunctionalItem : Item, IFunctionable
    {
        public FunctionalItem(Sprite img)
            : base(img)
        {
        }

        public abstract List<BattleMessage> use(Entity user, Entity[] target);
        public abstract bool isUseOnPlayerParty();
    }
}