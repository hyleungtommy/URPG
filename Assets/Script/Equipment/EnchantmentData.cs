using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace RPG{
    public class EnchantmentData{
        public List<Requirement>requirements{get;set;}
        public int requireMoney{get;set;}
        public List<EnchantmentEffect>effects{get;set;}

        public EnchantmentData(){
            effects = new List<EnchantmentEffect>();
        }

        public BasicStat getEnchantmentMatrix(){
            float[]matrix = {0f,0f,0f,0f,0f,0f,0f,0f,0f};
            foreach(EnchantmentEffect effect in effects){
                if(effect.id <= 8)//only for enchantments that increase basic stat
                    matrix[effect.id - 1] = effect.modifier;
            }
            return new BasicStat(matrix[0],matrix[1],matrix[2],matrix[3],matrix[4],matrix[5],matrix[6],matrix[7]);
        }

        public string onSave(){
            string saveStr = "";
            foreach(EnchantmentEffect effect in effects){
                saveStr += (effect.onSave() + "!");
            }
            return saveStr;
        }

        public void onLoad(string saveStr){
            if(saveStr.Length > 0){
                //Debug.Log("enchant save str=" + saveStr);
                string[]saveArr = saveStr.Split('!');
                foreach(string s in saveArr){
                    if(s.Length == 3){
                        int enchantId = Int32.Parse(s.Split('\'')[0]);
                        int enchantLv = Int32.Parse(s.Split('\'')[1]);
                        EnchantmentEffect effect = DB.enchantmentEffects[enchantId - 1].toEnchantmentEffect(enchantLv);
                        effects.Add(effect);
                    }
                }
            }
        }
    }
}