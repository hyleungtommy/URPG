using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using System;

namespace RPG
{
    public class Dungeon
    {
        private int[,] dungeonLayout;
        public int currentFloorNum { get; set; }
        public int currentRoomNum { get; set; }
        public string dungeonName { get; set; }
        int dungeonId { get; set; }
        int equipmentDropLv;
        int[] lootItemId;
        int[] lootMaxAmount;
        int[] lootMinAmount;
        int boss;
        int[] enemyList;
        int[] appearChance;
        int mimic;
        public Sprite battleImg {set; get;}

        public enum Action{
            None, EnterBattle,ShowLoot
        }

        public Room currentRoom
        {
            get
            {
                return new Room(dungeonLayout[currentFloorNum, currentRoomNum]);
            }
        }
        public Dungeon(int[,] dungeonLayout, int dungeonId)
        {
            DungeonTemplate dungeonTemplate = DB.QueryDungeon(dungeonId);
            this.dungeonId = dungeonId;
            this.dungeonName = dungeonTemplate.name;
            this.dungeonLayout = dungeonLayout;
            currentFloorNum = 0;
            currentRoomNum = 0;
            this.boss = dungeonTemplate.boss;
            this.enemyList = dungeonTemplate.enemyList;
            this.appearChance = dungeonTemplate.appearChance;
            this.mimic = dungeonTemplate.mimic;
            this.battleImg = Resources.Load<Sprite>("Background/Dungeon BG/" + dungeonTemplate.bg);
            if (dungeonTemplate.lootItemId.Length == dungeonTemplate.lootMaxAmount.Length && dungeonTemplate.lootMaxAmount.Length == dungeonTemplate.lootMinAmount.Length)
            {
                this.equipmentDropLv = dungeonTemplate.equipmentDropLv;
                this.lootItemId = dungeonTemplate.lootItemId;
                this.lootMaxAmount = dungeonTemplate.lootMaxAmount;
                this.lootMinAmount = dungeonTemplate.lootMinAmount;
            }
            else
            {   
                this.lootItemId = new int[0];
                this.lootMaxAmount = new int[0];
                this.lootMinAmount = new int[0];
                Debug.LogError("dungeon loot length mismatch, lootItemId=" + dungeonTemplate.lootItemId.Length + ", lootMaxAmount=" + dungeonTemplate.lootMaxAmount.Length + ", lootMinAmount=" + dungeonTemplate.lootMinAmount.Length);
            }

        }

        public void BackToPreviousRoom()
        {
            if (currentFloorNum > 0)
            {
                currentFloorNum -= 1;
                //find the door room and go back to there
                for (int i = 0; i < 3; i++)
                {
                    int room = dungeonLayout[currentFloorNum, i];
                    if (room == (int)RoomType.ONE_DOOR || room == (int)RoomType.TWO_DOOR || room == (int)RoomType.THREE_DOOR)
                    {
                        currentRoomNum = i;
                        break;
                    }
                }
            }
        }

        public void GoToDoor(int doorNo)
        {
            if (currentFloorNum < dungeonLayout.GetLength(0))
            {
                currentFloorNum++;
                currentRoomNum = doorNo;
            }
        }

        public Action ChooseOption(int option)
        {
            switch (option)
            {
                case 1:
                    if (
                        currentRoom.roomType == (int)RoomType.BEFORE_BOSS ||
                        currentRoom.roomType == (int)RoomType.ONE_DOOR ||
                        currentRoom.roomType == (int)RoomType.TWO_DOOR ||
                        currentRoom.roomType == (int)RoomType.THREE_DOOR
                    )
                    {
                        GoToDoor(0);
                        return Action.None;
                    }
                    else if (
                        currentRoom.roomType == (int)RoomType.EMPTY ||
                        currentRoom.roomType == (int)RoomType.TRAP
                    )
                    {
                        BackToPreviousRoom();
                        return Action.None;
                    }
                    else if (
                        currentRoom.roomType == (int)RoomType.BOSS ||
                        currentRoom.roomType == (int)RoomType.MONSTER ||
                        currentRoom.roomType == (int)RoomType.TREASURE_AND_MONSTER ||
                        currentRoom.roomType == (int)RoomType.MIMIC
                    )
                    {
                        //BATTLE
                        return Action.EnterBattle;
                    }
                    else if (
                        currentRoom.roomType == (int)RoomType.TREASURE
                    )
                    {
                        //Open Treasure
                        return Action.ShowLoot;
                    }
                    else if (
                        currentRoom.roomType == (int)RoomType.TRANSPORTATION_TRAP
                    )
                    {
                        //Get transported
                        return Action.None;
                    }
                    break;
                case 2:
                    if (
                        currentRoom.roomType == (int)RoomType.TWO_DOOR ||
                        currentRoom.roomType == (int)RoomType.THREE_DOOR
                    )
                    {
                        GoToDoor(1);
                        return Action.None;
                    }
                    else if (
                        currentRoom.roomType == (int)RoomType.BEFORE_BOSS ||
                        currentRoom.roomType == (int)RoomType.MIMIC ||
                        currentRoom.roomType == (int)RoomType.TRANSPORTATION_TRAP ||
                        currentRoom.roomType == (int)RoomType.TREASURE ||
                        currentRoom.roomType == (int)RoomType.ONE_DOOR
                    )
                    {
                        BackToPreviousRoom();
                        return Action.None;
                    }
                    break;
                case 3:
                    if (
                        currentRoom.roomType == (int)RoomType.TWO_DOOR
                    )
                    {
                        BackToPreviousRoom();
                        return Action.None;
                    }
                    else if (
                        currentRoom.roomType == (int)RoomType.THREE_DOOR
                    )
                    {
                        GoToDoor(2);
                        return Action.None;
                    }
                    break;
                case 4:
                    if (
                        currentRoom.roomType == (int)RoomType.THREE_DOOR
                    )
                    {
                        BackToPreviousRoom();
                        return Action.None;
                    }
                    break;
                default:
                    break;
            }
            return Action.None;
        }

        public StorageSystem GetLoot()
        {
            int rndLootNo = UnityEngine.Random.Range(1, 4);
            StorageSystem lootStorage = new StorageSystem(rndLootNo);
            List<int> rndLootItemId = Util.GetMultipleNumFromRandomList(lootItemId.ToList(), rndLootNo);
            for (int i = 0; i < rndLootNo; i++)
            {
                Item lootItem = DB.QueryItem(rndLootItemId[i]);
                int indexOfItem = Array.FindIndex(lootItemId, (l) => l == rndLootItemId[i]);
                int rndQty = UnityEngine.Random.Range(lootMinAmount[indexOfItem], lootMaxAmount[indexOfItem] + 1);
                lootStorage.smartInsert(lootItem, rndQty);
            }
            return lootStorage;
        }

        public EntityEnemy[] GenerateEnemy()
        {
            List<EntityEnemy> generatedEnemyList = new List<EntityEnemy>();
            //if player is in last zone, generate boss enemy
            if (currentRoom.roomType == (int)RoomType.BOSS)
            {
                generatedEnemyList.Add(DB.QueryEnemy(boss).toEntity());
            }
            else if(currentRoom.roomType == (int)RoomType.MIMIC){
                generatedEnemyList.Add(DB.QueryEnemy(mimic).toEntity());
            }
            else
            {
                int maxEnemy = 5;
                int enemyNum = UnityEngine.Random.Range(1, maxEnemy);
                for (int i = 0; i < enemyNum; i++)
                {
                    int monsterCodexValue = Game.globalBuffManager.GetMonsterCodexValue();
                    int rndEnemyStrength = 0;
                    if (monsterCodexValue == 1)
                    {
                        rndEnemyStrength = UnityEngine.Random.Range(3, 5);
                    }
                    else if (monsterCodexValue == -1)
                    {
                        rndEnemyStrength = UnityEngine.Random.Range(0, 3);
                    }
                    else
                    {
                        rndEnemyStrength = UnityEngine.Random.Range(0, 5);
                    }
                    EntityEnemy enemy = DB.QueryEnemy(enemyList[Util.getRandomIndexFrom(appearChance, 100f)]).toEntity(rndEnemyStrength, 1);
                    generatedEnemyList.Add(enemy);
                }
            }
            return generatedEnemyList.ToArray();
        }

        public void ChangeCurrentRoomType(){
            if(currentRoom.roomType == (int)RoomType.TRAP || currentRoom.roomType == (int)RoomType.MONSTER || currentRoom.roomType == (int)RoomType.TREASURE){
                dungeonLayout[currentFloorNum,currentRoomNum] = (int)RoomType.EMPTY;
            }else if(currentRoom.roomType == (int)RoomType.MONSTER){
                dungeonLayout[currentFloorNum,currentRoomNum] = (int)RoomType.TREASURE;
            }
        }

        public bool IsBossFight(){
            return currentRoom.roomType == (int)RoomType.BOSS;
        }
    }

}