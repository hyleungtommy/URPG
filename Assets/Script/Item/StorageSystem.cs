using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace RPG
{
    public class StorageSystem
    {
        private StorageSlot[] content;
        private int size;
        public StorageSystem(int size)
        {
            //Debug.Log("Inv size " + size);
            this.size = size;
            content = new StorageSlot[size];
            for (int i = 0; i < size; i++)
            {
                content[i] = new StorageSlot();
                content[i].setId(i);
            }
        }

        public StorageSlot getSlot(int index)
        {
            return content[index];
        }

        public int getSize()
        {
            return size;
        }

        public StorageSlot[] getOnlyBattleItem()
        {
            List<StorageSlot> slots = new List<StorageSlot>();
            foreach (StorageSlot s in content)
            {
                if (s.getContainment() is FunctionalItem)
                    slots.Add(s);
            }
            return slots.ToArray();
        }


        public StorageSlot[] getOnlyEquipment()
        {
            List<StorageSlot> slots = new List<StorageSlot>();
            foreach (StorageSlot s in content)
            {
                if (s.getContainment() is Equipment)
                    slots.Add(s);
            }
            return slots.ToArray();
        }


        // return remaining item numbers
        public int smartInsert(Item item, int qty)
        {
            Debug.Log("insert " + item.name + " qty " + qty);
            int remaining = qty;
            ArrayList sameItem = searchInInventory(item);

            if (sameItem.Count > 0)
            {

                foreach (StorageSlot e in sameItem)
                {
                    if (remaining > 0)
                    {
                        remaining = e.insert(item, remaining);
                        //Debug.Log("insert in" + e.getId());

                    }
                    else
                        break;
                }
            }
            StorageSlot nextEmptySlot;
            while (remaining > 0 && (nextEmptySlot = findFirstEmptySlot()) != null)
                remaining = nextEmptySlot.insert(item, remaining);
            if (remaining > 0)
                Debug.Log("inventory full, " + remaining + " can not insert " + item.name);
            return remaining;
        }

        /*
                public Item smartInsertCrafting(string itemCode, int qty, bool randomEquipQuality)
                {
                    Item item = null;
                    if (itemCode.StartsWith("E"))
                    {
                        int quality = 0;
                        if (randomEquipQuality)
                        {
                            quality = RPGUtil.getRandomIndexFrom(RPGParameter.CRAFT_EQUIP_QUAL_CHANCE, RPGUtil.calculateSum(RPGParameter.CRAFT_EQUIP_QUAL_CHANCE));

                        }
                        item = EquipmentDB.get(itemCode).create(quality);

                    }
                    else if (itemCode.StartsWith("I"))
                    {
                        item = ItemDB.get(itemCode).create();
                    }
                    smartInsert(item, qty);
                    return item;
                }
         */

        public void smartDelete(Item item, int qty)
        {
            int remaining = qty;
            ArrayList sameItem = searchInInventory(item);

            if (sameItem.Count > 0)
            {
                foreach (StorageSlot e in sameItem)
                    if (remaining > 0)
                    {
                        if (remaining > e.getQty())
                        {
                            e.remove(e.getQty());
                            remaining -= e.getQty();
                        }
                        else
                            e.remove(remaining);
                    }
                    else
                        break;
            }
        }

        /*
                public void smartDeleteCrafting(string itemCode, int qty)
                {
                    Item item = null;
                    if (itemCode.StartsWith("E"))
                    {
                        item = EquipmentDB.get(itemCode).create(0);
                    }
                    else if (itemCode.StartsWith("I"))
                    {
                        item = ItemDB.get(itemCode).create();
                    }
                    smartDelete(item, qty);
                }
         */


        // return first empty slot, return null if storage is full
        private StorageSlot findFirstEmptySlot()
        {
            foreach (StorageSlot slot in content)
            {
                if (slot.isEmpty())
                    return slot;
            }
            return null;
        }

        public ArrayList searchInInventory(Item item)
        {
            ArrayList sameItem = new ArrayList();
            foreach (StorageSlot e in content)
            {
                if (e.getContainment() != null && item != null && e.getContainment().id == item.id && (e.getContainment().name.Equals(item.name)))
                    sameItem.Add(e);
            }
            return sameItem;
        }
        /* 
                public int searchForTotalQtyInInventory(string itemCode)
                {
                    int qty = 0;
                    int id = Int32.Parse(itemCode.Substring(1)) - 1;
                    if (itemCode.StartsWith("E"))
                    {
                        foreach (StorageSlot e in content)
                        {
                            if (e.getContainment() != null && e.getContainment().ID == id && e.getContainment() is Equipment)
                            {
                                qty = e.getQty();
                                break;
                            }
                        }
                    }
                    else if (itemCode.StartsWith("I"))
                    {
                        foreach (StorageSlot e in content)
                        {
                            if (e.getContainment() != null && e.getContainment().ID == id)
                            {
                                qty = e.getQty();
                                break;
                            }
                        }
                    }

                    return qty;
                }
        */
        public void insert(Item storable, int qty, int index)
        {
            content[index].insert(storable, qty);
        }

        public void remove(int index, int qty)
        {
            content[index].remove(qty);
        }

        public void clear(int index)
        {
            content[index].remove(content[index].getQty());
        }

        public int transferTo(StorageSystem other, int index)
        {
            int remaining = other.smartInsert(content[index].getContainment(), content[index].getQty());
            if (remaining > 0)
                content[index].insert(content[index].getContainment(), remaining);
            else
                clear(index);
            return remaining;
        }



        public virtual string onSave()
        {
            string saveStr = "";
            foreach (StorageSlot slot in content)
                if (saveStr.Length > 0)
                    saveStr = string.Concat(saveStr, ";", slot.onSave());
                else
                    saveStr = string.Concat(saveStr, slot.onSave());
            return saveStr;
        }

        public virtual void onLoad(string save)
        {
            //Debug.Log ("inv save str : "  +save);
            if (save.Length > 0)
            {
                string[] saveStr = save.Split(';');
                int i = 0;
                foreach (StorageSlot slot in content)
                {
                    slot.onLoad(saveStr[i]);
                    i++;
                }
            }
            else
            {// save data is reset
                for (int i = 0; i < size; i++)
                {
                    content[i] = new StorageSlot();
                    content[i].setId(i);
                }
            }

        }

        public void onResetSave()
        {
            for (int i = 0; i < size; i++)
            {
                content[i] = new StorageSlot();
                content[i].setId(i);
            }
        }

        public void onUpgrade(int newsize)
        {
            this.size = newsize;
            StorageSlot[] newcontent = new StorageSlot[size];
            for (int i = 0; i < size; i++)
            {

                newcontent[i] = new StorageSlot();
                newcontent[i].setId(i);
                if (i < content.Length && content[i].getContainment() != null)
                    newcontent[i].insert(content[i].getContainment(), content[i].getQty());
            }
            content = newcontent;
        }

        public List<ItemAndQty> createVirtualItemInv()
        {
            List<ItemAndQty> list = new List<ItemAndQty>();
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] != null && content[i].getContainment() != null && content[i].getContainment() is FunctionalItem)
                {
                    bool itemInList = false;
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j].item.id == content[i].getContainment().id)
                        {
                            list[j].qty += content[i].getQty();
                            itemInList = true;
                            break;
                        }
                    }
                    if (!itemInList)
                    {
                        list.Add(new ItemAndQty(content[i].getContainment(), content[i].getQty()));
                    }
                }
            }
            return list.OrderBy(o => o.item.id).ToList(); ;
        }
    }
}

