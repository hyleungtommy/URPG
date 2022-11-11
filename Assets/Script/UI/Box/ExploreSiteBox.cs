using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ExploreSiteBox : MonoBehaviour
{
    public Image imgSite;
    public Text textSiteName;
    public Text textReqTime;
    public Text textReqMoney;
    private ExploreSite exploreSite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setExploreSite(ExploreSite exploreSite){
        this.exploreSite = exploreSite;
    }

    public void render(){
        if(exploreSite.exploreTask != null){
            render(exploreSite.exploreTask);
        }else{
            imgSite.sprite = exploreSite.img;
            textSiteName.text = exploreSite.name;
            textReqTime.text = "Require Time:" + new DateTime(new TimeSpan(0,0,exploreSite.requireTime).Ticks).ToString("HH:mm:ss");
            textReqMoney.text = exploreSite.requireMoney.ToString();
        }
        
    }

    public void render(ExploreTask exploreTask){
        imgSite.sprite = exploreTask.exploreSite.img;
        textSiteName.text = exploreTask.exploreSite.name;
        textReqTime.text = "Remaining Time:" + exploreTask.getRemainingTimeFormatted();
        textReqMoney.text = exploreTask.exploreSite.requireMoney.ToString();
    }

    public void updateTime(){
        if(exploreSite.exploreTask != null)
            textReqTime.text = "Remaining Time:" + exploreSite.exploreTask.getRemainingTimeFormatted();
    }

    
}
