using UnityEngine;
using System.Collections;

namespace RPG
{
    public class Accessory:Equipment{
        public Accessory(Sprite img):base(img){

        }

        public override BasicStat getBasicStat()
        {
            return new BasicStat(0, 0, 0, 0, 0, 0, 0, 0);
        }

        public override string getTypeName()
        {
            return "Accessory";
        }
    }
}