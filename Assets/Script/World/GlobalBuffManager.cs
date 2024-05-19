using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace RPG
{
    public class GlobalBuffManager{
        public List<GlobalBuff> buffList {get;}

        public GlobalBuffManager(){
            buffList = new List<GlobalBuff>();
        }
        public void addBuff(GlobalBuff globalBuff){
            //replace exist buff if it is the same type
            List<GlobalBuff> sameTypeBuffs = buffList.Where(buff => buff.type.Equals(globalBuff.type)).ToList();
            if(sameTypeBuffs.Count > 0){
                buffList.Remove(sameTypeBuffs[0]);
            }
            buffList.Add(globalBuff);
        }

        public string onSave(){
            string globalBuffSaveStr = string.Join("|", buffList.Select(buff => buff.onSave()).ToList());
            return globalBuffSaveStr;
        }

        public void onLoad(string saveStr){
            buffList.Clear();
            string[] globalBuffSaveSplit = saveStr.Split('|');
            foreach(string globalBuffSave in globalBuffSaveSplit){
                string[]globalBuffSaveSplot =  globalBuffSave.Split(',');
                if(globalBuffSaveSplot.Length == 2){
                    int id = Int32.Parse(globalBuffSave.Split(',')[0]);
                    int rounds = Int32.Parse(globalBuffSave.Split(',')[1]);
                    GlobalBuffTemplate globalBuffTemplate = DB.globalBuffTemplates[id - 1];
                    buffList.Add(globalBuffTemplate.ToGlobalBuff(rounds));    
                }
            }
        }

        public float GetModFromType(string type){
            List<GlobalBuff>globalBuff = buffList.Where(buff => buff.type.Equals(type)).ToList();
            if(globalBuff.Count > 0){
                float mod = globalBuff[0].mod;
                return mod;
            }
            return 1;
        }

        public int GetMonsterCodexValue(){
            List<GlobalBuff>globalBuff = buffList.Where(buff => buff.type.Equals("Monster")).ToList();
            if(globalBuff.Count > 0){
                float mod = globalBuff[0].mod;
                return (int)mod;
            }
            return 0;
        }

        public int GetCount(){
            return buffList.Count;
        }

        public GlobalBuff GetBuff(int index){
            return buffList[index];
        }

        public void PassRound(){
            foreach(GlobalBuff globalBuff in buffList.ToArray()){
                if(globalBuff.rounds <= 1){
                    buffList.Remove(globalBuff);
                }else{
                    globalBuff.rounds -= 1;
                }
            }
        }



    }
}