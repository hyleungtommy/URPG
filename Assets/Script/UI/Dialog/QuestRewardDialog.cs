using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class QuestRewardDialog : MonoBehaviour
{
    public Text rewardMoney;
    public Text rewardEXP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setQuest(DailyQuest dailyQuest){
        rewardMoney.text = dailyQuest.reward.money.ToString();
        rewardEXP.text = dailyQuest.reward.exp.ToString();
    }
    public void show(){
        gameObject.SetActive(true);
    }   
    public void close(){
        gameObject.SetActive(false);
    }
}
