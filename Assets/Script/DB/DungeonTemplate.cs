using System;
namespace RPG{
    [Serializable]
    public class DungeonTemplate{
        public int id;
        public string name;
        public string bg;
        public int minFloor;
        public int maxFloor;
        public int[] enemyList;
        public int[] appearChance;
        public int boss;
        public int mimic;
        public int[] availableRoomType;
        public int equipmentDropLv;
        public int[] lootItemId;
        public int[] lootMaxAmount;
        public int []lootMinAmount;
    }
}