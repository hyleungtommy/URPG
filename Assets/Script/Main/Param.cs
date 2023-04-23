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
        public static int equipmentDropRate = 15;
        public static int invSize = 100;
        public static int rareEnemyAppearChance = 5;

        public static int[] characterStartingLv = new int[]{
            1,1,10,20,30,40,50,50
        };

        public static string[] difficulty = {
            "Easy",
            "Normal",
            "Hard",
            "Lunatic"
        };

        public static float[] difficultyModifier = {
            0.75f,
            1f,
            1.25f,
            2f
        };
        
        public static int[]expRequire = {
            100,
            150,
            200,
            250,
            300,
            350,
            400,
            500,
            600,
            1000,//Lv.10
            1500,
            2000,
            2500,
            3000,
            3500,
            4000,
            4500,
            5000,
            5500,
            10000,//Lv.20
            12500,
            15000,
            17500,
            20000,
            25000,
            30000,
            35000,
            40000,
            45000,
            60000,//Lv.30
            65000,
            70000,
            75000,
            80000,
            85000,
            90000,
            95000,
            100000,
            110000,
            130000,//Lv.40
            150000,
            175000,
            200000,
            225000,
            250000,
            275000,
            300000,
            325000,
            400000,
            500000,//Lv.50
            550000,
            600000,
            650000,
            700000,
            750000,
            800000,
            850000,
            900000,
            950000,
            1000000//Lv.60
        };


    }
}