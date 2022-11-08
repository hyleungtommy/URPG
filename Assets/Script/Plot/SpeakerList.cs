using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RPG
{
    public static class SpeakerList
    {
        public static Speaker[] list = new Speaker[]{
            new Speaker("Narrator",new String[]{""}),
            new Speaker("%playername%",new String[]{"Face_Adventurer"}),
            new Speaker("Anson",new String[]{"Berserker_Face1","Berserker_Face2"}),
            new Speaker("Villager1",new String[]{"Villager1_Face1"}),
            new Speaker("Simon",new String[]{"Knight_Face1","Knight_Face2"}),
            new Speaker("Guild Receptionist",new String[]{"Guild-Receptionist_Face1"}),
            new Speaker("Conrad",new String[]{"Conrad_Face1","Conrad_Face2"}),
            new Speaker("Ted",new String[]{"Ted_Face1"}),
            new Speaker("Ivan",new String[]{"Mage_Face1"}),
            new Speaker("Jimmy",new String[]{"Priest_Face1"}),
            new Speaker("Caroline",new String[]{"Caroline_Face1","Caroline_Face2"}),
            new Speaker("Andy",new String[]{"Necromancer_Face1"}),
            new Speaker("Matius",new String[]{"Matius_Face1","Matius_Face2"}),
            new Speaker("Katie",new String[]{"Archer_Face1"}),
            new Speaker("Chi Ka",new String[]{"Assassin_Face1"})
        };
        static SpeakerList()
        {
        }

        public static Speaker findSpeaker(string name)
        {
            return Array.Find(list, s => s.name == name);
        }

    }
}