using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class GlobalBuff{
        public int id {get; set;}
        public string name {get; set;}
        public string type {get; set;}
        public float mod {get; set;}
        public Sprite img {get; set;}
        public int rounds {get; set;}

        public string onSave(){
            return id + "," + rounds;
        }
    }
}