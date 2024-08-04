using UnityEngine;

namespace RPG{
    public class Room{
        static string[] ROOM_DESC = {
            "",
            "There is one door in the room",
            "There is two door in the room, which way do you want to go?",
            "There is three door in the room, which way do you want to go?",
            "The is an empty room",
            "You stand in front of a huge, ominous floor, be prepared...",
            "The guardian of the dungeon appeared! Get ready!",
            "This room is filled with monster! Get ready to battle!",
            "When you step inside the room, a trap activated and your party got hurted!",
            "You saw a treasure chest in the middle of the room",
            "You saw a treasure chest in the middle of the room",
            "You saw a treasure chest in the middle of the room, but there is a guradian too, get ready to battle!",
            "You saw a magical circle in the middle of the room"
        };

        static string[][] ROOM_BUTTON_OPTION = {
            new string[]{""},
            new string[]{"go to the room", "go back"},
            new string[]{"left room", "right room", "go back"},
            new string[]{"left room", "middle room", "right room", "go back"},
            new string[]{"go back"},
            new string[]{"open the door", "go back"},
            new string[]{"battle!"},
            new string[]{"battle!"},
            new string[]{"go back"},
            new string[]{"open the chest", "go back"},
            new string[]{"open the chest", "go back"},
            new string[]{"battle!"},
            new string[]{"get in magical circle", "go back"},
        };

        static int[] ROOM_SPRITE_MAPPING = {
            3,3,3,3,2,0,1,5,6,7,7,7,4
        };

        public int roomType {get; set;}
        public string desc {get{
            return ROOM_DESC[roomType];
        }}
        public string[] buttonTexts {get{
            return ROOM_BUTTON_OPTION[roomType];
        }}
        public Sprite img{get{
            return SpriteManager.dungeonSprite[ROOM_SPRITE_MAPPING[roomType]];
        }}
        public Room(int roomType){
            this.roomType = roomType;
        }
    }
}