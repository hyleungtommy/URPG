using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class Warehouse:Building
    {
        public int MaxCapacity {get{
            return Constant.WarehouseResourceCapacityStart + (Lv - 1) * Constant.WarehouseResourceCapacityIncrement;
        }}

        public StorageSystem ItemStorage {set; get;}

        public Warehouse(Sprite img):base(img){
            ItemStorage = new StorageSystem(Constant.WarehouseStorageSlotStart);
        }
    }
}