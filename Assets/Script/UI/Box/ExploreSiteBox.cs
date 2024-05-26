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
    public Image[] imgResources;
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
        imgSite.sprite = exploreSite.img;
        textSiteName.text = exploreSite.name;
        textReqMoney.text = exploreSite.requireMoney.ToString();
        int i = 0;
        foreach(Image resourceImg in imgResources){
            if(i < exploreSite.obtainableItems.Length){
                resourceImg.gameObject.SetActive(true);
                resourceImg.sprite = exploreSite.obtainableItems[i].img;
            }else{
                resourceImg.gameObject.SetActive(false);
            }
            i++;
        }
        if(exploreSite.exploreTask != null){
            if(exploreSite.exploreTask.getRemainingTimeSecond() <= 0){
                textReqTime.text = "Done! Click to collect items";
            }else{
                textReqTime.text = "Remaining Time:" + exploreSite.exploreTask.getRemainingTimeFormatted();
            }
        }else{
            textReqTime.text = "Require Time:" + new DateTime(new TimeSpan(0,0,exploreSite.requireTime).Ticks).ToString("HH:mm:ss");
        }
    }
}
