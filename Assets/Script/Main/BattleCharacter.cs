using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class BattleCharacter : Displayable
    {
        public int id { get; set; }
        public string name { get; set; }
        public int lv { get; set; } //Need To Save
        public int currexp { get; set; } //Need To Save
        public int expneed { get; set; }
        public int uppt
        {
            get
            {
                return upptEarned - upptSpend;
            }
        }
        public int upptSpend
        {
            get
            {
                return (int)Util.calculateSum(upptAlloc);
            }
        }
        public int[] upptAlloc { get; set; } //Need To Save
        public int upptEarned { get; set; } //Need To Save
        public Job job { get; set; }
        public BattleCharacterStat stat { get; set; }
        public Sprite[] faceImg { get; set; }
        public Sprite bodyImg { get; set; }
        public bool unlocked { get; set; } //Need To Save
        public int listPos { get; set; } //Need To Save
        public EquipmentManager equipmentManager { get; set; } //Need To Save
        public int skillPtsAvailable
        {
            get
            {
                return skillPtsEarned - skillPtsSpent;
            }
        }
        public int skillPtsEarned { get; set; }
        public int skillPtsSpent { get; set; }
        public BattleCharacter(string name, Sprite[] faceImg, Sprite bodyImg, Job job, bool unlocked, int startingLv) : base(faceImg[0])
        {
            this.name = name;
            this.faceImg = faceImg;
            this.bodyImg = bodyImg;
            this.job = job;
            this.expneed = Util.getRequireEXPForLevel(startingLv);
            this.upptEarned = Param.upptGainPerLv * startingLv;
            this.lv = startingLv;
            this.skillPtsEarned = Param.skillPtsGainPerLv * startingLv;
            this.skillPtsSpent = 0;
            this.unlocked = unlocked;
            upptAlloc = new int[] { 0, 0, 0, 0, 0 };
            equipmentManager = new EquipmentManager();
            getBattleCharacterStat();
        }

        //Save format : lv,currexp,upptEarned,stamina_alloc,strength_alloc,mana_alloc,agi_alloc,dex_alloc,unlocked,listPos,skillPtEarn,skillPtsSpent
        public void onload(string save)
        {
            string[] data = save.Split('|');
            if (data.Length == 12)
            {
                lv = int.Parse(data[0]);
                currexp = int.Parse(data[1]);
                expneed = Util.getRequireEXPForLevel(lv);
                upptEarned = int.Parse(data[2]);

                upptAlloc[0] = int.Parse(data[3]);
                upptAlloc[1] = int.Parse(data[4]);
                upptAlloc[2] = int.Parse(data[5]);
                upptAlloc[3] = int.Parse(data[6]);
                upptAlloc[4] = int.Parse(data[7]);

                getBattleCharacterStat();

                unlocked = (int.Parse(data[8]) == 1 ? true : false);
                listPos = int.Parse(data[9]);
                skillPtsEarned = int.Parse(data[10]);
                skillPtsSpent = int.Parse(data[11]);
            }
            else
            {
                Debug.Log("length is not correct :" + save);
            }

        }

        public string onsave()
        {
            int unlock = (unlocked ? 1 : 0);
            return lv + "|" + currexp + "|" + upptEarned + "|" + upptAlloc[0] + "|" + upptAlloc[1] + "|" + upptAlloc[2] + "|" + upptAlloc[3] + "|" + upptAlloc[4] + "|" + unlock + "|" + listPos + "|" + skillPtsEarned + "|" + skillPtsSpent;
        }

        public EntityPlayer toEntity()
        {
            //Debug.Log(name + "=" + stat);
            BasicStat statt = this.stat.toBasicStat();
            statt = statt.plus(equipmentManager.getEquipmentStat());
            statt = statt.multiply(equipmentManager.getEquipmentEnchantmentStat());
            
            Debug.Log(name + " stat:" + statt.ToString() + " equip stat: " + equipmentManager.getEquipmentStat().ToString() + " enchant stat=" + equipmentManager.getEquipmentEnchantmentStat().ToString());
            EntityPlayer player = new EntityPlayer(name, statt, faceImg[0], job.createSkillList());
            return player;
        }

        public bool assignEXP(int value)
        {
            //Debug.Log("assignEXP value=" + value);
            currexp += value;
            if (currexp >= expneed)
            {
                levelUp();
                return true;
            }
            //Debug.Log("assignEXP BattleMember=" + ToString());
            return false;
        }

        public void assignUPPT(int[] upptTempAlloc){
            for(int i = 0 ; i < 5 ; i++){
                upptAlloc[i] += upptTempAlloc[i];
            }
            getBattleCharacterStat();
        }

        private void levelUp()
        {
            lv++;
            currexp -= expneed;
            upptEarned += Param.upptGainPerLv;
            skillPtsEarned += Param.skillPtsGainPerLv;
            expneed = Util.getRequireEXPForLevel(lv);
            getBattleCharacterStat();

        }

        void getBattleCharacterStat()
        {
            stat = new BattleCharacterStat();
            stat.stamina = upptAlloc[0] + job.levelUpGain[0] * lv + job.startingStat[0];
            stat.strength = upptAlloc[1] + job.levelUpGain[1] * lv + job.startingStat[1];
            stat.mana = upptAlloc[2] + job.levelUpGain[2] * lv + job.startingStat[2];
            stat.agility = upptAlloc[3] + job.levelUpGain[3] * lv + job.startingStat[3];
            stat.dexterity = upptAlloc[4] + job.levelUpGain[4] * lv + job.startingStat[4];
        }

    }
}