using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class ItemUPPT: ItemSpecial{
        public ItemUPPT(Sprite img)
        :base(img){

        }

        public override string getTypeName()
        {
            return "Upgrade Point Scrolls";
        }

        public override void OnUse()
        {
            int addUPPTAmount = 0;
            if(id == 101){
                addUPPTAmount = 1;
            }else if(id == 102){
                addUPPTAmount = 5;
            }else if (id == 103){
                addUPPTAmount = 10;
            }
            Game.party.addUPPTForParty(addUPPTAmount);
        }
    }
}