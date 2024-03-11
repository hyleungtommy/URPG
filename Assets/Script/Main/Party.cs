using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace RPG
{
    /// <summary>
    /// Represent a player party
    /// </summary>
    public class Party
    {
        private BattleCharacter[] battleParty;
        //private SupportCharacter[] supportParty;

        public Party()
        {
            battleParty = new BattleCharacter[]{
                new BattleCharacter("tommy",Resources.LoadAll<Sprite>("Characters/Dialog Face/Face_Adventurer"),Resources.Load<Sprite>("Characters/Body/Body_Adventurer"),DB.jobs[0],true,Param.characterStartingLv[0]),
                new BattleCharacter("Anson",Resources.LoadAll<Sprite>("Characters/Dialog Face/Berserker_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Berserker"),DB.jobs[1],true,Param.characterStartingLv[1]),
                new BattleCharacter("Simon",Resources.LoadAll<Sprite>("Characters/Dialog Face/Knight_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Knight"),DB.jobs[2],false,Param.characterStartingLv[2]),
                new BattleCharacter("Ivan",Resources.LoadAll<Sprite>("Characters/Dialog Face/Mage_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Mage"),DB.jobs[3],false,Param.characterStartingLv[3]),
                new BattleCharacter("Jimmy",Resources.LoadAll<Sprite>("Characters/Dialog Face/Priest_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Priest"),DB.jobs[4],false,Param.characterStartingLv[4]),
                new BattleCharacter("Andy",Resources.LoadAll<Sprite>("Characters/Dialog Face/Necromancer_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Necromancer"),DB.jobs[5],false,Param.characterStartingLv[5]),
                new BattleCharacter("Chi Ka",Resources.LoadAll<Sprite>("Characters/Dialog Face/Archer_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Archer"),DB.jobs[6],false,Param.characterStartingLv[6]),
                new BattleCharacter("Katie",Resources.LoadAll<Sprite>("Characters/Dialog Face/Assassin_Face1"),Resources.Load<Sprite>("Characters/Body/Body_Assassin"),DB.jobs[7],false,Param.characterStartingLv[7]),
            };
            for (int i = 0; i < 8; i++)
            {
                battleParty[i].id = i;
                battleParty[i].listPos = i;
                if (Param.unlockAllCharacter) battleParty[i].unlocked = true;
                //battleParty[i].onload("1|0|0|0|0|0|0|0|0");
            }
            /*
            supportParty = new SupportCharacter[]{
                new SupportCharacter("Jack",Resources.LoadAll<Sprite>("Characters/Dialog Face/Berserker_Face1")[0]),
                new SupportCharacter("Ann",Resources.LoadAll<Sprite>("Characters/Dialog Face/Assassin_Face1")[0]),
                null,null,null,null,null,null
            };
            */

        }
        /// <summary>
        /// Get all player character that is in the first column (will go to battle)
        /// </summary>
        public BattleCharacter[] getAllBattleCharacter()
        {
            return battleParty.Where(a => (a.unlocked == true) && a.listPos < 4).ToArray();
        }

        /// <summary>
        /// Get all available characters in the party
        /// </summary>
        /// <returns>A list of available character ordered according to predefined position in selection menu</returns>
        public BattleCharacter[] getAllUnlockedCharacter()
        {
            BattleCharacter[] list = new BattleCharacter[8];
            for (int i = 0; i < 8; i++)
            {
                if (battleParty[i].unlocked)
                {
                    list[battleParty[i].listPos] = battleParty[i];
                }
            }
            return list;
        }

        /* 
                public BattleCharacter[] getBattleCharacters()
                {
                    BattleCharacter[] list = new BattleCharacter[4];
                    list[0] = battleParty[0];
                    list[1] = battleParty[1];
                    list[2] = battleParty[2];
                    list[3] = battleParty[3];
                    return list;
                }

                public BattleCharacter[] getIdleCharacters()
                {
                    BattleCharacter[] list = new BattleCharacter[4];
                    list[0] = battleParty[4];
                    list[1] = battleParty[5];
                    list[2] = battleParty[6];
                    list[3] = battleParty[7];
                    return list;
                }*/

        /// <summary>
        /// Create a list of EntityPlayer from the first column of the selection menu
        /// </summary>
        /// <returns>A list of EntityPlayer object represent the party member that join the battle</returns>
        public EntityPlayer[] createBattleParty()
        {

            BattleCharacter[] list = getAllBattleCharacter();
            List<EntityPlayer> elist = new List<EntityPlayer>();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    elist.Add(list[i].toEntity());
                }
            }
            return elist.ToArray(); ;
        }

        /// <summary>
        /// Unlock a specific character
        /// </summary>
        public void unlockCharacter(int pos)
        {
            if (pos > 0 && pos < battleParty.Length)
            {
                battleParty[pos].unlocked = true;
                Debug.Log("unlock character " + battleParty[pos].name);
            }

        }

        public void save()
        {
            int i = 0;
            foreach (BattleCharacter ch in battleParty)
            {
                string chSave = ch.onsave();
                Debug.Log(SaveKey.battle_character_i + i + ":" + chSave);
                SaveManager.saveValue(SaveKey.battle_character_i + i, chSave);
                string equipManSave = ch.equipmentManager.onSave();
                Debug.Log(SaveKey.equip_manager_i + i + ":" + equipManSave);
                SaveManager.saveValue(SaveKey.equip_manager_i + i, equipManSave);
                i++;
            }
            i = 0;
            foreach (Job j in DB.jobs)
            {
                string jobSave = j.onSave();
                Debug.Log(SaveKey.job_i + i + ":" + jobSave);
                SaveManager.saveValue(SaveKey.job_i + i, jobSave);
                i++;
            }

        }

        public void load()
        {
            int i = 0;
            foreach (BattleCharacter ch in battleParty)
            {
                string chSave = SaveManager.getString(SaveKey.battle_character_i + i);
                ch.onload(chSave);
                string equipManSave = SaveManager.getString(SaveKey.equip_manager_i + i);
                ch.equipmentManager.onLoad(equipManSave);
                i++;
            }
            i = 0;
            foreach (Job j in DB.jobs)
            {
                string jobSave = SaveManager.getString(SaveKey.job_i + i);
                j.onLoad(jobSave);
                i++;
            }
        }

        /// <summary>
        /// Testing purpose, unlock all member from start
        /// </summary>
        public void unlockAllMember()
        {
            foreach (BattleCharacter ch in battleParty)
            {
                ch.unlocked = true;
            }
        }

        /// <summary>
        /// Testing purpose, learn all skill from start
        /// </summary>
        public void learntAllSkill()
        {
            foreach (BattleCharacter ch in battleParty)
            {
                foreach (GeneralSkill s in ch.job.skills)
                {
                    s.learn();
                }
            }
        }
        /*
        public SupportCharacter[] getSupportCharacterList()
        {
            return supportParty;
        }

        public SupportCharacter[] getSupportCharacterList(int craftSkillTypeId)
        {
            List<SupportCharacter> characters = new List<SupportCharacter>();
            foreach (SupportCharacter ch in supportParty)
            {
                if (ch != null)
                {
                    foreach (SkillCraft sc in ch.craftSkillSet)
                    {
                        if (sc.type == craftSkillTypeId)
                        {
                            characters.Add(ch);
                            break;
                        }
                    }
                }
            }
            return characters.ToArray();
        }

        public void addNewSupportCharacter(SupportCharacter newCharacter)
        {
            for (int i = 0; i < supportParty.Length; i++)
            {
                if (supportParty[i] == null)
                {
                    supportParty[i] = newCharacter;
                }
            }
        }

        public void fireSupportCharacter(int slotId)
        {
            supportParty[slotId] = null;
        }

        public int getSupportPartyLength()
        {
            return supportParty.Where(a => a != null).ToArray().Length;
        }
        */
    }
}