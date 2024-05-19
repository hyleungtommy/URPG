using System;
using System.Collections;
using System.Collections.Generic;
namespace RPG{
    [Serializable]
    public class EnchantRecipeTemplate{
        public int equipmentLv;
        public int[] requireItem;
        public int[] requireQty;
        public int requireMoney;

        public EnchantmentData toEnchantmentData(){
            EnchantmentData ed = new EnchantmentData();
            List<Requirement>reqs = new List<Requirement>();
            for(int i = 0 ; i < requireItem.Length ; i++){
                reqs.Add(new Requirement(DB.QueryItem(requireItem[i]),requireQty[i],Requirement.Type.Item));
            }
            ed.requirements = reqs;
            ed.requireMoney = requireMoney;
            return ed;
        }
    }
}