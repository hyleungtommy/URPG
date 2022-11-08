using UnityEngine;

namespace RPG
{
    public class Weapon : Equipment
    {

        public Weapon(Sprite img) : base(img)
        {
        }

        public override string getTypeName()
        {
            return "Weapon";
        }

        public override BasicStat getBasicStat()
        {
            return new BasicStat(0, 0, power, 0, magicPower, 0, 0, 0);
        }

        public Constant.WeaponHand getWeaponHand()
        {
            if (equipmentType == Constant.EquipmentType.Sword || equipmentType == Constant.EquipmentType.Wand)
                return Constant.WeaponHand.SingleHand;
            else if (equipmentType == Constant.EquipmentType.Shield || equipmentType == Constant.EquipmentType.MagicBook)
                return Constant.WeaponHand.Shield;
            else
                return Constant.WeaponHand.DoubleHand;

        }


    }
}