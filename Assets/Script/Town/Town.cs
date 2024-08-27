using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class Town
    {
        public string Name{get; set;}
        public Resources Resource{get; set;}

        public Town(){
            Resource = new Resources();
        }

        
        public class Resources{
            public int Food{get; set;}
            public int Wood{get; set;}
            public int Stone{get; set;}
            public int Metal{get; set;}
            public Resources(){
                Food = 500;
                Wood = 500;
                Stone = 500;
                Metal = 500;
            }
        }

        public class Buildings{
            
        }

    }
}