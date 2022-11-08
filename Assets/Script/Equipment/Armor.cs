using UnityEngine;
using System.Collections;

namespace RPG
{
    public class Armor : Equipment
    {
        public Armor(Sprite img)
            : base(img)
        {
        }

        public override BasicStat getBasicStat()
        {
            return new BasicStat(0, 0, 0, power, 0, magicPower, 0, 0);
        }

        public override string getTypeName()
        {
            return "Armor";
        }

    }
}
