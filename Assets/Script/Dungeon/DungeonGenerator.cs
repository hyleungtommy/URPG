using UnityEngine;
namespace RPG{
    public class DungeonGenerator{
    // Dungeon generation rule:
    // 1. Dunegon is generated in tree structure
    // 2. Each level has 2-3 rooms
    // 3. Each level has exactly 1 room to next level (rm type 1,2,3)
    // 4. Last 2 level must be room 5 and room 6
        public static int[,] GenerateDungeon(int dungeonId){
            DungeonTemplate dungeonTemplate = DB.dunegonTemplates[dungeonId];
            int rndFloor = Random.Range(dungeonTemplate.minFloor, dungeonTemplate.maxFloor + 1);
            int[,] dungeon = new int[rndFloor,3];
            int[] rmNoForEachFloor = new int[rndFloor];
            //generate floors
            for(int i = 1 ; i < rndFloor - 2 ; i++){
                int rndTotalRmNo = Random.Range(1,3) + 1;
                rmNoForEachFloor[i] = rndTotalRmNo;
                //generate rooms
                for(int j = 0 ; j < rndTotalRmNo; j++){
                    dungeon[i,j] = dungeonTemplate.availableRoomType[Random.Range(0,dungeonTemplate.availableRoomType.Length)];
                }
            }
            //generate before boss & boss floor
            dungeon[rndFloor - 2,0] = (int)RoomType.BEFORE_BOSS;
            dungeon[rndFloor - 1,0] = (int)RoomType.BOSS;
            rmNoForEachFloor[rndFloor - 2] = rmNoForEachFloor[rndFloor - 1] = 1;
            //replace one room with door room
            for(int i = 0 ; i < rndFloor - 2 ; i++){
                int rmNoForNextFloor = rmNoForEachFloor[i + 1];
                int rndPosition = Random.Range(0,rmNoForNextFloor);
                int rmType;
                if(rmNoForNextFloor == 1){
                    rmType = (int)RoomType.ONE_DOOR;
                }else if(rmNoForNextFloor ==2){
                    rmType = (int)RoomType.TWO_DOOR;
                }else{
                    rmType = (int)RoomType.THREE_DOOR;
                }
                dungeon[i,rndPosition] = rmType;
            }
            Print2DArray(dungeon);
            return dungeon;
        }

        static void Print2DArray(int[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                string str = "";
                for (int j = 0; j < cols; j++)
                {
                   str += array[i, j] + ",";
                }
                Debug.Log("Floor " + i + " : [" + str + "]");
            }
        }
    }
}