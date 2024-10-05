using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace RPG
{
    public class TownResources
    {
        public enum Type
        {
            Food = 0,
            Wood = 1,
            Stone = 2,
            Metal = 3
        }
        public int[] AllResources { get; set; }
        public int Food
        {
            get
            {
                return AllResources[(int)Type.Food];
            }
            set
            {
                AllResources[(int)Type.Food] = value;
            }
        }
        public int Wood
        {
            get
            {
                return AllResources[(int)Type.Wood];
            }
            set
            {
                AllResources[(int)Type.Wood] = value;
            }
        }
        public int Stone
        {
            get
            {
                return AllResources[(int)Type.Stone];
            }
            set
            {
                AllResources[(int)Type.Stone] = value;
            }
        }
        public int Metal
        {
            get
            {
                return AllResources[(int)Type.Metal];
            }
            set
            {
                AllResources[(int)Type.Metal] = value;
            }
        }
        public TownResources()
        {
            AllResources = new int[4];
            Food = 200;
            Wood = 200;
            Stone = 200;
            Metal = 200;
        }

        public string OnSave(){
            return String.Join(",", AllResources);
        }

        public void OnLoad(string saveStr){
            if(saveStr.Split(',').Length == 4){
                AllResources = saveStr.Split(',').Select(int.Parse).ToArray();
            }
        }
    }
}