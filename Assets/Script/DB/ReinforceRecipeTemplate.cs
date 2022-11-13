using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class ReinforceRecipeTemplate
    {
        public string[]requireItem;
        public int[]requireQtyStart;
        public int requireMoneyStart;
        public int requireLevel;
        public int[] requireQtyIncrement;
        public int requireMoneyIncrement;
        public int maxReinforceLv;
        public int powerIncrementPerLevel;
        public int magicPowerIncrementPerLevel;

    }
}