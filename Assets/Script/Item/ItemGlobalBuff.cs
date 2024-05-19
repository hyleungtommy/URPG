using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace RPG
{
    public class ItemGlobalBuff:ItemSpecial{
        ApplyGlobalBuffTemplate applyGlobalBuffTemplate;
        public ItemGlobalBuff(Sprite img, ApplyGlobalBuffTemplate applyGlobalBuffTemplate):base(img){
            this.applyGlobalBuffTemplate = applyGlobalBuffTemplate;
        }

        public override string getTypeName()
        {
            return "Global Buff Item";
        }


        public override void OnUse()
        {
            GlobalBuffTemplate globalBuffTemplate = DB.globalBuffTemplates[applyGlobalBuffTemplate.id - 1];
            GlobalBuff globalBuff = globalBuffTemplate.ToGlobalBuff(base.img, applyGlobalBuffTemplate.rounds);
            Game.globalBuffManager.addBuff(globalBuff);
        }
    }
}