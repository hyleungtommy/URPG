using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class TownResources{
        public enum Type {
            Food = 0,
            Wood = 1,
            Stone = 2,
            Metal = 3
        }
        public int Food{get; set;}
        public int Wood{get; set;}
        public int Stone{get; set;}
        public int Metal{get; set;}
        public TownResources(){
            Food = 500;
            Wood = 500;
            Stone = 500;
            Metal = 500;
        }
    }
}