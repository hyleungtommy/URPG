using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class Param
    {
        public const bool isDevelopment = true;
        //EXP gain multiplier
        public const float expRatio = 2.0f;
        //EXP gain increment by each area
        public const float areaRewardMultiplier = 0.05f;
        public const int upptGainPerLv = 5;
        public const int skillPtsGainPerLv = 2;
        public static bool learntAllSkill = true;
        public const int startLv = 20;
        public static bool unlockAllCharacter = true;
        public static float maxMapEnemyModifier = 1.5f;
        public static int equipmentDropRate = 100;
        public static int invSize = 100;

        public static int[] characterStartingLv = new int[]{
            1,1,10,20,30,40,50,50
        };


    }
}