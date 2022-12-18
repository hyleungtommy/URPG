using UnityEngine;
using System.Collections;
namespace RPG
{
    public class EquipmentManager
    {
        public Weapon weaponEquipped;
        public Armor armorEquipped;
        public Shield shieldEquipped;
        public Accessory accessoryEquipped;
        public EquipmentManager()
        {
            //weaponEquipped = EquipmentDB.get (0).create(2) as Weapon;
            //weaponEquipped.ReinLv = 5;
            //armorEquipped = EquipmentDB.get (24).create(0) as Armor;
            //shieldEquipped = EquipmentDB.get (3).create (0) as Shield;
        }

        public BasicStat getEquipmentStat()
        {

            BasicStat equipStat = new BasicStat(0, 0, 0, 0, 0, 0, 0, 0);
            if (weaponEquipped != null){
                equipStat = equipStat.plus(weaponEquipped.getBasicStat());
            }
                
            if (shieldEquipped != null){
                equipStat = equipStat.plus(shieldEquipped.getBasicStat());
            }
                
            if (armorEquipped != null){
                equipStat = equipStat.plus(armorEquipped.getBasicStat());
            }
                
            return equipStat;
        }

        public BasicStat getEquipmentEnchantmentStat(){
            BasicStat equipStat = new BasicStat(1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
            if (weaponEquipped != null){
                equipStat = equipStat.plus(weaponEquipped.getEnchantmentMatrix());
            }
                
            if (shieldEquipped != null){
                equipStat = equipStat.plus(shieldEquipped.getEnchantmentMatrix());
            }
                
            if (armorEquipped != null){
                equipStat = equipStat.plus(armorEquipped.getEnchantmentMatrix());
            }

            if(accessoryEquipped != null){
                equipStat = equipStat.plus(accessoryEquipped.getEnchantmentMatrix());
            }
                
            return equipStat;
        }

        public void equip(StorageSlot slot, Equipment equipment)
        {
            if (equipment is Weapon)
            {
                if ((equipment as Weapon).getWeaponHand() == Constant.WeaponHand.SingleHand)
                {
                    slot.remove(1);
                    removeWeapon();
                    weaponEquipped = equipment as Weapon;
                }
                else if ((equipment as Weapon).getWeaponHand() == Constant.WeaponHand.DoubleHand)
                {
                    slot.remove(1);
                    removeWeapon();
                    removeShield();
                    weaponEquipped = equipment as Weapon;
                }
            }
            else if (equipment is Shield)
            {
                slot.remove(1);
                removeShield();
                if (weaponEquipped != null && weaponEquipped.getWeaponHand() == Constant.WeaponHand.DoubleHand)
                    removeWeapon();
                shieldEquipped = equipment as Shield;
            }
            else if (equipment is Armor)
            {
                slot.remove(1);
                removeArmor();
                armorEquipped = equipment as Armor;
            }
            else if (equipment is Accessory)
            {
                slot.remove(1);
                removeAccessory();
                accessoryEquipped = equipment as Accessory;
            }
        }

        public void unequip(int id)
        {
            if (id == 0)
                removeWeapon();
            else if (id == 1)
                removeShield();
            else if (id == 2)
                removeArmor();
            else if (id == 3)
                removeAccessory();
        }

        public void removeWeapon()
        {
            if (weaponEquipped != null)
            {
                Game.inventory.smartInsert(weaponEquipped, 1);
                weaponEquipped = null;
            }
        }

        public void removeShield()
        {
            if (shieldEquipped != null)
            {
                Game.inventory.smartInsert(shieldEquipped, 1);
                shieldEquipped = null;
            }
        }

        public void removeArmor()
        {
            if (armorEquipped != null)
            {
                Game.inventory.smartInsert(armorEquipped, 1);
                armorEquipped = null;
            }
        }

        public void removeAccessory(){
            if(accessoryEquipped != null){
                Game.inventory.smartInsert(accessoryEquipped,1);
                accessoryEquipped = null;
            }
        }

        public string onSave()
        {
            string saveStr = "";
            if (weaponEquipped != null)
                saveStr += weaponEquipped.onSave();
            saveStr += ";";
            if (shieldEquipped != null)
                saveStr += shieldEquipped.onSave();
            saveStr += ";";
            if (armorEquipped != null)
                saveStr += armorEquipped.onSave();
            //Debug.Log("Equip Hot Bar :" + saveStr);
            return saveStr;
        }

        public void onLoad(string saveStr)
        {
            string[] saveStrArray = saveStr.Split(';');
            if (saveStrArray.Length == 3)
            {
                Equipment weapon = DB.createEquipmentFormSaveStr(saveStrArray[0]);
                Equipment shield = DB.createEquipmentFormSaveStr(saveStrArray[1]);
                Equipment armor = DB.createEquipmentFormSaveStr(saveStrArray[2]);
                if(weapon != null)
                    weaponEquipped = weapon as Weapon;
                if(shield != null)
                    shieldEquipped = shield as Shield;
                if(armor != null)
                    armorEquipped = armor as Armor;
            }
        }

    }
}

