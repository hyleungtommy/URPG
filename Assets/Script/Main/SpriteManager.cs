using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    /// <summary>
    /// Contains all public sprite resources
    /// </summary>
    public static class SpriteManager
    {
        public static Sprite basicBoxSelected { set; get; }
        public static Sprite basicBoxNormal { set; get; }
        public static Sprite youWin { set; get; }
        public static Sprite youLose { set; get; }
        public static Sprite[] floatingTextEnemyDamage { set; get; }
        public static Sprite[] floatingTextPlayerDamage { set; get; }
        public static Sprite[] floatingTextMpDamage { set; get; }
        public static Sprite[] floatingTextHeal { set; get; }
        public static Sprite[] floatingTextMpHeal { set; get; }
        public static Dictionary<string,Sprite> buffImgs {set; get;}
        static SpriteManager()
        {
            basicBoxSelected = Resources.Load<Sprite>("UI/Frame/item_frame_selected");
            basicBoxNormal = Resources.Load<Sprite>("UI/Frame/item_frame");
            youWin = Resources.Load<Sprite>("UI/Text/you win");
            youLose = Resources.Load<Sprite>("UI/Text/you lose");
            //load floating numbers
            Sprite[] allFloatingNumber = Resources.LoadAll<Sprite>("UI/Text/num");
            floatingTextEnemyDamage = new Sprite[10];
            floatingTextPlayerDamage = new Sprite[10];
            floatingTextMpDamage = new Sprite[10];
            floatingTextHeal = new Sprite[10];
            floatingTextMpHeal = new Sprite[10];
            for (int i = 0; i < 10; i++)
            {
                floatingTextEnemyDamage[i] = allFloatingNumber[i];
                floatingTextPlayerDamage[i] = allFloatingNumber[i + 10];
                floatingTextMpDamage[i] = allFloatingNumber[i + 20];
                floatingTextHeal[i] = allFloatingNumber[i + 30];
                floatingTextMpHeal[i] = allFloatingNumber[i + 40];
            }
            //load buffs
            buffImgs = new Dictionary<string, Sprite>();
            foreach(BuffTemplate b in DB.buffs){
                buffImgs.Add(b.img,Resources.Load<Sprite>("Buff Icon/" + b.img));
            }
        }
    }
}