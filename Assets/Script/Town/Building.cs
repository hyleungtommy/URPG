using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class Building:Displayable
    {
        public int Id {set; get;}
        public string Name {set; get;}
        public string Desc {set; get;}
        public string Type {set; get;}
        public int Lv {set; get;} //save
        public int RequireMoney {get{
            return RequireMoneyStart + (Lv - 1)* RequireMoneyInc;
        }}
        public int RequireWood {get{
            return RequireWood + (Lv - 1)* RequireWoodInc;
        }}
        public int RequireStone{get{
            return RequireStoneStart + (Lv - 1)* RequireStoneInc;
        }}
        public int RequireMoneyStart {set; get;}
        public int RequireMoneyInc {set; get;}
        public int RequireWoodStart {set; get;}
        public int RequireStoneStart {set; get;}
        public int RequireWoodInc {set; get;}
        public int RequireStoneInc {set; get;}
        public int RequirePopulation {set; get;}
        public int RequireTime {set; get;}
        public Building(Sprite img):base(img){

        }

    }
}