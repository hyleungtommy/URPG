using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class DailyQuest:Displayable
{
    public bool accepted {get; set;}
    public int questId {get; set;}
    public string name {get; set;}
    public string desc {get; set;}
    public int requireLv {get; set;}
    public int maxLv {get; set;}
    public List<Requirement> requirements {get; set;}
    public int rewardEXP {get; set;}
    public int rewardMoney {get; set;}
    
    public DailyQuest():base(null){

    }


}

}