using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
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
        public static Sprite[] buffs { set; get; }
        public static Sprite[] debuffs { set; get; }
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
            buffs = new Sprite[8];
            debuffs = new Sprite[8];
            string[] buffImgPathGood = new string[] { "buff_hp", "buff_mp", "buff_atk", "buff_def", "buff_matk", "buff_mdef", "buff_agi", "buff_dex" };
            string[] buffImgPathBad = new string[] { "debuff_hp", "debuff_mp", "debuff_atk", "debuff_def", "debuff_matk", "debuff_mdef", "debuff_agi", "debuff_dex" };
            for (int i = 0; i < 8; i++)
            {
                buffs[i] = Resources.Load<Sprite>("Buff Icon/" + buffImgPathGood[i]);
                //Debug.Log(buffs[i]);
                debuffs[i] = Resources.Load<Sprite>("Buff Icon/" + buffImgPathBad[i]);
            }
        }
    }
}