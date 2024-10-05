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
        public BuildingRequirement Requirement {set; get;}
        public Building(Sprite img):base(img){

        }

        public string CanUpgrade(){
            string errorMsg = "";
            if(Game.money < Requirement.RequireMoney){
                errorMsg = "Not enough money";
            }else if(Game.town.Resources.Wood < Requirement.RequireWood){
                errorMsg = "Not enough wood";
            }else if(Game.town.Resources.Stone < Requirement.RequireStone){
                errorMsg = "Not enough stone";
            }else if(Game.town.MaxPopulation - Game.town.Population < Requirement.RequirePopulation){
                errorMsg = "Not enough population, build more house";
            }
            return errorMsg;
        }

        public void UpgradeBuilding(){
            Game.money -= Requirement.RequireMoney;
            Game.town.Resources.Wood -= Requirement.RequireWood;
            Game.town.Resources.Stone -= Requirement.RequireStone;
            Lv ++;
        }

        public string OnSave(){
            return Lv.ToString();
        }

        public void OnLoad(string saveStr){
            Lv = int.Parse(saveStr);
        }

    }
}