using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class GlobalBuffTemplate
    {
        public int id;
        public string name;
        public string type;
        public float mod;
        public string img;

        public GlobalBuff ToGlobalBuff(Sprite img, int rounds){
            GlobalBuff globalBuff = new GlobalBuff();
            globalBuff.id = id;
            globalBuff.img = img;
            globalBuff.mod = mod;
            globalBuff.type = type;
            globalBuff.rounds = rounds;
            return globalBuff;
        }

        public GlobalBuff ToGlobalBuff(int rounds){
            GlobalBuff globalBuff = new GlobalBuff();
            globalBuff.id = id;
            globalBuff.img = Resources.Load<Sprite>("Item/" + img);
            globalBuff.mod = mod;
            globalBuff.type = type;
            globalBuff.rounds = rounds;
            return globalBuff;
        }

    }
}