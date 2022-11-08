using UnityEngine;

namespace RPG
{
    public abstract class Equipment : Item
    {
        public Constant.EquipmentType equipmentType { get; set; }
        public int reqLv { get; set; }
        public int basePower { get; set; }
        public int baseMagicPower { get; set; }
        public int power { get { return basePower; } }
        public int magicPower { get { return baseMagicPower; } }

        public string fullName { get { return name; } }

        public Equipment(Sprite img) : base(img)
        {
        }

        public override int getMaxStack()
        {
            return 1;
        }

        public abstract BasicStat getBasicStat();

        public bool matchJobRestriction(Job job)
        {
            if (equipmentType == Constant.EquipmentType.Accessory)
                return true;
            if (job.name.Equals("Adventurer"))
                return true;
            else if (job.name.Equals("Berserker") || job.name.Equals("Knight"))
            {
                if (equipmentType == Constant.EquipmentType.Sword || equipmentType == Constant.EquipmentType.Axe || equipmentType == Constant.EquipmentType.Shield || equipmentType == Constant.EquipmentType.HeavyArmor)
                    return true;
            }
            else if (job.name.Equals("Mage") || job.name.Equals("Necromancer") || job.name.Equals("Priest"))
            {
                if (equipmentType == Constant.EquipmentType.Wand || equipmentType == Constant.EquipmentType.Staff || equipmentType == Constant.EquipmentType.MagicBook || equipmentType == Constant.EquipmentType.RobeArmor)
                    return true;
            }
            else if (job.name.Equals("Archer") || job.name.Equals("Assassin"))
            {
                if (equipmentType == Constant.EquipmentType.Bow || equipmentType == Constant.EquipmentType.Dagger)
                    return true;
            }
            return false;
        }

        public override string onSave()
        {
            return "E|" + id + "|" + quality;
        }

    }
}