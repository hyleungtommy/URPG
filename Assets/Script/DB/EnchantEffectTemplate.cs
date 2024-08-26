using System;
namespace RPG{
    [Serializable]
    public class EnchantEffectTemplate{
        public int id;
        public string name;
        public string desc;
        public int maxLv;
        public float modifierStart;
        public float modifierIncrement;
        public bool[] equipTypeWhiteList;
        public EnchantmentEffect toEnchantmentEffect(int level){
            EnchantmentEffect effect = new EnchantmentEffect();
            effect.id = id;
            effect.lv = level;
            effect.name = name;
            effect.modifier = modifierStart + modifierIncrement * (level-1);
            effect.desc = desc.Replace("%m", (effect.modifier * 100).ToString());
            effect.equipTypeWhiteList = equipTypeWhiteList;
            return effect;
        }
    }
}